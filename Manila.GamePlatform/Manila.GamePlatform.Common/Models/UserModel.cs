namespace Manila.GamePlatform.Common.Models
{
    using System.Runtime.Serialization;
    
    // Tmp data using userid, Stored data using uniqueid
    public class UserModel
    {
        public string UniqueId { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
    }

    [DataContract]
    public class PublicPlayerModel
    {
        [DataMember(Name = "userId")]
        public string UserId { get; set; }
        [DataMember(Name = "DisplayName")]
        public string DisplayName { get; set; }
    }
}
