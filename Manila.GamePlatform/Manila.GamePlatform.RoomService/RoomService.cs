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
    using Manila.GamePlatform.RoomService.RoomPlugins;

    public class RoomService
    {
        private string listening;
        private bool running = false;
        private WebSocketServer wssv = null;
        private DataAccess dataAccess = null;

        public bool ServeRoom(string roomId, string roomType)
        {
            wssv.AddWebSocketService<BaseRoomPlugin>(MakeRoomUrl(roomId), () => new BaseRoomPlugin(roomId, ref dataAccess)); // add reflect to choose different plugin
            return true;
        }

        public bool CloseRoom(string roomId)
        {
            wssv.RemoveWebSocketService(MakeRoomUrl(roomId));
            return true;
        }

        public RoomService(string endpoint, ref DataAccess da)
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
            wssv.AddWebSocketService<Chat>("/Chat");
        }

        public void Stop()
        {
            if (running)
            {
                wssv.Stop();
                wssv = null;
            }
        }

        static public string MakeRoomUrl(string roomId)
        {
            return $"/wss/room/{roomId}";
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
