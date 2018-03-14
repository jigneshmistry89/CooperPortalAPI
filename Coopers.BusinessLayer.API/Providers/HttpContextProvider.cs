using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Model.Interface;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;
using System.Linq;

namespace Coopers.BusinessLayer.API.Providers
{
    public class HttpContextProvider : IHttpContextProvider
    {

        #region PRIVATE MEMBERS

        private HttpContextBase _context;

        private UserWithAccountInfo _currentUSer;

        #endregion


        #region CONSTRUCTOR

        public HttpContextProvider(HttpContextBase context)
        {
            this._context = context;
        }

        #endregion


        #region PUBLIC MEMBERS   

        /// <summary>
        /// Get the token passed by the user.
        /// </summary>
        /// <returns>Token</returns>
        public string GetToken()
        {
            return _context.Request.Headers["Authorization"] != null ? _context.Request.Headers["Authorization"].Remove(0, 5).Trim() : "";
        }

        /// <summary>
        /// Get the registration token
        /// </summary>
        /// <returns>Token</returns>
        public string GetRegistrationToken()
        {
            return _context.Request.Headers["RegistrationToken"] != null ? _context.Request.Headers["RegistrationToken"] : "";
        }

        /// <summary>
        /// Get the Authentication token for the Integrated Notfiy API
        /// </summary>
        /// <returns>Athentication Token</returns>
        public string GetIntegratedNotifyToken()
        {
            return _context.Request.Headers["Authorization"] != null ? _context.Request.Headers["Authorization"].Remove(0, 5).Trim() : "";
        }

        /// <summary>
        /// Get the currently loggedin User Info.
        /// </summary>
        /// <returns>User Model</returns>
        public async Task<UserWithAccountInfo> GetCurrentUser()
        {
            var userDetail = new UserDetail();

            if(_currentUSer != null)
            {
                return _currentUSer;
            }
            else
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("authenticatedtoken", GetIntegratedNotifyToken());

                string path = string.Format("{0}{1}", ConfigurationManager.AppSettings["NotifEyeIntegratedAPIEndpoint"], "User/GetUserInfo");

                HttpResponseMessage response = await httpClient.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    userDetail = (await response.Content.ReadAsAsync<UserDetail>());
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }


                if (userDetail != null && userDetail.User != null)
                {
                    return userDetail.User;
                }
            }
            return null;
        }

        /// <summary>
        /// Get the AccountID for the currently loggedin User
        /// </summary>
        /// <returns>Account ID</returns>
        public async Task<long> GetCurrentUserAccountID()
        {
            var curUser = await GetCurrentUser();
            if(curUser != null && curUser.Account != null && curUser.Account.Count > 0)
            {
                return curUser.Account[0].AccountID;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}