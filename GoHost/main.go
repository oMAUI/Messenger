package main

import (
	"fmt"
	"net"
)

func main() {
	listener, errListenSocket := net.Listen("tcp", "localhost:3033")
	if errListenSocket != nil {
		fmt.Println(errListenSocket)
		return
	}

	for {
		conn, errAccept := listener.Accept()
		if errAccept != nil {
			fmt.Println(errAccept)
			continue
		}

		go handleClient(conn)
	}
}

func handleClient(conn net.Conn) {
	defer conn.Close()

	buf := make([]byte, 32)
	for {
		conn.Write([]byte("connected"))

		readLen, errRead := conn.Read(buf)
		if errRead != nil {
			fmt.Println(errRead)
			break
		}

		conn.Write(append([]byte("Goodbye, "), buf[:readLen]...))
	}
}
