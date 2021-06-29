using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

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
                SendMsg(user.ID + " Removed", 0);
            }
        }

        public void SendMsg(string msg, int id)
        {
            foreach(var item in users)
            {
                string ans = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(i => i.ID == id);

                if (user != null)
                {
                    ans += ", " + user.ID + ": ";
                }

                ans += msg;

                item.operationContext.GetCallbackChannel<ISrverChatCallBack>().MsgCallBack(ans);
            }
        }
    }
}
