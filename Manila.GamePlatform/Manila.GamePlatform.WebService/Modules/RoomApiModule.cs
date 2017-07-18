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
                    return Response.AsJson(new { result = "Success", roomList = res});
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
                    var res = GamePlatform.DataAccess.CreateUserSession((string)request["userId"]);
                    if (res.Key == "Success")
                    {
                        this.Session["UserId"] = res.Value.UserId;
                        this.Session["Token"] = res.Value.Token;
                        return Response.AsJson(new { result = "Success", displayName = res.Value.DisplayName, token = res.Value.Token });
                    }
                    else
                    {
                        return Response.AsJson(new { result = "Failed" });
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
                    var res = GamePlatform.DataAccess.CreateUserSession((string)request["userId"]);
                    if (res.Key == "Success")
                    {
                        this.Session["UserId"] = res.Value.UserId;
                        this.Session["Token"] = res.Value.Token;
                        return Response.AsJson(new { result = "Success", displayName = res.Value.DisplayName, token = res.Value.Token });
                    }
                    else
                    {
                        return Response.AsJson(new { result = "Failed" });
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
