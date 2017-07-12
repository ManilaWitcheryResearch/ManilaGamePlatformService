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
        public DataAccess(string remoteDbPath = null)
        {
            try
            {
                UserCollection = new Dictionary<string, UserModel>();
                RoomCollection = new Dictionary<string, RoomModel>();
                TokenCollection = new Dictionary<string, TokenModel>();
                ;
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

        public KeyValuePair<string, string> CloseUserSession(string token)
        {
            // call by Web
            // rm token + rm user
            // if oko
            return new KeyValuePair<string, string>("Success", "userid");
        }

        public KeyValuePair<string, UserModel> ValidateToken(string token, string permissionCode = null)
        {
            return new KeyValuePair<string, UserModel>("Pass", new UserModel());
        }

        public string UpdateUserState(string userId, UserState newState)
        {
            // call by Web
            // rm token + rm user
            // if oko
            return "";
        }

        public UserModel GetUserById()
        {
            return new UserModel();
        }

        public UserModel GetUserByUniqueId()
        {
            throw new NotImplementedException();
            return new UserModel();
        }
        #endregion UserSessionCRUD

        #region RoomCRUD
        #endregion RoomCRUD
    }
}
