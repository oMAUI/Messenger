package Models

type Client struct {
	ID       string    `json:"id,omitempty"`
	Login    string `json:"login,omitempty"`
	Password string `json:"password,omitempty"`
}

type User struct {
	Data Client   `json:"data,omitempty"`
	Contacts []Client `json:"contacts,omitempty"`
}

type Msg struct {
	FromID  string `json:"from_id,omitempty"`
	ToID    string `json:"to_id,omitempty"`
	Message string `json:"message,omitempty"`
}