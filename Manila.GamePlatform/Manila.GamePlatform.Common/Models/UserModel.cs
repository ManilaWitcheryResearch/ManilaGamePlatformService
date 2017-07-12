namespace Manila.GamePlatform.Common.Models
{
    using System.Runtime.Serialization;

    public enum UserState
    {
        InHall = 0,
        InRoom = 1,
        InGame = 2,
    }
    
    // Tmp data using userid, Stored data using uniqueid
    public class UserModel
    {
        public string UniqueId { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public UserState UserState { get; set; }
    }

    [DataContract]
    public class PublicPlayerModel
    {
        [DataMember(Name = "userId")]
        public string UserId { get; set; }
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
    }
}
