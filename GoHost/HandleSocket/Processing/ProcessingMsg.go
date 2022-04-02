package Processing

import (
	"GoHost/Models"
	"encoding/json"
	"fmt"
	"github.com/gorilla/websocket"
	"sync"
)

type Connections struct {
	mutex           *sync.Mutex
	connections     map[string]*websocket.Conn
	connectionCount int
}

var conns Connections

func Init() {
	conns = Connections{
		mutex:           &sync.Mutex{},
		connections:     make(map[string]*websocket.Conn),
		connectionCount: 0,
	}
}

func read(id string, conn *websocket.Conn) {
	var msg Models.Msg
	defer delete(conns.connections, id)
	defer conn.Close()

	for {
		_, data, errRead := conn.ReadMessage()
		if errRead != nil {
			fmt.Println("err read: %w", errRead)
			conns.connectionCount--
			break
		}
		if errUnmarshal := json.Unmarshal(data, &msg); errUnmarshal != nil {
			fmt.Println("unmarshal, ", errUnmarshal)
			continue
		}

		fmt.Println(msg)
		go Write(msg)
	}
}

func NewConnection(uuid string, connect *websocket.Conn) {
	conns.mutex.Lock()
	conns.connections[uuid] = connect
	conns.connectionCount++
	go read(uuid, connect)

	conns.mutex.Unlock()
	fmt.Printf("new connection with id: %v", conns.connectionCount)
}

func Write(msg Models.Msg) {
	var mutex sync.Mutex
	userTo := conns.connections[msg.ToID]
	msgJson, _ := json.Marshal(msg)

	mutex.Lock()
	if errWrite := userTo.WriteMessage(2, msgJson); errWrite != nil {
		fmt.Printf("err write: %v", errWrite)
		return
	}
	mutex.Unlock()
	fmt.Printf("msg send from: %v to: %v", msg.FromID, msg.ToID)
}
