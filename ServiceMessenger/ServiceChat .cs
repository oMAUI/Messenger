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
        int connectionID;

        public int Connection()
        {
            throw new NotImplementedException();
        }

        public void Diconnection()
        {
            throw new NotImplementedException();
        }

        public void SendMsg(string msg)
        {
            throw new NotImplementedException();
        }
    }
}
