namespace Manila.GamePlatform.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.Serialization;

    class BaseModels
    {
    }

    [DataContract]
    public class ResultBaseModel
    {
        [DataMember(Name = "result")]
        public string Result { get; set; }
        [DataMember(Name = "errormsg")]
        public string ErrorMsg { get; set; }

        public ResultBaseModel(string result = "", string errorMsg = "")
        {
            Result = result;
            ErrorMsg = errorMsg;
        }
    }
}
