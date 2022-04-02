using Mora.Designs.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using Newtonsoft.Json;
using Mora.Models;

namespace Mora.Connection
{
    internal class WsConnection
    {
        private string _address;
        private string _port;
        private string _url;

        private WebSocket connection;
        private IMsg controller;

        public WsConnection(string address, string port, IMsg Controller)
        {
            _address = address;
            _port = port;
            _url = address + port;
            this.controller = Controller;
        }

        public void Connect(string userID)
        {
            try
            {
                connection = new WebSocket(_url + "/ws/" + userID);
                connection.SetCredentials(userID, "", false);
                connection.ConnectAsync();
                connection.OnMessage += Socket_OnMessage;
            }
            catch (Exception ex)
            {
                Task.Delay(2000);
                Connect(_url + "/ws/" + userID);
            }
        }

        private void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            string text = Encoding.Default.GetString(e.RawData);
            
            var msg = JsonConvert.DeserializeObject<Msg>(text);

            controller.DrawMsgBox(msg.from_id, msg.message);
        }

        public void SendMsg(string FromID, string ToID, string Message)
        {
            try
            {
                var msg = new Msg
                {
                    from_id = FromID,
                    to_id = ToID,
                    message = Message
                };

                string msgJson = JsonConvert.SerializeObject(msg);
                connection.Send(msgJson);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
