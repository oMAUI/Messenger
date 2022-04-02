package main

import (
	"GoHost/Models"
	"encoding/json"
	"fmt"
	"github.com/gorilla/websocket"
)

var upgreader websocket.Upgrader

func main() {
	conn, _, err := websocket.DefaultDialer.Dial("ws://127.0.0.1:3000/ws", nil)
	if err != nil {
		fmt.Println("err http.get: ", err)
		return
	}
	defer conn.Close()

	fmt.Println("connected")
	fmt.Scanln()
	msg := Models.Msg{
		FromID: 0,
		ToID: 1,
		Message: "Hello",
	}
	msgJson, _ := json.Marshal(msg)
	if errWrite := conn.WriteMessage(2, msgJson); errWrite != nil {
		fmt.Println("Error ", errWrite)
		return
	}

	fmt.Scanln()
}
