package HandleSocket

import (
	"GoHost/HandleSocket/Processing"
	"GoHost/HttpProcessing"
	"GoHost/Models"
	"encoding/json"
	"io"
	"io/ioutil"

	"fmt"
	"github.com/go-chi/chi/v5"
	"github.com/go-chi/chi/v5/middleware"
	"github.com/gorilla/websocket"

	"net/http"
)

type IDataBase interface {
	GetUserData(Models.Client) (Models.User, error)
}

type DataBase struct {
	DB IDataBase
}

var (
	connections = make(map[int]*websocket.Conn)
	upgrader    = websocket.Upgrader{
		WriteBufferSize: 1024,
		ReadBufferSize:  1024,
	}
	broadcast = make(chan Models.Client)

	db DataBase

	UserCount int
)

func Route(route DataBase) *chi.Mux {
	db = route

	router := chi.NewRouter()
	router.Use(middleware.Logger)

	router.Get("/ws/{id}", func(w http.ResponseWriter, r *http.Request) {
		UserID := chi.URLParam(r, "id")

		conn, errUpgrade := upgrader.Upgrade(w, r, nil)
		if errUpgrade != nil {
			fmt.Printf("err upgrade %v", errUpgrade)
			return
		}
		fmt.Println("connection upgrade")
		go Processing.NewConnection(UserID, conn)
	})

	router.Post("/login", func(w http.ResponseWriter, r *http.Request) {
		defer r.Body.Close()

		var body Models.Client
		if errBody := UnmarshalBody(r.Body, &body); errBody != nil {
			HttpProcessing.HttpError(w, errBody, "err unmarshal body", "bad request",
				http.StatusBadRequest)
			return
		}

		user, errGetUser := db.DB.GetUserData(body)
		if errGetUser != nil {
			HttpProcessing.HttpError(w, errGetUser, "err get user", "server error",
				http.StatusInternalServerError)
			return
		}

		userB, _ := json.Marshal(user)
		w.Header().Set("Content-Type", "application-json")
		w.Write(userB)
	})

	fmt.Println("start")
	return router
}

func UnmarshalBody(r io.Reader, v interface{}) error {
	resp, errResp := ioutil.ReadAll(r)
	if errResp != nil {
		//ErrorPorcessing.HttpError(w, errResp, "failed to get body", "Bad Request", http.StatusBadRequest)
		return fmt.Errorf("server error: %w", errResp)
	}

	fmt.Println(string(resp))
	if errUnmarshalJson := json.Unmarshal(resp, v); errUnmarshalJson != nil {
		//ErrorPorcessing.HttpError(w, errUnmarshalJson, "failed to get Json in Authorization", "Server Error", http.StatusInternalServerError)
		return fmt.Errorf("server error: %w", errUnmarshalJson)
	}

	return nil
}
