using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.HttpService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public class AccountClient : IAccountClient
    {

        #region PRIVATE MEMBERS
  
        private readonly IHttpService _httpService;

        #endregion


        #region CONSTRUCTOR

        public AccountClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion


        //public async Task<object> CreateAccount( Acc Account Account)
        //{
        //    var query = HttpUtility.ParseQueryString(string.Empty);
        //    query["CompanyName"] = Account.CompanyName;
        //    query["TimeZoneID"] = Account.TimeZoneID.ToString();
        //    query["Address"] = Account.Address;
        //    query["Address2"] = Account.Address2;
        //    query["City"] = Account.City;
        //    query["State"] = Account.State;
        //    query["PostalCode"] = Account.PostalCode;
        //    query["Country"] = Account.Country;
        //    //query["ResellerID"] = Account.ResellerID.ToString();
        //    query["UserName"] = Account.UserName;
        //    query["Password"] = Account.Password;
        //    query["ConfirmPassword"] = Account.ConfirmPassword;
        //    query["FirstName"] = Account.FirstName;
        //    query["LastName"] = Account.LastName;
        //    query["NotificationEmail"] = Account.NotificationEmail;
        //    //query["NotificationPhone"] = Account.NotificationPhone;
        //    //query["SMSCarrierID"] = Account.SMSCarrierID.ToString();
        //    string path = string.Format("{0}CreateAccount/{1}", ConfigurationManager.AppSettings["NotifEyeAPIEndpoint"], query.ToString());
        //    var response =  await new HttpClient().GetAsync(path);
        //    object retVal = null;
        //    if(response.IsSuccessStatusCode)
        //    {
        //        retVal = (await response.Content.ReadAsAsync<APIResponse<GatewayDTO>>()).Result;
        //    }
        //    return Task.FromResult(retVal);
        //}

        #region NOTIFEYEAPI

        /// <summary>
        /// Get the users list for a given account
        /// </summary>
        /// <param name="AccountID">Unique identofier for the account</param>
        /// <returns>List of Users</returns>
        public async Task<List<UserInfo>> GetAccountUserList(long AccountID)
        {
            return await _httpService.GetAsAsync<List<UserInfo>>("AccountUserList", string.Format("AccountID={0}", AccountID), false);
        }

        public async Task<List<UserInfo>> GetAccountUserListForUser(long AccountID,string UserName)
        {
            return await _httpService.GetWithUserAsync<List<UserInfo>>(UserName,"AccountUserList", string.Format("AccountID={0}", AccountID), false);
        }

        #endregion


        #region INTEGRATED API

        public async Task<List<Account>> GetAccountList()
        {
            return await _httpService.GetAsAsync<List<Account>>("account/GetAccountList", "", true);
        }

        public async Task<AccountDetails> GetAccountByID(long AccountID)
        {
            return await _httpService.GetAsAsync<AccountDetails>("account/GetAccountByID", string.Format("accountID={0}", AccountID), true);
        }

        public async Task<AccountDetails> GetAccountByCustomerID(long CustomerID)
        {
            return await _httpService.GetAsAsync<AccountDetails>("account/GetAccountByCustomerID", string.Format("customerid={0}", CustomerID), true);
        }

        public async Task<List<AccountSensors>> GetNumSensorByAccount()
        {
            return await _httpService.GetAsAsync<List<AccountSensors>>("account/GetNumSensorsByAccount", "", true);
        }

        /// <summary>
        /// Update the Account ExpirationDate for a given Account
        /// </summary>
        /// <param name="AccountID">unique identofication for the Account</param>
        /// <param name="ExpirationDate">New Expiration Date for the Account</param>
        /// <returns>Record Updated Count</returns>
        public async Task<int> UpdateAccountSubscription(long AccountID,string ExpirationDate)
        {
            var Uri = "account/UpdateAccountSubscription?" + string.Format("AccountID={0}&ExpirationDate={1}", AccountID, ExpirationDate);
            return await _httpService.PutAsAsync<int>(Uri, true);
        }

        /// <summary>
        /// Get the Manual PaymentHistory for the Account
        /// </summary>
        /// <param name="AccountID">Unique identofier for the Account</param>
        /// <returns>List of PaymentHistories</returns>
        public async Task<List<ManualPaymentHistory>> GetAccountSubscriptionHistory(long AccountID)
        {
            return await _httpService.GetAsAsync<List<ManualPaymentHistory>>("account/GetAccountSubscriptionHistory", string.Format("AccountID={0}", AccountID), true, false);
        }

        /// <summary>
        /// Update the Account
        /// </summary>
        /// <param name="Account">Account Model</param>
        /// <returns>Updated record count</returns>
        public async Task<int> UpdateAccount(UpdateAccount Account)
        {
            var Uri = "account/UpdateAccount?" + string.Format("AccountID={0}", Account.AccountID);
            return await _httpService.PutAsAsync<int>(Uri, Account);
        }

        #endregion


        #region PRIVATE MEMBERS    

        #endregion


    }
}
