package main

import (
	"io"
	"log"
	"net"
	"os"
)

func main() {
	conn, _ := net.Dial("tcp", "localhost:3033") // открываем TCP-соединение к серверу
	go copyTo(os.Stdout, conn)                   // читаем из сокета в stdout
	copyTo(conn, os.Stdin)                       // пишем в сокет из stdin
}

func copyTo(dst io.Writer, src io.Reader) {
	if _, err := io.Copy(dst, src); err != nil {
		log.Fatal(err)
	}
}
