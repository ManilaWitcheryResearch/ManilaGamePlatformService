using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manila.GamePlatform.Common
{
    public class Utils
    {
        public static string GeneratePersonalToken()
        {
            // new guid
            Guid token = Guid.NewGuid();
            return token.ToString();
        }
    }
}
