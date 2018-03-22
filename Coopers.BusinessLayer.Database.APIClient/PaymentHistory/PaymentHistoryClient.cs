using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Coopers.BusinessLayer.Model.DTO;

namespace Coopers.BusinessLayer.Database.APIClient
{
    public class PaymentHistoryClient : IPaymentHistoryClient
    {


        #region PRIVATE MEMBER

        private string PaymentHistoryEndPoint = "PaymentHistory/";
        private readonly IHttpService _httpService;

        #endregion


        #region CONSTRUCTOR

        public PaymentHistoryClient(IHttpService httpService)
        {
            _httpService = httpService;
        }


        #endregion


        #region PUBLIC MEMBERS

        /// <summary>
        /// Create PaymentHistory record
        /// </summary>
        /// <param name="PaymentHistory">PaymentHistory model</param>
        /// <returns>ID of the created PaymentHistory</returns>
        public async Task<long> CreatePaymentHistory(PaymentHistory PaymentHistory)
        {
            long paymentHistoryID = 0;

            try
            {
                paymentHistoryID = await _httpService.PostAsAsync<long>(PaymentHistoryEndPoint, PaymentHistory);
            }
            catch (Exception)
            {
                //Do Nothing..
            }

            return await Task.FromResult(paymentHistoryID);
        }

        /// <summary>
        /// Delete the PaymentHistory by ID
        /// </summary>
        /// <param name="PaymentHistoryID">Unique identifier for the PaymentHistory</param>
        /// <returns>No of records updated</returns>
        public async Task<int> DeletePaymentHistoryByID(long PaymentHistoryID)
        {
            return await _httpService.DeleteAsAsync<int>(PaymentHistoryEndPoint, string.Format("PaymentHistoryID={0}", PaymentHistoryID));
        }

        /// <summary>
        /// Get the paymenthistory by ID
        /// </summary>
        /// <param name="PaymentHistoryID">Unique identifier for the PaymentHistory</param>
        /// <returns>PaymentHistory Model</returns>
        public async Task<PaymentHistory> GetPaymentHistoryByID(long PaymentHistoryID)
        {
            return await _httpService.GetAsAsync<PaymentHistory>(PaymentHistoryEndPoint + PaymentHistoryID,"");
        }

        /// <summary>
        /// Get the online paymenthistory for a given account
        /// </summary>
        /// <param name="AccountID">unique identifier for the Account</param>
        /// <returns>List of payment histories</returns>
        public async Task<List<PaymentHistory>> GetPaymentHistoryList(long AccountID)
        {
            return await _httpService.GetAsAsync<List<PaymentHistory>>(PaymentHistoryEndPoint + "List", string.Format("AccountID={0}", AccountID));
        }


        #endregion


    }

}
