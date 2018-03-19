using System;
using System.Threading.Tasks;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.HttpService;
using System.Collections.Generic;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public class UserClient : IUserClient
    {

        #region PRIVATE MEMBERS

        private readonly IHttpService _httpService;

        #endregion
  

        #region CONSTRUCTOR

        public UserClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion

        #region PUBLIC MEMBERS     


        #region NOTIFEYEAPI


        #endregion


        #region INTEGRATED API

        /// <summary>
        /// Get the currently loggeg in User info. 
        /// </summary>
        /// <returns>User Model</returns>
        public async Task<UserWithAccountInfo> GetUserInfo()
        {
            var retVal = await _httpService.GetAsAsync<UserDetail>("User/GetUserInfo", "", true, false);
            if (retVal != null && retVal.User != null) 
            {
                return retVal.User;
            }
            return null;
        }

        public async Task<UserWithAccountInfo> GetUserInfoWithRegistrationToken(string Token)
        {
            var retVal = await _httpService.GetAsAsyncWithRegistrationToken<UserDetail>("User/GetUserInfo", "", Token);
            if (retVal != null && retVal.User != null)
            {
                return retVal.User;
            }
            return null;
        }

        /// <summary>
        /// Get the User info for the given userID
        /// </summary>
        /// <param name="UserID">Unique indetifier for the User</param>
        /// <returns>User Model</returns>
        public async Task<UserWithAccountInfo> GetUserInfoByID(long UserID)
        {
            string queryParam = string.Format("CustomerID={0}", UserID);

            var retVal = await _httpService.GetAsAsync<UserDetail>("User/GetUserInfoByID", queryParam, true, false);
            if (retVal != null && retVal.User != null)
            {
                return retVal.User;
            }
            return null;
        }

        /// <summary>
        /// Register a New Dasboard User
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>Authetication Token</returns>
        public async Task<string> RegisterNewUser(UserNewRegistration User)
        {
            return await _httpService.PostAsAsyncWithRegistrationToken<string>("User/registernewuser", User);
        }

        /// <summary>
        /// Register the existing notifeye user to the Dashboard portal
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>Authentication Token</returns>
        public async Task<string> RegisterNotifEyeUser(UserNotifEyeRegistration User)
        {
            return await _httpService.PostAsAsyncWithRegistrationToken<string>("User/registernotifeyeuser", User);
        }

        /// <summary>
        /// Get the registration token for the given user
        /// </summary>
        /// <param name="UserName">UserEmail</param>
        /// <returns>Registration Token</returns>
        public async Task<string> GetRegistrationToken(string UserName)
        {
            string queryParam = string.Format("UserName={0}", UserName);

            return await _httpService.GetAsAsync<string>("dashboard/GetRegistrationToken", queryParam, true, true);
        }

        /// <summary>
        /// Create the user
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>unique indetifier for the created user</returns>
        public async Task<long> CreateUser(UserCreate User)
        {
            return await _httpService.PostAsAsync<long>("User/CreateUser", User);
        }

        /// <summary>
        /// Update the user
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>success/failure</returns>
        public async Task<string> UpdateUser(UpdateUser User)
        {
            return await _httpService.PutAsAsync<string>("User/EditUser", User);
        }

        /// <summary>
        /// Delete the user by the ID
        /// </summary>
        /// <param name="UserID">Unique identifier for the user</param>
        /// <returns>Success/Failure</returns>
        public async Task<string> DeleteUser(long UserID)
        {
            string queryParam = string.Format("CustomerID={0}", UserID);
            return await _httpService.DeleteAsAsync<string>("User/DeleteUser", queryParam);
        }

        /// <summary>
        /// Update the User permissions
        /// </summary>
        /// <param name="UserPermission">Update Permissions info</param>
        /// <returns>Success/Failure</returns>
        public async Task<string> UpdateUserPermissions(UserPermission UserPermission)
        {
            return await _httpService.PutAsAsync<string>("User/UpdateUserPermissions", UserPermission);
        }

        /// <summary>
        /// Get the UserPermissions for a given User
        /// </summary>
        /// <param name="UserID">Unique indetofier for the User</param>
        /// <returns>UserPermissions</returns>
        public async Task<List<string>> GetUserPermissons(long UserID)
        {
            string queryParam = string.Format("CustomerID={0}", UserID);

            return await _httpService.GetAsAsync<List<string>>("User/GetUserPermisssions", queryParam, true, false);
        }

        #endregion


        #endregion

        #region PRIVATE MEMBERS    



        #endregion


    }
}
