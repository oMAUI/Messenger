using System.ServiceModel;

namespace ServiceMessenger
{
    class ServerUser
    {
        public int ID { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public OperationContext operationContext { get; set; }
    }
}
