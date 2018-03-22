using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Coopers.BusinessLayer.Model.DTO;

namespace Coopers.BusinessLayer.Database.APIClient.Location
{
    public class AccountLocationClient : IAccountLocationClient
    {

        #region PRIVATE MEMBER

        private readonly string AccountLocationEndPoint = "AccountLocation/";
        private readonly IHttpService _httpService;

        #endregion


        #region CONSTRUCTOR

        public AccountLocationClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion


        #region PUBLIC MEMBERS

        /// <summary>
        /// Create a accountLocation record
        /// </summary>
        /// <param name="AccountLocation">AccountLocation model</param>
        /// <returns>Id of the newly created AccountLocation</returns>
        public async Task<long> CreateAccountLocation(AccountLocation AccountLocation)
        {
            return await _httpService.PostAsAsync<long>(AccountLocationEndPoint, AccountLocation);
        }

        /// <summary>
        /// Get the accountLocation details by AccountID
        /// </summary>
        /// <param name="AccountID">Unique indentofier for the Account</param>
        /// <returns>AccountLocation Model</returns>
        public async Task<AccountLocation> GetAccountLocationByID(long AccountID)
        {
            return await _httpService.GetAsAsync<AccountLocation>(AccountLocationEndPoint + AccountID, "");
        }

        /// <summary>
        /// Update a AccountLocation record
        /// </summary>
        /// <param name="AccountLocation">AccountLocation model</param>
        /// <returns>No of records Update</returns>
        public async Task<int> UpdateAccountLocation(AccountLocation AccountLocation)
        {
            return await _httpService.PutAsAsync<int>(AccountLocationEndPoint, AccountLocation);
        }

        #endregion


    }

}
