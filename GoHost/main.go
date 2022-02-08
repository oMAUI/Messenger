package main

import (
	"fmt"
	"net"
	"strings"
)

var (
	connections = make(map[int]net.Conn)
	i           = 0
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

		connections[i] = conn

		go handleClient(connections, i)
		i++
	}
}

func process(conns map[int]net.Conn, n int) {
	var clientNo int
	buf := make([]byte, 256)
	// получаем доступ к текущему соединению
	conn := conns[n]
	// определим, что перед выходом из функции, мы закроем соединение
	fmt.Println("Accept cnn:", n)
	defer conn.Close()

	for {
		readed_len, err := conns[n].Read(buf)
		if err != nil {
			if err.Error() == "EOF" {
				fmt.Println("Close ", n)
				delete(conns, n)
				break
			}
			fmt.Println(err)
		}

		// Распечатываем полученое сообщение
		// fmt.Println("Received Message:", read_len, buf)
		var message = ""
		message = string(buf[:readed_len])

		// парсинг полученного сообщения
		_, err = fmt.Sscanf(message, "%d", &clientNo) // определи номер клиента
		if err != nil {
			// обработка ошибки формата
			conn.Write([]byte("error format message\n"))
			continue
		}
		pos := strings.Index(message, " ") // нашли позицию разделителя

		if pos > 0 {
			out_message := message[pos+1:] // отчистили сообщение от номера клиента
			// Распечатываем полученое сообщение

			// if buf[0] == 0 {
			conn = conns[clientNo]
			if conn == nil {
				conns[n].Write([]byte("client is close"))
				continue
			}

			// }
			out_buf := []byte(fmt.Sprintf("%d->>%s\n", clientNo, out_message))

			// Отправить новую строку обратно клиенту
			_, err2 := conn.Write(out_buf)

			// анализируем на ошибку
			if err2 != nil {
				fmt.Println("Error:", err2.Error())

				break // выходим из цикла
			}
		}

	}
}

func handleClient(conns map[int]net.Conn, n int) {
	conn := conns[n]
	defer conn.Close()

	buf := make([]byte, 256)
	var clientTo int

	for {
		readedLen, errRead := conn.Read(buf)
		if errRead != nil {
			if errRead.Error() == "EOF" {
				fmt.Println("Close ", n)
				delete(conns, n)
				break
			}
			fmt.Println(errRead)
		}

		message := string(buf[:readedLen])

		_, errParsMsg := fmt.Sscanf(message, "%d", &clientTo) // определи номер клиента
		if errParsMsg != nil {
			conn.Write([]byte("error format message"))
			continue
		}
		position := strings.Index(message, " ")

		if position > 0 {
			outMsg := message[position+1:]
			conn = conns[clientTo]
			if conn == nil {
				conns[n].Write([]byte("client is close"))
				continue
			}

			outBuf := []byte(fmt.Sprintf("%d --> %s\n", clientTo, outMsg))

			_, errWriteOut := conn.Write(outBuf)
			if errWriteOut != nil {
				fmt.Println("Error: ", errWriteOut.Error())
				break
			}
		}
	}
}
