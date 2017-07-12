/*            try
            {
                requestChatModel = JsonConvert.DeserializeObject<WebChatModel>(RequestJson);
                AirFrog.LoggerMan.Log(JsonConvert.SerializeObject(requestChatModel));
            }
            catch (Exception e)
            {
                AirFrog.LoggerMan.LogErr(e.ToString());
                return Response.AsJson(badRequestResponse, Nancy.HttpStatusCode.BadRequest);
            }
            return null;
            
     
     
                 Post["/api/mcs/chatmsg"] = parameters =>
            {
                AirFrog.EventHub.Emit("Chat.Public.FromMcs.Group", new StdChatModel {
                    Source = requestChatModel.ServerId,
                    DisplayName = requestChatModel.PlayerName,
                    Text = requestChatModel.Text
                });

                return Response.AsJson(successResponse);
            };
            */

namespace Manila.GamePlatform.WebService.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Nancy;
    using Newtonsoft.Json;

    class UserApiModule : BaseApiModule
    {
        public UserApiModule()
        {
            Post["/api/user/signin"] = parameters =>
            {
                var request = JsonConvert.DeserializeObject<dynamic>(RequestJsonText);
                GamePlatform.Log.Log(JsonConvert.SerializeObject(request));

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

            Post["/api/user/signout"] = parameters =>
            {
                var request = JsonConvert.DeserializeObject<dynamic>(RequestJsonText);
                GamePlatform.Log.Log(JsonConvert.SerializeObject(request));

                try
                {
                    if (string.IsNullOrEmpty((string)Session["Token"]))
                    {
                        return Response.AsJson(new { result = "Failed", errorMsg = "Not Signed In" });
                    }
                    var res = GamePlatform.DataAccess.CloseUserSession((string)Session["Token"]);
                    this.Session["UserId"] = null;
                    this.Session["Token"] = null;
                    return Response.AsJson(new { result = res });
                }
                catch (Exception e)
                {
                    return Response.AsJson(new { result = "Failed", errorMsg = e.Message });
                }
            };
        }
    }
}
