using Coopers.BusinessLayer.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public interface IPaymentHistoryApplicationService
    {
        /// <summary>
        /// Get the paged PaymentHistory records for the current account 
        /// </summary>
        /// <param name="Offset">no of records to skip</param>
        /// <param name="PageSize">no of record to fetch</param>
        /// <returns>PaymetnHistory List</returns>
        Task<List<PaymentHistoryInfo>> GetPaymentHistoryList(int Offset, int PageSize);

        /// <summary>
        /// Create PaymentHistory record
        /// </summary>
        /// <param name="PaymentHistory">PaymentHistory model</param>
        /// <returns>ID of the created PaymentHistory</returns>
        Task<long> CreatePaymentHistory(Transaction transcation, string ChargeID);

        /// <summary>
        /// Delete the PaymentHistory by ID
        /// </summary>
        /// <param name="PaymentHistoryID">Unique identifier for the PaymentHistory</param>
        /// <returns>No of records updated</returns>
        Task<int> DeletePaymentHistoryByID(long PaymentHistoryID);

        /// <summary>
        /// Get the paymenthistory by ID
        /// </summary>
        /// <param name="PaymentHistoryID">Unique identifier for the PaymentHistory</param>
        /// <returns>PaymentHistory Model</returns>
        Task<PaymentHistory> GetPaymentHistoryByID(long PaymentHistoryID);
    }
}
