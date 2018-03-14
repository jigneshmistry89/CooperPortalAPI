using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public interface IAccountApplicationService
    {

        Task<AccountDetails> GetAccountByID(long AccountID);

        Task<List<UserInfo>> GetAccountUserList(long AccountID);

        Task<AccountDetails> GetAccountByCustomerID(long CustomerID);

        /// <summary>
        /// Get the Account summary
        /// </summary>
        /// <returns></returns>
        Task<AccountSummary> GetAccountSummary();

        Task<AccountResource> GetAccountResources(long AccountID);

        /// <summary>
        /// Get the customers list
        /// </summary>
        /// <returns>List of customer summary model</returns>
        Task<List<Customer>> GetCustomers();

        /// <summary>
        /// Get the customer status summary
        /// </summary>
        /// <returns>status summary model</returns>
        Task<List<CustomerStatus>> GetCustomerStatusSummary();

        Task<double> GetSensorAnnualFee(AccountSensors AccountSensors,string PostalCode);

        double GetSensorAmount(AccountSensors AccountSensors);

        Task<double> GetTaxAmount(double Amount, string PostanCode);

        /// <summary>
        /// Update the Account ExpirationDate for a given Account
        /// </summary>
        /// <param name="AccountID">unique identofication for the Account</param>
        /// <param name="ExpirationDate">New Expiration Date for the Account</param>
        /// <returns>Record Updated Count</returns>
        Task<int> UpdateAccountSubscription(long AccountID, DateTime ExpirationDate);

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

        ///// <summary>
        ///// Creates a new account
        ///// </summary>
        ///// <param name="Account">Account info</param>
        ///// <returns>Success info/Error List </returns>
        //Task<object> CreateAccount(Account Account);

    }
}
