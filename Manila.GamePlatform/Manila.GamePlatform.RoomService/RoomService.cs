using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manila.GamePlatform.RoomService
{
    using WebSocketSharp;
    using WebSocketSharp.Server;
    using Manila.GamePlatform.Common;

    public class RoomService
    {
        private string listening;
        private bool running = false;
        private WebSocketServer wssv = null;
        private DataAccess dataAccess = null;

        public RoomService(string endpoint, DataAccess da)
        {
            listening = endpoint;
            dataAccess = da;
        }

        public void Start()
        {
            if (running)
            {
                return;
            }
            wssv = new WebSocketServer(listening);
            wssv.Start();
        }

        public void Stop()
        {
            if (running)
            {
                wssv.Stop();
                wssv = null;
            }
        }
    }

    public class Chat : WebSocketBehavior
    {
        private string _suffix;

        public Chat()
          : this(null)
        {
        }

        public Chat(string suffix)
        {
            _suffix = suffix ?? String.Empty;
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Sessions.Broadcast($"[{this.ID}]: {e.Data}");
        }
    }
}
