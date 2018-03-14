using Coopers.BusinessLayer.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Model.Interface
{
    public interface IHttpContextProvider
    {
        /// <summary>
        /// Get the token passed by the user.
        /// </summary>
        /// <returns>Token</returns>
        string GetToken();

        /// <summary>
        /// Get the registration token
        /// </summary>
        /// <returns>Token</returns>
        string GetRegistrationToken();

        /// <summary>
        /// Get the Authentication token for the Integrated Notfiy API
        /// </summary>
        /// <returns>Athentication Token</returns>
        string GetIntegratedNotifyToken();

        /// <summary>
        /// Get the currently loggedin User Info.
        /// </summary>
        /// <returns>User Model</returns>
        Task<UserWithAccountInfo> GetCurrentUser();

        /// <summary>
        /// Get the AccountID for the currently loggedin User
        /// </summary>
        /// <returns>Account ID</returns>
        Task<long> GetCurrentUserAccountID();
    }
}
