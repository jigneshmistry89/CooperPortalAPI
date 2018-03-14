using Coopers.BusinessLayer.NotifEye.APIClient;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public class AuthenticationApplicationService : IAuthenticationApplicationService
    {


        #region PRIVATE MEMBERS

        private readonly IAuthenticationClient _authenticationClient;

        #endregion


        #region CONSTRUCTOR

        public AuthenticationApplicationService(IAuthenticationClient authenticationClient)
        {
            _authenticationClient = authenticationClient;
        }

        #endregion


        #region PUBLIC MEMBERS     

        /// <summary>
        /// Returns authorization token for username password to be used in all other api methods.
        /// </summary>
        /// <param name="UserName">Username you use to access iMonnit</param>
        /// <param name="Password">Password you use to access iMonnit</param>
        /// <returns>Token</returns>
        public async Task<string> GetAuthToken(string UserName, string Password)
        {
            return await _authenticationClient.GetAuthToken(UserName, Password);
        }

        /// <summary>
        /// Check authorization token by calling this method
        /// </summary>
        /// <param name="Token">User authetication token</param>
        /// <returns>Success/Failure</returns>
        public async Task<string> Logon(string Token)
        {
            return await _authenticationClient.Logon(Token);
        }

        /// <summary>
        /// Check if the given token valid or not.
        /// </summary>
        /// <param name="Token">Token to be validated</param>
        /// <returns>True/False</returns>
        public async Task<bool> IsValidToken(string Token)
        {
            return await _authenticationClient.IsValidToken(Token);
        }

        /// <summary>
        /// Get the Authentication token for the Dashboard User.
        /// </summary>
        /// <param name="UserName">Dashboard UserName</param>
        /// <param name="Password">Password</param>
        /// <returns>Authentication token</returns>
        public async Task<string> GetDashboardAuthToken(string UserName, string Password)
        {
            return await _authenticationClient.GetDashboardAuthToken(UserName, Password);
        }

        #endregion


        #region PRIVATE MEMBERS     


        #endregion


    }
}
