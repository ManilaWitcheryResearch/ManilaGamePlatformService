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
    public class AuthApiModule : BaseApiModule
    {
        protected UserModel currentUser = null;

        private Response BeforeApiRequest(NancyContext ctx)
        {
            try
            {
                if (string.IsNullOrEmpty((string)Session["Token"]) && string.IsNullOrEmpty((string)Session["UserId"]))
                {
                    return Response.AsJson(new { result = "Failed", errorMsg = "NotAuthenicated" });
                }
                currentUser = GamePlatform.DataAccess.ValidateToken((string)Session["Token"]).Value;
                if (currentUser == null)
                {
                    return Response.AsJson(new { result = "Failed", errorMsg = "NotAuthenicated_ValidateTokenErr" });
                }
            }
            catch (Exception e)
            {
                GamePlatform.Log.LogErr(e.ToString());
                return Response.AsJson(badRequestResponse, Nancy.HttpStatusCode.BadRequest);
            }
            return null;
        }
        private void AfterApiResponse(NancyContext ctx)
        {
            ;
        }
        private Response OnApiRequestError(NancyContext ctx, Exception ex)
        {
            GamePlatform.Log.LogErr(ex.ToString());
            return Response.AsJson(internalErrorResponse, Nancy.HttpStatusCode.InternalServerError);
        }
        public AuthApiModule()
        {
            Before += BeforeApiRequest;
            After += AfterApiResponse;
            OnError += OnApiRequestError;
        }
    }
}
