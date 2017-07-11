namespace Manila.GamePlatform.WebService
{
    using System;
    using Mono.Unix;
    using Mono.Unix.Native;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Utility
    {
        public static bool CheckIfRunningOnMono()
        {
            if (Type.GetType("Mono.Runtime") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
