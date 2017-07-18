namespace Manila.GamePlatform.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.Serialization;

    public enum RoomState
    {
        Open = 0,
        InGame = 1,
        PreOpen = 2,
        Closed = 3
    }

    public class RoomModel
    {
        public string RoomId { get; set; }
        public string RoomName { get; set; }
        public string GameType { get; set; }
        public List<string> PlayerList { get; set; } // list by userid
        public string Password { get; set; }
        public int RoundWaitTime { get; set; }
        public int PlayerCount { get; set; }
        public int PlayerUpperBound { get; set; }
        public int PlayerLowerBound { get; set; }
        public RoomState RoomState { get; set; }
        public string OwnerId { get; set; }
        public object locker = new object();

        // TODO: owner, custom_player_amount
        // TODO: bind U/L bound to gametype
    }

    [DataContract]
    public class PublicRoomModel
    {
        [DataMember(Name = "id")]
        public string RoomId { get; set; }
        [DataMember(Name = "name")]
        public string RoomName { get; set; }
        [DataMember(Name = "gameType")]
        public string GameType { get; set; }
        [DataMember(Name = "playerList")]
        public List<PublicPlayerModel> PlayerList { get; set; }
        [DataMember(Name = "havePassword")]
        public bool HavePassword { get; set; }
        [DataMember(Name = "roundWaitTime")]
        public int RoundWaitTime { get; set; }
        [DataMember(Name = "playerCount")]
        public int PlayerCount { get; set; }
        [DataMember(Name = "playerUpperBound")]
        public int PlayerUpperBound { get; set; }
        [DataMember(Name = "playerLowerBound")]
        public int PlayerLowerBound { get; set; }
        [DataMember(Name = "roomState")]
        public int RoomState { get; set; }
        [DataMember(Name = "ownerId")]
        public string OwnerId { get; set; }
    }
}
