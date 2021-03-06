﻿using Coopers.BusinessLayer.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.APIClient
{
    public interface IPaymentHistoryClient
    {

        /// <summary>
        /// Create PaymentHistory record
        /// </summary>
        /// <param name="PaymentHistory">PaymentHistory model</param>
        /// <returns>ID of the created PaymentHistory</returns>
        Task<long> CreatePaymentHistory(PaymentHistory PaymentHistory);

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

        /// <summary>
        /// Get the online paymenthistory for a given account
        /// </summary>
        /// <param name="AccountID">unique identifier for the Account</param>
        /// <returns>List of payment histories</returns>
        Task<List<PaymentHistory>> GetPaymentHistoryList(long AccountID);

    }
}
