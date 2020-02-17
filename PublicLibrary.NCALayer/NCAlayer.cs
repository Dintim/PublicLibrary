using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PublicLibrary.NCALayer
{

    public class Info
    {
        public string module { get; set; }
        public string method { get; set; }
        public Object[] args { get; set; }

        public Info(string module, string method, Object[] args)
        {
            this.module = module;
            this.method = method;
            this.args = args;
        }
    }

    
    public class ActiveTokens
    {
        public string[] responseObjects { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }


    public class NCAlayer
    {
        public WebSocket socket = null;
        bool isConnect = false;

        public NCAlayer():this("http://127.0.0.1:13579", "ws://127.0.0.1:13579")
        {
        }

        public NCAlayer(string origin, string cryptoSocketUri)
        {
            socket = new WebSocket(cryptoSocketUri);
            socket.Origin = origin;
            socket.OnOpen += Socket_OnOpen;
            socket.OnClose += Socket_OnClose;
            socket.OnError += Socket_OnError;

            socket.Connect();
        }

        private void Socket_OnOpen(object sender, EventArgs args)
        {

        }

        private void Socket_OnClose(object sender, CloseEventArgs close)
        {

        }

        private void Socket_OnError(object sender, ErrorEventArgs e)
        {

        }

        public bool getActiveTokens()
        {
            if (!isConnect)
            {
                return false;
            }

            socket.OnMessage += (sender, e) =>
              {
                  ActiveTokens result = JsonConvert.DeserializeObject<ActiveTokens>(e.Data);
              };

            Info info = new Info("kz.gov.pki.knca.commonUtils", "getActiveTokens", null);

            socket.SendAsync(JsonConvert.SerializeObject(info), (result) =>
            {
                var test = result;
            });

            return true;

        }
    }
}
