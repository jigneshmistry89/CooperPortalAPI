using Coopers.BusinessLayer.Model.DTO;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public interface IUserApplicationService
    {
        /// <summary>
        /// Get the currently loggeg in User info. 
        /// </summary>
        /// <returns>User Model</returns>
        Task<UserWithAccountInfo> GetUserInfo();

        /// <summary>
        /// Get the User details of the currently loggedin user. It will also get the Users List info and the Payment histories if the user is admin
        /// </summary>
        /// <returns>User details</returns>
        Task<UserDetailWithPaymentHistory> GetUserDetails();

        /// <summary>
        /// Get the User info for the given userID
        /// </summary>
        /// <param name="UserID">Unique indetifier for the User</param>
        /// <returns>User Model</returns>
        Task<UserWithAccountInfo> GetUserInfoByID(long UserID);

        Task<string> SendRegisterationLink(string UserName);

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

    }
}
