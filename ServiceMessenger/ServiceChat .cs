using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using Npgsql;
using System.Windows.Forms;

namespace ServiceMessenger
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int connectionID = 1;

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

            userFrom.operationContext.GetCallbackChannel<ISrverChatCallBack>().MsgCallBack(ans, idFrom);
            userTo.operationContext.GetCallbackChannel<ISrverChatCallBack>().MsgCallBack(ans, idTo);
        }

        public bool DBconnection(string connStr) 
        {
            NpgsqlConnection pg = new NpgsqlConnection(connStr);

            pg.Open();

            if (pg.FullState == ConnectionState.Broken || pg.FullState == ConnectionState.Closed)
            {
                return false;
            }

            return true;
        }
    }
}
