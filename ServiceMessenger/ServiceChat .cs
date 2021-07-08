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

namespace ServiceMessenger
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        NpgsqlConnection pg;

        List<ServerUser> users = new List<ServerUser>();
        int connectionID = 1;
        private object npgSqlCommand;

        public string ConnectionString { get; private set; }

        public int Connection(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = connectionID,
                Login = name,
                operationContext = OperationContext.Current
            };

            connectionID++;

            users.Add(user);

            //SendMsg(user.ID + " Added", 0);

            return user.ID;
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
            string ans = DateTime.Now.ToShortTimeString();

            var userFrom = users.FirstOrDefault(i => i.ID == idFrom);
            var userTo = users.FirstOrDefault(i => i.ID == idTo);

            if (userTo != null)
            {
                ans += ", " + userFrom.ID + ": ";
            }

            ans += msg;

            userTo.operationContext.GetCallbackChannel<ISrverChatCallBack>().MsgCallBack(ans, idFrom);
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

        public int LoginUser(string login, string password)
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
                    //comm.ExecuteNonQuery();
                    return (int)record["id"];
                }
            }
            
            return -1;
        }
    }
}
