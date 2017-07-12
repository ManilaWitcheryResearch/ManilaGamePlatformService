namespace Manila.GamePlatform.WebService.Modules
{
    using Nancy;
    using Manila.GamePlatform.WebService.Models;
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return View["index"];
            };

        }
    }
}
