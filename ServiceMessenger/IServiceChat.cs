using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceMessenger
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(ISrverChatCallBack))]
    public interface IServiceChat
    {
        [OperationContract]
        void Connection(string login, int id);

        [OperationContract]
        void Diconnection(int id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int idFrom, int idTo);

        [OperationContract]
        bool DBconnection(string connStr);

        [OperationContract]
        bool AddUserInDB(string login, string password);

        [OperationContract]
        string[] LoginUser(string login, string password);

        [OperationContract]
        byte[] GetUserContact(int id);

        [OperationContract]
        byte[] GetMsgHistory(int senderID, int reciptientID);
    }

    public interface ISrverChatCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallBack(string msg, int id);
    }
}
