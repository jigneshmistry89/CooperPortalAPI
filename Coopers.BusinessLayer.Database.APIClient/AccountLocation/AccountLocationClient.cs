using Coopers.BusinessLayer.Database.APIClient.DTO;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using Coopers.BusinessLayer.Model.DTO;

namespace Coopers.BusinessLayer.Database.APIClient.Location
{
    public class AccountLocationClient : IAccountLocationClient
    {

        #region PRIVATE MEMBER

        private string AccountLocationEndPoint = ConfigurationManager.AppSettings["MicroServiceAPIEndpoint"] + "AccountLocation/";

        #endregion

        #region PUBLIC MEMBERS

        /// <summary>
        /// Create a accountLocation record
        /// </summary>
        /// <param name="AccountLocation">AccountLocation model</param>
        /// <returns>Id of the newly created AccountLocation</returns>
        public async Task<long> CreateAccountLocation(AccountLocation AccountLocation)
        {
            long res = 0;
            HttpResponseMessage response = await new HttpClient().PostAsJsonAsync(AccountLocationEndPoint, AccountLocation);
            if (response.IsSuccessStatusCode)
            {
                res = (await response.Content.ReadAsAsync<long>());
            }
            return await Task.FromResult(res);
        }

        /// <summary>
        /// Get the accountLocation details by AccountID
        /// </summary>
        /// <param name="AccountID">Unique indentofier for the Account</param>
        /// <returns>AccountLocation Model</returns>
        public async Task<AccountLocation> GetAccountLocationByID(long AccountID)
        {
            AccountLocation res = new AccountLocation();

            HttpResponseMessage response = await new HttpClient().GetAsync(AccountLocationEndPoint + "/" + AccountID);
            if (response.IsSuccessStatusCode)
            {
                res = (await response.Content.ReadAsAsync<AccountLocation>());
            }
            return await Task.FromResult(res);
        }

        /// <summary>
        /// Update a AccountLocation record
        /// </summary>
        /// <param name="AccountLocation">AccountLocation model</param>
        /// <returns>No of records Update</returns>
        public async Task<int> UpdateAccountLocation(AccountLocation AccountLocation)
        {
            int res = 0;
            HttpResponseMessage response = await new HttpClient().PutAsJsonAsync(AccountLocationEndPoint, AccountLocation);
            if (response.IsSuccessStatusCode)
            {
                res = (await response.Content.ReadAsAsync<int>());
            }
            return await Task.FromResult(res);
        }

        #endregion


    }

}
