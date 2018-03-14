﻿using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public interface IAuthenticationClient
    {
        /// <summary>
        /// Returns authorization token for username password to be used in all other api methods.
        /// </summary>
        /// <param name="UserName">Username you use to access iMonnit</param>
        /// <param name="Password">Password you use to access iMonnit</param>
        /// <returns>Token</returns>
        Task<string> GetAuthToken(string UserName,string Password);


        /// <summary>
        /// Check if the given token valid or not.
        /// </summary>
        /// <param name="Token">Token to be validated</param>
        /// <returns>True/False</returns>
        Task<bool> IsValidToken(string Token);

        /// <summary>
        /// Check authorization token by calling this method
        /// </summary>
        /// <param name="Token">User authetication token</param>
        /// <returns>Success/Failure</returns>
        Task<string> Logon(string Token);


        /// <summary>
        /// Get the legacy notifeye from the integrated API using the NotifEye UserName
        /// </summary>
        /// <param name="UserName">NotifEye UserName</param>
        /// <returns>NotififEye Auth token</returns>
        Task<string> GetLegacyNotifEyeToken(string UserName);

        /// <summary>
        /// Get the Authentication token for the Dashboard User.
        /// </summary>
        /// <param name="UserName">Dashboard UserName</param>
        /// <param name="Password">Password</param>
        /// <returns>Authentication token</returns>
        Task<string> GetDashboardAuthToken(string UserName, string Password);

    }
}
