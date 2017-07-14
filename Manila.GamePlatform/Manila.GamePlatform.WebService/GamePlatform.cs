namespace Manila.GamePlatform.WebService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Nancy.Hosting.Self;
    using Manila.GamePlatform.Common;
    using Manila.GamePlatform.RoomService;

    class GamePlatform
    {
        private static NancyHost host;
        private static RoomService wshost;
        public static bool Inited { get; private set; }
        public static Logger Log;
        public static DataAccess DataAccess;


        public static void Init()
        {
            var uri1 = new Uri("http://localhost:8000");
            var uri2 = "ws://0.0.0.0:8888";
            Log = new Logger(String.Format("WebService_{0}.log", String.Format("{0:yyyy'-'MM'-'dd'_'HH'-'mm'-'ss}", DateTime.UtcNow)));
            host = new NancyHost(uri1);
            wshost = new RoomService(uri2, GamePlatform.DataAccess);
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

                Log.Log("Starting WSS.");
                wshost.Start();
                Log.Log("WSS start successfully.");

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
            wshost.Stop();  // stop hosting wss

            Log.Log(" WebService stopped.");
        }
    }
}
