namespace Manila.GamePlatform.WebService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Nancy.Hosting.Self;
    using Manila.GamePlatform.Common;

    class GamePlatform
    {
        private static NancyHost host;
        public static bool Inited { get; private set; }
        public static Logger Log;
        public static DataAccess DataAccess;

        public static void Init()
        {
            var uri = new Uri("http://localhost:8000");
            Log = new Logger(String.Format("WebService_{0}.log", String.Format("{0:yyyy'-'MM'-'dd'_'HH'-'mm'-'ss}", DateTime.UtcNow)));
            host = new NancyHost(uri);
            DataAccess = new DataAccess();
            Inited = true;
            Log.Log("Successfully inited.");
        }

        public static bool Startup()
        {
            if (Inited == true)
            {
                Log.Log("Starting  WebService.");

                Log.Log("Starting Nancy.");
                host.Start();
                Log.Log("Nancy start successfully.");

                Log.Log(" WebService start successfully.");
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Stop()
        {
            Log.Log("Stopping  WebService.");

            host.Stop();  // stop hosting nancy

            Log.Log(" WebService stopped.");
        }
    }
}
