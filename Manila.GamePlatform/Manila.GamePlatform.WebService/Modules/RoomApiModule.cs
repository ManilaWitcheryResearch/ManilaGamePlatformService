namespace Manila.GamePlatform.WebService.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Nancy;
    using Newtonsoft.Json;
    using Manila.GamePlatform.Common.Models;

    public class RoomApiModule : AuthApiModule
    {
        public RoomApiModule()
        {
            Get["/api/room/getAll"] = parameters =>
            {
                try
                {
                    var res = GamePlatform.DataAccess.RefreshAllRoomInfo();
                    return Response.AsJson(new { result = "Success", roomList = JsonConvert.SerializeObject(res) });
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { result = "Failed", errorMsg = e.Message, roomList = new { } });
                }
            };

            Post["/api/room/createRoom"] = parameters =>
            {
                var request = JsonConvert.DeserializeObject<dynamic>(RequestJsonText);
                GamePlatform.Log.Log(JsonConvert.SerializeObject(RequestJsonText));

                try
                {
                    var res = GamePlatform.DataAccess.CreateRoom(currentUser.UserId);
                    if (!string.IsNullOrEmpty(res))
                    {
                        return Response.AsJson(new { result = "Success", roomId = res});
                    }
                    else
                    {
                        return Response.AsJson(new { result = "Failed", errorMsg = "RequestRoomPermission Failed" });
                    }
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { result = "Failed", errorMsg = e.Message });
                }
            };

            Post["/api/room/requestRoomPermission"] = parameters =>
            {
                var request = JsonConvert.DeserializeObject<dynamic>(RequestJsonText);
                GamePlatform.Log.Log(JsonConvert.SerializeObject(RequestJsonText));

                try
                {
                    var res = GamePlatform.DataAccess.RequestRoomPermission(currentUser.UserId, (string)request["roomId"], (string)request["passwd"]);
                    if (res)
                    {
                        return Response.AsJson(new { result = "Success", roomId = (string)request["roomId"] });
                    }
                    else
                    {
                        return Response.AsJson(new { result = "Failed", errorMsg = "RequestRoomPermission Failed" });
                    }
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { result = "Failed", errorMsg = e.Message });
                }
            };
        }
    }
}
