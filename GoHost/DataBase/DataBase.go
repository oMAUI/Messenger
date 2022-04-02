package PgDataBase

import (
	"GoHost/Models"
	_ "github.com/jackc/pgx/v4/stdlib"
	"github.com/jmoiron/sqlx"
)

type DB struct {
	conn *sqlx.DB
}

func Connection(url string) (*DB, error){
	connUrl := url

	dataBase, errConnect := sqlx.Connect("pgx", connUrl)
	if errConnect != nil {
		return nil, errConnect
	}

	return &DB {
		conn: dataBase,
	}, nil
}

func (db *DB) GetUserData(user Models.Client) (Models.User, error) {
	var userResp Models.Client
	if errGet := db.conn.Get(&userResp, `
		SELECT id, login, password
		FROM users
		WHERE login = $1 and password = $2;`, user.Login, user.Password);
	errGet != nil {

	}

	contacts, errGetContacts := db.getUserContacts(userResp.ID)
	if errGetContacts != nil {
		return Models.User{}, errGetContacts
	}

	userData := Models.User{
		Data: userResp,
		Contacts: contacts,
	}

	return userData, nil
}

func (db *DB) getUserContacts(id string) ([]Models.Client, error){
	var contactsWithIDs []string
	if errSelect := db.conn.Select(&contactsWithIDs, `
		SELECT contact_with
		FROM user_contact_with
		WHERE user_id = $1`, id);
	errSelect != nil {
		return nil, errSelect
	}

	var users []Models.Client
	for i := 0; i < len(contactsWithIDs); i++ {
		var user Models.Client
		if errSelect := db.conn.Get(&user, `
			SELECT id, login
			FROM users
			WHERE id = $1`, contactsWithIDs[i]);
		errSelect != nil {
			return nil, errSelect
		}

		users = append(users, user)
	}

	return users, nil
}

//func (dataBase *DB) PatchMessage(sender Models.Client) error {
//	_, errPatchMsg := dataBase.conn.Exec("INSERT INTO msg_histories (sender_id, recepient_id, msg, date_time) " +
//		"VALUES ($1, $2, $3)", sender.ID, sender.IDto, sender.Message)
//	if errPatchMsg != nil {
//		return errPatchMsg
//	}
//
//	return nil
//}
