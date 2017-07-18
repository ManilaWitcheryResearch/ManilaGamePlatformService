namespace Manila.GamePlatform.RoomService.RoomPlugins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WebSocketSharp;
    using WebSocketSharp.Server;
    using Manila.GamePlatform.Common;

    public class BaseRoomPlugin : WebSocketBehavior
    {
        private string RoomId;
        private bool running;
        private DataAccess dataAccess;

        protected override void OnOpen()
        {
            //_name = Context.QueryString["name"]; I guess here many objects of a kind of plugin, so use other ds to store ID =>  USERID
        }

        public BaseRoomPlugin(string roomId, ref DataAccess da)
        {
            RoomId = roomId;
            dataAccess = da;
            running = false;
        }

        public void Start()
        {
            dataAccess.WssInitRoom(RoomId);
            running = true;
        }

        public void Stop()
        {
            running = false;
        }
    }
}
