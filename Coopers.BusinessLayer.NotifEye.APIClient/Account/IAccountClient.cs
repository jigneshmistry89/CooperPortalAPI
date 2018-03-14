using Coopers.BusinessLayer.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public interface IAccountClient
    {
        ///// <summary>
        ///// Creates a new account
        ///// </summary>
        ///// <param name="Account">Account info</param>
        ///// <returns>Success info/Error List </returns>
        //Task<object> CreateAccount(Account Account);

        Task<List<Account>> GetAccountList();

        /// <summary>
        /// Get the users list for a given account
        /// </summary>
        /// <param name="AccountID">Unique identofier for the account</param>
        /// <returns>List of Users</returns>
        Task<List<UserInfo>> GetAccountUserList(long AccountID);

        Task<List<UserInfo>> GetAccountUserListForUser(long AccountID, string UserName);

        Task<List<AccountSensors>> GetNumSensorByAccount();

        Task<AccountDetails> GetAccountByID(long AccountID);

        Task<AccountDetails> GetAccountByCustomerID(long CustomerID);

        /// <summary>
        /// Update the Account ExpirationDate for a given Account
        /// </summary>
        /// <param name="AccountID">unique identofication for the Account</param>
        /// <param name="ExpirationDate">New Expiration Date for the Account</param>
        /// <returns>Record Updated Count</returns>
        Task<int> UpdateAccountSubscription(long AccountID, string ExpirationDate);

        /// <summary>
        /// Get the Manual PaymentHistory for the Account
        /// </summary>
        /// <param name="AccountID">Unique identofier for the Account</param>
        /// <returns>List of PaymentHistories</returns>
        Task<List<ManualPaymentHistory>> GetAccountSubscriptionHistory(long AccountID);

        /// <summary>
        /// Update the Account
        /// </summary>
        /// <param name="Account">Account Model</param>
        /// <returns>Updated record count</returns>
        Task<int> UpdateAccount(UpdateAccount Account);

    }
}
