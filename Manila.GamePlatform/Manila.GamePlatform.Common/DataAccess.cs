using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manila.GamePlatform.Common
{
    class DataAccess
    {
        public DataAccess(string remoteDbPath = null)
        {
            try
            {
                ;
            }
            catch (Exception e)
            {
                throw new Exception("DataAccess initial failed.", e);
            }
        }
    }
}
