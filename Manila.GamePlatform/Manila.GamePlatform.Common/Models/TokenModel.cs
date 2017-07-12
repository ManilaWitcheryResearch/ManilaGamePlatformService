using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manila.GamePlatform.Common.Models
{
    public class TokenModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }
        public List<string> PermissionList { get; set; }
    }
}
