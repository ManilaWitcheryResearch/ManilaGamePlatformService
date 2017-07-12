using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manila.GamePlatform.RoomService
{
    using WebSocketSharp;
    using WebSocketSharp.Server;

    class Program
    {
        static void Main(string[] args)
        {
            var wssv = new WebSocketServer("ws://0.0.0.0:8080");
            wssv.Start();
            Console.ReadKey();
            wssv.Stop();
        }
    }
}