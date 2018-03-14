using Coopers.BusinessLayer.Database.APIClient.DTO;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Coopers.BusinessLayer.Model.DTO;
using Newtonsoft.Json;

namespace Coopers.BusinessLayer.Database.APIClient
{
    public class PaymentHistoryClient : IPaymentHistoryClient
    {

        #region PRIVATE MEMBER

        private string PaymentHistoryEndPoint = ConfigurationManager.AppSettings["MicroServiceAPIEndpoint"] + "PaymentHistory/";

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

            HttpResponseMessage response = await new HttpClient().PostAsJsonAsync(PaymentHistoryEndPoint, PaymentHistory);
            if (response.IsSuccessStatusCode)
            {
                paymentHistoryID = (await response.Content.ReadAsAsync<long>());
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
            int retVal = 0;
            string query = string.Format("?PaymentHistoryID={0}", PaymentHistoryID);
            HttpResponseMessage response = await new HttpClient().DeleteAsync(PaymentHistoryEndPoint.TrimEnd('/')+ query);

            if (response.IsSuccessStatusCode)
            {
                retVal = (await response.Content.ReadAsAsync<int>());
            }

            return await Task.FromResult(retVal);
        }

        /// <summary>
        /// Get the paymenthistory by ID
        /// </summary>
        /// <param name="PaymentHistoryID">Unique identifier for the PaymentHistory</param>
        /// <returns>PaymentHistory Model</returns>
        public async Task<PaymentHistory> GetPaymentHistoryByID(long PaymentHistoryID)
        {
            HttpResponseMessage response = await new HttpClient().GetAsync(PaymentHistoryEndPoint + PaymentHistoryID);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadAsAsync<PaymentHistory>());
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<List<PaymentHistory>> GetPaymentHistoryList(int Offset, int PageSize,long AccountID)
        {
            var QueryParam = string.Format("?Offset={0}&PageSize={1}&AccountID={2}", Offset, PageSize, AccountID);
            HttpResponseMessage response = await new HttpClient().GetAsync(PaymentHistoryEndPoint + "List" + QueryParam);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadAsAsync< List<PaymentHistory>>());
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }


        #endregion

    }

}
