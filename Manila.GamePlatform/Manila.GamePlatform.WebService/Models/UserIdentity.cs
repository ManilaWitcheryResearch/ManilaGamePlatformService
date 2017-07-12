using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manila.GamePlatform.WebService.Models
{
    using Nancy.Security;
    public class UserIdentity : IUserIdentity
    {
        public IEnumerable<string> Claims { get; set; }

        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
