using Coopers.BusinessLayer.NotifEye.APIClient.Models;
using Coopers.BusinessLayer.Utility;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public class AuthenticationClient : IAuthenticationClient
    {
        #region PRIVATE MEMBERS


        #endregion

        #region CONSTRUCTOR

        #endregion

        #region PUBLIC MEMBERS 


        #region NOTIFEYEAPI


        /// <summary>
        /// Returns authorization token for username password to be used in all other api methods.
        /// </summary>
        /// <param name="UserName">Username you use to access iMonnit</param>
        /// <param name="Password">Password you use to access iMonnit</param>
        /// <returns>Token</returns>
        public async Task<string> GetAuthToken(string UserName, string Password)
        {
            string res = "";
            string path = string.Format("{0}GetAuthToken?username={1}&password={2}", ConfigurationManager.AppSettings["NotifEyeAPIEndpoint"], UserName, Password);

            HttpResponseMessage response = await new HttpClient().GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                res = (await response.Content.ReadAsAsync<APIResponse<string>>()).Result;
            }

            return await Task.FromResult(res);
        }

        /// <summary>
        /// Check authorization token by calling this method
        /// </summary>
        /// <param name="Token">User authetication token</param>
        /// <returns>Success/Failure</returns>
        public async Task<string> Logon(string Token)
        {
            string res = "";
            string path = string.Format("{0}Logon/{1}", ConfigurationManager.AppSettings["NotifEyeAPIEndpoint"], Token);

            HttpResponseMessage response = await new HttpClient().GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                res = (await response.Content.ReadAsAsync<APIResponse<string>>()).Result;
            }

            return await Task.FromResult(res);
        }

        #endregion


        #region INTEGRATED API

        /// <summary>
        /// Get the Authentication token for the Dashboard User.
        /// </summary>
        /// <param name="UserName">Dashboard UserName</param>
        /// <param name="Password">Password</param>
        /// <returns>Authentication token</returns>
        public async Task<string> GetDashboardAuthToken(string UserName, string Password)
        {
            string token = "";

            //prepare notify endpoint
            string path = string.Format("{0}Authentication/NotifEye", ConfigurationManager.AppSettings["NotifEyeIntegratedAPIEndpoint"]);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", AppUtility.Base64Encode(UserName + ":" + Password));

            //make api call
            HttpResponseMessage response = await httpClient.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<string> values;
                if (response.Headers.TryGetValues("authenticatedtoken", out values))
                {
                    token = values.First();
                }
            }
            return token;
        }

        /// <summary>
        /// Get the legacy notifeye from the integrated API using the NotifEye UserName
        /// </summary>
        /// <param name="UserName">NotifEye UserName</param>
        /// <returns>NotififEye Auth token</returns>
        public async Task<string> GetLegacyNotifEyeToken(string UserName)
        {
            string res = "";

            string path = string.Format("{0}Authentication/GetLegacyNotifEyeToken?{1}", ConfigurationManager.AppSettings["NotifEyeIntegratedAPIEndpoint"], string.Format("UserName={0}", UserName));

            HttpResponseMessage response = await new HttpClient().GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                res = (await response.Content.ReadAsAsync<string>());
            }

            return await Task.FromResult(res);
        }

        /// <summary>
        /// Check if the given token valid or not.
        /// </summary>
        /// <param name="Token">Token to be validated</param>
        /// <returns>True/False</returns>
        public async Task<bool> IsValidToken(string Token)
        {
            bool res = false;

            string path = string.Format("{0}Authentication/IsValidToken", ConfigurationManager.AppSettings["NotifEyeIntegratedAPIEndpoint"]);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("authenticatedtoken", Token);

            HttpResponseMessage response = await httpClient.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                res = (await response.Content.ReadAsAsync<bool>());
            }

            return await Task.FromResult(res);
        }

        #endregion

        #endregion


    }
}
