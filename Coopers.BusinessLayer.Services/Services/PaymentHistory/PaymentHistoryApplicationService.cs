using System.Threading.Tasks;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Database.APIClient;
using System;
using Newtonsoft.Json;
using Coopers.BusinessLayer.Utility;
using System.Collections.Generic;
using Coopers.BusinessLayer.Model.Interface;
using AutoMapper;
using System.Web;
using System.Linq;
using Coopers.BusinessLayer.NotifEye.APIClient;

namespace Coopers.BusinessLayer.Services.Services
{
    public class PaymentHistoryApplicationService : IPaymentHistoryApplicationService
    {

        #region PRIVATE MEMBERS
       
        private readonly IPaymentHistoryClient _paymentHistoryClient;
        private readonly IHttpContextProvider _iHttpContextProvider;
        private readonly IMapper _mapper;
        private readonly IAccountClient _accountClient;

        #endregion


        #region CONSTRUCTOR

        public PaymentHistoryApplicationService(IAccountClient accountClient, IHttpContextProvider iHttpContextProvider, IPaymentHistoryClient paymentHistoryClient, IMapper mapper)
        {
            _paymentHistoryClient = paymentHistoryClient;
            _iHttpContextProvider = iHttpContextProvider;
            _mapper = mapper;
            _accountClient = accountClient;
        }

        #endregion


        #region PUBLIC MEMBERS     

        /// <summary>
        /// Get the paged PaymentHistory records for the current account 
        /// </summary>
        /// <returns>PaymetnHistory List</returns>
        public async Task<List<PaymentHistoryInfo>> GetPaymentHistoryList()
        {
            //Get the accountID of the currently loggedin user.
            var accID = await _iHttpContextProvider.GetCurrentUserAccountID();

            //Get the online paymentHistory.
            List<PaymentHistoryInfo> retVal = await PrepareNotifEyePaymentHistory(accID);

            //Get the Manual Payment History.
            retVal.AddRange(await PrepareManualPaymentHistory(accID, retVal));

            //Retrun the result.
            return retVal;
        }

        /// <summary>
        /// Delete the PaymentHistory by ID
        /// </summary>
        /// <param name="PaymentHistoryID">Unique identifier for the PaymentHistory</param>
        /// <returns>No of records updated</returns>
        public async Task<int> DeletePaymentHistoryByID(long PaymentHistoryID)
        {
            return await _paymentHistoryClient.DeletePaymentHistoryByID(PaymentHistoryID);
        }

        /// <summary>
        /// Delete the PaymentHistory by ID
        /// </summary>
        /// <param name="PaymentHistoryID">Unique identifier for the PaymentHistory</param>
        /// <returns>No of records updated</returns>
        public async Task<PaymentHistory> GetPaymentHistoryByID(long PaymentHistoryID)
        {
            return await _paymentHistoryClient.GetPaymentHistoryByID(PaymentHistoryID);
        }

        /// <summary>
        /// Create PaymentHistory record
        /// </summary>
        /// <param name="PaymentHistory">PaymentHistory model</param>
        /// <returns>ID of the created PaymentHistory</returns>
        public async Task<long> CreatePaymentHistory(Transaction transcation, string ChargeID)
        {
            var paymentHistory = new PaymentHistory();

            switch (transcation.ProductName)
            {
                case AppConstant.NotifEye:
                    paymentHistory = PrepareNotifyPaymentHistory(transcation);
                    break;
            }

            paymentHistory.HistoryDate = DateTime.UtcNow;
            paymentHistory.StripeChargeID = ChargeID;
            return await _paymentHistoryClient.CreatePaymentHistory(paymentHistory);
        }

        #endregion


        #region PRIVATE MEMBERS     

        private PaymentHistory PrepareNotifyPaymentHistory(Transaction transcation)
        {
            var paymentHistory = new PaymentHistory();

            NotifEyeTransactionInfo transInfo = (NotifEyeTransactionInfo)transcation.TransactionInfo;
           
            paymentHistory.AccountID = transInfo.AccountID;
            paymentHistory.CustomerID = transInfo.CustomerID;
            paymentHistory.ProductName = AppConstant.NotifEye;
            paymentHistory.Data = JsonConvert.SerializeObject(transInfo);
            return paymentHistory;
        }

        private PaymentHistoryInfo PrepareNotifEyePaymentHistoryInfo(PaymentHistory paymentHistory)
        {
            var notTransactionInfo = JsonConvert.DeserializeObject<NotifEyeTransactionInfo>(paymentHistory.Data);
            var retVal = _mapper.Map<PaymentHistoryInfo>(notTransactionInfo);
            retVal.TotalAmount = retVal.TotalAmount / 100;
            retVal.TaxAmount = retVal.TaxAmount / 100;
            retVal.SubscriptionAmount = retVal.SubscriptionAmount / 100;
            retVal.ID = paymentHistory.ID;
            retVal.StripeChargeID = paymentHistory.StripeChargeID;
            retVal.ProductName = paymentHistory.ProductName;
            retVal.HistoryDate = paymentHistory.HistoryDate.ToString("yyyy-MM-dd");
            retVal.InvoiceDownloadLink = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/api/payment/Invoice?PaymentHistoryID=" + paymentHistory.ID;
            return retVal;
        }

        private async Task<List<PaymentHistoryInfo>> PrepareNotifEyePaymentHistory(long AccountID)
        {
            List<PaymentHistoryInfo> retVal = new List<PaymentHistoryInfo>();
            var paymentHistoryInfo = new PaymentHistoryInfo();

            //Get the online payment histories
            var paymentHistories = await _paymentHistoryClient.GetPaymentHistoryList(AccountID);

            foreach (var history in paymentHistories)
            {
                switch (history.ProductName)
                {
                    case AppConstant.NotifEye:
                        paymentHistoryInfo = PrepareNotifEyePaymentHistoryInfo(history);
                        break;
                }

                retVal.Add(paymentHistoryInfo);
            }

            return retVal;
        }

        private async Task<List<PaymentHistoryInfo>> PrepareManualPaymentHistory(long AccountID,List<PaymentHistoryInfo> OnlinePaymentHistory)
        {
            List<PaymentHistoryInfo> retVal = new List<PaymentHistoryInfo>();

            //Get the manual paymenthistories
            var manualPaymentHistories = await _accountClient.GetAccountSubscriptionHistory(AccountID);

            foreach (var paymentHistory in manualPaymentHistories)
            {
                if (!OnlinePaymentHistory.Any(history=> history.HistoryDate == paymentHistory.ChangeDate.ToString("yyyy-MM-dd")))
                {
                    retVal.Add(_mapper.Map<PaymentHistoryInfo>(paymentHistory));
                }
            }

            return retVal;
        }

        #endregion

    }
}
