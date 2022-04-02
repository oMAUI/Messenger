package HttpProcessing

import (
	"encoding/json"
	"fmt"
	"go.uber.org/zap"
	"net/http"
)

type CustomError struct {
	Message string `json:"message"`
}

func HttpError(w http.ResponseWriter, err error, msgForLogger string, msgForResponse string, code int) {
	w.Header().Set("Content-Type", "application/json")
	ce := CustomError{
		Message: msgForResponse,
	}

	fmt.Println(msgForLogger, err)
	res, errGetJson := json.Marshal(ce)
	if errGetJson != nil {
		zap.S().Errorw("marshal", "error", errGetJson)
		w.WriteHeader(http.StatusInternalServerError)
		w.Write([]byte("error"))
		return
	}

	w.WriteHeader(code)
	w.Write(res)
}
