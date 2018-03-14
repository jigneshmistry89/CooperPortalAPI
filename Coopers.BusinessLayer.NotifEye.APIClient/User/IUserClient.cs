using Coopers.BusinessLayer.Model.DTO;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public interface IUserClient
    {
        /// <summary>
        /// Get the currently loggeg in User info. 
        /// </summary>
        /// <returns>User Model</returns>
        Task<UserWithAccountInfo> GetUserInfo();

        /// <summary>
        /// Get the User info for the given userID
        /// </summary>
        /// <param name="UserID">Unique indetifier for the User</param>
        /// <returns>User Model</returns>
        Task<UserWithAccountInfo> GetUserInfoByID(long UserID);

        /// <summary>
        /// Register a New Dasboard User
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>Authetication Token</returns>
        Task<string> RegisterNewUser(UserNewRegistration User);

        /// <summary>
        /// Register the existing notifeye user to the Dashboard portal
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>Authentication Token</returns>
        Task<string> RegisterNotifEyeUser(UserNotifEyeRegistration User);

        /// <summary>
        /// Get the registration token for the given user
        /// </summary>
        /// <param name="UserName">UserEmail</param>
        /// <returns>Registration Token</returns>
        Task<string> GetRegistrationToken(string UserName);
      

        /// <summary>
        /// Create the user
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>unique indetifier for the created user</returns>
        Task<long> CreateUser(UserCreate User);

        /// <summary>
        /// Update the user
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>success/failure</returns>
        Task<string> UpdateUser(UpdateUser User);

        /// <summary>
        /// Delete the user by the ID
        /// </summary>
        /// <param name="UserID">Unique identifier for the user</param>
        /// <returns>Success/Failure</returns>
        Task<string> DeleteUser(long UserID);

        /// <summary>
        /// Update the User permissions
        /// </summary>
        /// <param name="UserPermission">Update Permissions info</param>
        /// <returns>Success/Failure</returns>
        Task<string> UpdateUserPermissions(UserPermission UserPermission);

    }
}
