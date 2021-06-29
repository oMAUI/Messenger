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
        int Connection();

        [OperationContract]
        void Diconnection();

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg);
    }

    public interface ISrverChatCallBack
    {
        [OperationContract]
        void MsgCallBack();
    }
}
