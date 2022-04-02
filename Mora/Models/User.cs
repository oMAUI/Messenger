using System.Collections.Generic;
using Newtonsoft.Json;

namespace Mora.Models
{
    public class Client
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("login")]
        public string login { get; set; }
        [JsonProperty("password")]
        public string password { get; set; }
    }

    public class User
    {
        [JsonProperty(PropertyName = "data")]
        public Client data { get; set; }
        [JsonProperty(PropertyName = "contacts")]
        public List<Client> contacts { get; set; }
    }

    public class Msg
    {
        public string from_id { get; set; }
        public string to_id { get; set; }
        public string message { get; set; }
    }
}
