using System;
using System.Collections.Generic;
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
        int Connection(string login);

        [OperationContract]
        void Diconnection(int id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id);
    }

    public interface ISrverChatCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallBack(string msg);
    }
}
