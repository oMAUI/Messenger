package main

import (
	"GoHost/Models"
	"encoding/json"
	"fmt"
	"github.com/gorilla/websocket"
)

func main(){
	conn, _, err := websocket.DefaultDialer.Dial("ws://127.0.0.1:3000/ws", nil)
	if err != nil {
		fmt.Println("err http.get: ", err)
		return
	}

	go func(){
		for {
			var ans Models.Msg
			_, data, errRead := conn.ReadMessage()
			if errRead != nil {
				fmt.Println("err read: ", errRead)
				return
			}
			json.Unmarshal(data, &ans)
			fmt.Println(ans)
		}
	}()

	fmt.Scanln()
}
