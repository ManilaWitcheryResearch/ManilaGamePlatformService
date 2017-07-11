namespace Manila.GamePlatform.WebService.Modules
{
    using System;
    using Nancy;
    using Newtonsoft.Json;
    //using Manila.GamePlatform.WebService.Models;

    public class BaseApiModule : NancyModule
    {
        protected object successResponse = new { result = "success", errormsg = "" };
        protected object badRequestResponse = new { result = "failed", errormsg = "bad request" };
        protected object internalErrorResponse = new { result = "failed", errormsg = "internal error" };
        protected string RequestJson = "{}";

        private Response BeforeApiRequest(NancyContext ctx)
        {
            try
            {
                var id = this.Request.Body;
                var length = this.Request.Body.Length;
                var data = new byte[length];
                id.Read(data, 0, (int)length);
                var body = System.Text.Encoding.Default.GetString(data);
                //AirFrog.LoggerMan.Log(body);
                RequestJson = body;
            }
            catch (Exception e)
            {
                //AirFrog.LoggerMan.LogErr(e.ToString());
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
            //AirFrog.LoggerMan.LogErr(ex.ToString());
            return Response.AsJson(internalErrorResponse, Nancy.HttpStatusCode.InternalServerError);
        }
        public BaseApiModule()
        {
            Before += BeforeApiRequest;
            After += AfterApiResponse;
            OnError += OnApiRequestError;

            //Get["/api/version"] = parameters =>
            //{
            //    var request = JsonConvert.DeserializeObject<ChatModel>(RequestJson);

            //    AirFrog.LoggerMan.Log(JsonConvert.SerializeObject(request));

            //    return Response.AsJson(successResponse);
            //};
        }
    }
}
