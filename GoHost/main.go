package main

import (
	PgDataBase "GoHost/DataBase"
	"GoHost/HandleSocket"
	"GoHost/HandleSocket/Processing"
	"fmt"
	"log"
	"net/http"
)

func main() {
	conn, errConnection := PgDataBase.Connection("postgresql://maui:maui@192.168.0.11:5432/postgres")
	if errConnection != nil {
		fmt.Errorf("db connect error: %w", errConnection)
		log.Printf("connect error: %v", errConnection.Error())
		return
	}
	Processing.Init()
	router := HandleSocket.Route(HandleSocket.DataBase{DB: conn})
	if errListen := http.ListenAndServe(":3030", router); errListen != nil {
		fmt.Println("listen error: ", errListen.Error())
		log.Printf("listen error: %v", errListen.Error())
	}
}



//func process(conns map[int]net.Conn, n int) {
//	var clientNo int
//	buf := make([]byte, 256)
//	// получаем доступ к текущему соединению
//	conn := conns[n]
//	// определим, что перед выходом из функции, мы закроем соединение
//	fmt.Println("Accept cnn:", n)
//	defer conn.Close()
//
//	for {
//		readed_len, err := conns[n].Read(buf)
//		if err != nil {
//			if err.Error() == "EOF" {
//				fmt.Println("Close ", n)
//				delete(conns, n)
//				break
//			}
//			fmt.Println(err)
//		}
//
//		// Распечатываем полученое сообщение
//		// fmt.Println("Received Message:", read_len, buf)
//		var message = ""
//		message = string(buf[:readed_len])
//
//		// парсинг полученного сообщения
//		_, err = fmt.Sscanf(message, "%d", &clientNo) // определи номер клиента
//		if err != nil {
//			// обработка ошибки формата
//			conn.Write([]byte("error format message\n"))
//			continue
//		}
//		pos := strings.Index(message, " ") // нашли позицию разделителя
//
//		if pos > 0 {
//			out_message := message[pos+1:] // отчистили сообщение от номера клиента
//			// Распечатываем полученое сообщение
//
//			// if buf[0] == 0 {
//			conn = conns[clientNo]
//			if conn == nil {
//				conns[n].Write([]byte("client is close"))
//				continue
//			}
//
//			// }
//			out_buf := []byte(fmt.Sprintf("%d->>%s\n", clientNo, out_message))
//
//			// Отправить новую строку обратно клиенту
//			_, err2 := conn.Write(out_buf)
//
//			// анализируем на ошибку
//			if err2 != nil {
//				fmt.Println("Error:", err2.Error())
//
//				break // выходим из цикла
//			}
//		}
//
//	}
//}


