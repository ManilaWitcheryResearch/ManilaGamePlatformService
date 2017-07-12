using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manila.GamePlatform.Common
{
    using Manila.GamePlatform.Common.Models;
    public class DataAccess
    {
        private Dictionary<string, UserModel> UserCollection { get; set; }
        private Dictionary<string, RoomModel> RoomCollection { get; set; }
        private Dictionary<string, TokenModel> TokenCollection { get; set; }

        private ILogger Log;
        public DataAccess(string remoteDbPath = null)
        {
            try
            {
                UserCollection = new Dictionary<string, UserModel>();
                RoomCollection = new Dictionary<string, RoomModel>();
                TokenCollection = new Dictionary<string, TokenModel>();
                Log = new Logger("tmp");
            }
            catch (Exception e)
            {
                throw new Exception("DataAccess initial failed.", e);
            }
        }

        #region UserSessionCRUD
        public KeyValuePair<string, UserModel> CreateUserSession(string userId, string casToken = null)
        {
            // call by Web
            
            if (UserCollection.ContainsKey(userId))
            {
                return new KeyValuePair<string, UserModel>("Failed", null);
            }

            var token = Utils.GeneratePersonalToken();
            while (TokenCollection.ContainsKey(token))
            {
                token = Utils.GeneratePersonalToken();
            }

            var userModel = new UserModel()
            {
                UniqueId = userId,
                UserId = userId,
                DisplayName = userId,
                Token = token,
                UserState = UserState.InHall,
            };

            UserCollection.Add(userId, userModel);

            TokenCollection.Add(token, new TokenModel()
            {
                UserId = userId,
                Token = token,
                PermissionList = new List<string>(),
            });
            
            return new KeyValuePair<string, UserModel>("Success", userModel);
        }

        public string CloseUserSession(string token)
        {
            // call by Web
            
            var res = ValidateToken(token);
            if (res.Key == "Pass")
            {
                TokenCollection.Remove(token);
                UserCollection.Remove(res.Value.UserId);
            }
            return "Success";
        }

        public KeyValuePair<string, UserModel> ValidateToken(string token, string permissionCode = null)
        {
            if (TokenCollection.ContainsKey(token))
            {
                var user = UserCollection[TokenCollection[token].UserId];
                if (string.IsNullOrEmpty(permissionCode))
                {
                    return new KeyValuePair<string, UserModel>("Pass", user);
                }
                else
                {
                    if (TokenCollection[token].PermissionList.Contains(permissionCode))
                    {
                        return new KeyValuePair<string, UserModel>("Pass", user);
                    }
                }
            }
            return new KeyValuePair<string, UserModel>("Failed", null);
        }

        public string UpdateUserState(string userId, UserState newState)
        {
            // call by room

            if (UserCollection.ContainsKey(userId))
            {
                UserCollection[userId].UserState = newState;
                return "Success";
            }
            return "Failed";
        }

        public UserModel GetUserById(string userId)
        {
            if (UserCollection.ContainsKey(userId))
            {
                return UserCollection[userId];
            }
            return null;
        }

        public UserModel GetUserByUniqueId()
        {
            throw new NotImplementedException();
        }
        #endregion UserSessionCRUD

        #region RoomCRUD
        public List<PublicRoomModel> RefreshAllRoomInfo()
        {
            var result = RoomCollection.Values.Select(x => new PublicRoomModel()
            {
                RoomId = x.RoomId,
                RoomName = x.RoomName,
                GameType = x.GameType,
                PlayerLowerBound = x.PlayerLowerBound,
                PlayerUpperBound = x.PlayerUpperBound,
                HavePassword = (!string.IsNullOrEmpty(x.Password)),
                RoundWaitTime = x.RoundWaitTime,
                RoomState = (int)x.RoomState,
                PlayerList = x.PlayerList.Select<string, PublicPlayerModel>(p => new PublicPlayerModel()
                {
                    UserId = p,
                    DisplayName = UserCollection[p].DisplayName,
                }).ToList(),
            }).ToList();
            return result;
        }
        #endregion RoomCRUD
    }
}
