using Mora.Models;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Text;

namespace Mora.Connection
{
    internal class ServerConnection
    {
        private NpgsqlConnection pg;

        private string wsUrl;
        private string httpUrl;
        private string port;
        private string loginUri;



        public ServerConnection()
        {
            wsUrl = ConfigurationManager.AppSettings.Get("ws_server_address");
            httpUrl = ConfigurationManager.AppSettings.Get("http_server_address");
            port = ConfigurationManager.AppSettings.Get("port_server_address");
            loginUri = ConfigurationManager.AppSettings.Get("login_uri");
        }

        public User Login(Client userData)
        {
            string data = JsonSerializer.Serialize(userData);
            byte[] dataB = Encoding.UTF8.GetBytes(data);

            WebRequest request = WebRequest.Create(httpUrl + port + loginUri);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = dataB.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(dataB, 0, dataB.Length);
            }

            string respData;
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    respData = reader.ReadToEnd();
                }
            }

            if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
            {
                User userResp = JsonSerializer.Deserialize<User>(respData);
                if (userResp.contacts == null)
                    userResp.contacts = new List<Client>();
                
                return userResp;

            }
            else
                return null;
        }

        public List<User> GetUserContacts(User userID)
        {
            List<User> userContacts = new List<User>();
            string data = ""; //JsonConvert.SerializeObject(userID);
            byte[] dataB = Encoding.Unicode.GetBytes(data);

            WebRequest request = WebRequest.Create(httpUrl + loginUri);
            request.Method = "GET";

            request.ContentType = "application/json";
            request.ContentLength = dataB.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(dataB, 0, dataB.Length);
            }

            string respData;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    respData = reader.ReadToEnd();
                }
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return null; //JsonConvert.DeserializeObject<List<User>>(respData);
            }

            return null;
        }
    }
}
