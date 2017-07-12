namespace Manila.GamePlatform.WebService
{
    using Nancy;
    using Nancy.Session;
    using Nancy.TinyIoc;
    using Nancy.Bootstrapper;
    using Nancy.Conventions;
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            CookieBasedSessions.Enable(pipelines);
        }
    }
}
