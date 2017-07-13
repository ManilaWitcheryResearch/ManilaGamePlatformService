namespace Manila.GamePlatform.WebService.Modules
{
    using System;
    using Newtonsoft.Json;
    using Nancy;
    using Manila.GamePlatform.WebService.Models;
    public class NewUserApiModule : BaseApiModule
    {
        public NewUserApiModule()
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
