using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using Npgsql;
using System.Windows.Forms;
using System.Data.Common;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ServiceMessenger
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        NpgsqlConnection pg;

        List<ServerUser> users = new List<ServerUser>();
        int connectionID = 1;
        private object npgSqlCommand;


        private string connectionString = "Server = 95.217.232.188; Port = 7777; Username = habitov; Password = habitov";
        public string ConnectionString { get => connectionString; private set => connectionString = value; }

        public void Connection(string name, int id)
        {
            DBconnection(connectionString);

            ServerUser user = new ServerUser()
            {
                ID = id,
                Login = name,
                operationContext = OperationContext.Current
            };

            users.Add(user);
        }

        public void Diconnection(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);

            if(user != null)
            {
                users.Remove(user);
                //SendMsg(user.ID + " Removed", 0);
            }
        }

        public void SendMsg(string msg, int idFrom, int idTo)
        {
            

            string ans = "";

            var userFrom = users.FirstOrDefault(i => i.ID == idFrom);
            var userTo = users.FirstOrDefault(i => i.ID == idTo);

            if (userTo != null)
            {
                userTo.operationContext.GetCallbackChannel<ISrverChatCallBack>().MsgCallBack(ans, idFrom);
            }

            ans += msg;

            DBconnection(connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(
                "INSERT INTO msg_histories(sender_id, recipient_id, msg, date_time) " +
                "VALUES(" + idFrom.ToString() + ", " + idTo.ToString() + ", '" + msg + "', '" + DateTime.Now.ToString() + "')", pg
                );
            comm.ExecuteNonQuery();

            userFrom.operationContext.GetCallbackChannel<ISrverChatCallBack>().MsgCallBack(ans, idFrom);
        }

        public bool DBconnection(string connStr) 
        {
            pg = new NpgsqlConnection(connStr);

            pg.Open();

            if (pg.FullState == ConnectionState.Broken || pg.FullState == ConnectionState.Closed)
            {
                return false;
            }

            return true;
        }

        public bool AddUserInDB(string login, string password)
        {
            DBconnection("Server = 95.217.232.188; Port = 7777; Username = habitov; Password = habitov");

            NpgsqlCommand comm = new NpgsqlCommand(
                @"SELECT *
                FROM usersmora
                WHERE login like '" + login + "' and password like '" + password + "';", pg);

            NpgsqlDataReader npgSqlDataReader = comm.ExecuteReader();
            foreach (DbDataRecord record in npgSqlDataReader)
            {
                if (npgSqlDataReader.HasRows && (string)record["login"] == login && (string)record["password"] == password)
                {
                    //comm.ExecuteNonQuery();
                    return false;
                }
            }

            DBconnection("Server = 95.217.232.188; Port = 7777; Username = habitov; Password = habitov");

            comm = new NpgsqlCommand(
            @"INSERT into usersmora(login, password)
            values('" + login + "', '" + password + "');", pg);

            comm.ExecuteNonQuery();
            return true;
        }

        public string[] LoginUser(string login, string password)
        {
            DBconnection("Server = 95.217.232.188; Port = 7777; Username = habitov; Password = habitov");

            NpgsqlCommand comm = new NpgsqlCommand(
                @"SELECT *
                FROM usersmora
                WHERE login like '" + login + "' and password like '" + password + "';", pg);

            NpgsqlDataReader npgSqlDataReader = comm.ExecuteReader();
            foreach(DbDataRecord record in npgSqlDataReader)
            {
                if (npgSqlDataReader.HasRows && (string)record["login"] == login && (string)record["password"] == password)
                {
                    string[] userData = new string[3] { record["id"].ToString(), record["login"].ToString(), record["password"].ToString() };
                    //Connection(record["login"].ToString(), int.Parse(record["id"].ToString()));
                    return userData;
                }
            }
            
            return null;
        }

        public byte[] GetUserContact(int id)
        {
            DBconnection(connectionString);
            
            List<List<string[]>> userContact = new List<List<string[]>>();

            NpgsqlCommand comm = new NpgsqlCommand(
                "SELECT * " +
                "FROM user_connection " +
                "WHERE " + id.ToString() + " = user_id", pg
                );

            NpgsqlDataReader data = comm.ExecuteReader();
            
            foreach(DbDataRecord record in data)
            {
                DBconnection(connectionString);

                NpgsqlCommand commGetUser = new NpgsqlCommand(
                "SELECT * " +
                "FROM usersmora " +
                "WHERE " + record["connection_with"].ToString() + " = id", pg
                );

                NpgsqlDataReader dataGetUser = commGetUser.ExecuteReader();
                List<string[]> arrStr = new List<string[]>();
                foreach(DbDataRecord recordGetUser in dataGetUser)
                {
                    string[] arr = new string[2];
                    arr[0] = recordGetUser["id"].ToString();
                    arr[1] = recordGetUser["login"].ToString();
                    arrStr.Add(arr);
                }

                userContact.Add(arrStr);
            }

            return SerializeArr(userContact);
        }

        private void dbComm()
        {

        }

        private byte[] SerializeArr(object arr)
        {
            BinaryFormatter bf = new BinaryFormatter();

            using(var ms = new MemoryStream())
            {
                bf.Serialize(ms, arr);
                return ms.ToArray();
            }
        }

        public byte[] GetMsgHistory(int senderID, int reciptientID)
        {
            DBconnection(connectionString);

            List<object[]> msgHistory = new List<object[]>();

            NpgsqlCommand comm = new NpgsqlCommand(
                "SELECT * " +
                "FROM msg_histories " +
                "WHERE " + senderID.ToString() + " = sender_id AND " + reciptientID.ToString() + " = recipient_id OR " + 
                           reciptientID.ToString() + " = sender_id AND " + senderID.ToString() + " = recipient_id" , pg
                );

            NpgsqlDataReader reader = comm.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                if (reader.HasRows)
                {
                    object[] objects = new object[record.FieldCount];

                    for(int i = 0; i < record.FieldCount; i++)
                    {
                        objects[i] = record[i];
                    }

                    msgHistory.Add(objects);
                }
            }

            return SerializeArr(msgHistory);
        }
    }
}