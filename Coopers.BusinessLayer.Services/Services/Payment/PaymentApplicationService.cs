using System.Threading.Tasks;
using Coopers.BusinessLayer.Model.DTO;
using Stripe;
using Coopers.BusinessLayer.Database.APIClient;
using System;
using Newtonsoft.Json;
using Coopers.BusinessLayer.Utility;
using System.IO;
using Coopers.BusinessLayer.Localizer;
using System.Web;
using Coopers.BusinessLayer.Model.Interface;

namespace Coopers.BusinessLayer.Services.Services
{
    public class PaymentApplicationService : IPaymentApplicationService
    {

        #region PRIVATE MEMBERS

        private readonly ITaxableStateClient _taxableStateClient;
        private readonly IAccountApplicationService _accountApplicationService;
        private readonly ITranscationCacheApplicationService _transcationCacheApplicationService;
        private readonly IPaymentHistoryApplicationService _paymentHistoryApplicationService;
        private readonly IPDFExportApplicationService _pdfExportApplicationService;
        private readonly IHttpContextProvider _iHttpContextProvider;
        private readonly IEmailApplicationService _emailApplicationService;

        #endregion


        #region CONSTRUCTOR

        public PaymentApplicationService(IEmailApplicationService emailApplicationService,IPaymentHistoryApplicationService paymentHistoryApplicationService,IHttpContextProvider iHttpContextProvider, 
                                            IPDFExportApplicationService pdfExportApplicationService,ITaxableStateClient taxableStateClient,
                                            IAccountApplicationService accountApplicationService, ITranscationCacheApplicationService transcationCacheApplicationService)
        {
            _taxableStateClient = taxableStateClient;
            _accountApplicationService = accountApplicationService;
            _transcationCacheApplicationService = transcationCacheApplicationService;
            _pdfExportApplicationService = pdfExportApplicationService;
            _iHttpContextProvider = iHttpContextProvider;
            _paymentHistoryApplicationService = paymentHistoryApplicationService;
            _emailApplicationService = emailApplicationService;
        }

        #endregion


        #region PUBLIC MEMBERS     

        public async Task<Transaction> GetPaymentDetails(string ProductName)
        {
            var transcation = new Transaction(ProductName);

            switch (ProductName)
            {
                case AppConstant.NotifEye:
                    transcation.TransactionInfo = await GetNotifEyePaymentDetails();
                    _transcationCacheApplicationService.AddTransaction(transcation);
                    break;
                default:
                    transcation.TransactionInfo = null;
                    break;
            }

            return transcation;
        }

        public async Task<object> ExecutePayment(Payment Payment)
        {
            StripeCharge charge = new StripeCharge();
            StripeChargeCreateOptions chargeOption;
            long paymentHistoryID = 0;

            var transcation = _transcationCacheApplicationService.GetTransaction(Payment.TransactionID);

            try
            {
                chargeOption = CreateStripeChargeOption(transcation, Payment);

                if (chargeOption != null)
                {
                    var chargeService = new StripeChargeService();
                    charge = chargeService.Create(chargeOption);

                    paymentHistoryID = await _paymentHistoryApplicationService.CreatePaymentHistory(transcation, charge.Id);

                    if(paymentHistoryID != 0)
                    {
                        var retVal = await UpdateAccountSubscription(transcation);

                        if (retVal>0)
                        {
                            var user = await _iHttpContextProvider.GetCurrentUser();

                            //Get the template
                            var template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Payment Confirmation Email.html"));

                            //Replace the link.
                            template = template.Replace("{%PaymentReferenceNo%}", charge.Id);
                            template = template.Replace("{%UserName%}", user.UserFullName);
                            _emailApplicationService.SendAnEmail(template, user.Email, AppLocalizer.SubscriptionRenewed);


                            return new PaymentInfo
                            {
                                PaymentHistoryID = paymentHistoryID,
                                StripeChargeID = charge.Id,
                                Transaction = transcation
                            };
                        }
                        else if (retVal == -1)
                        {
                            throw new Exception(AppLocalizer.ERR_UPDATESUBSCRIPTION_RIGHTS);
                        }
                        else
                        {
                            throw new Exception(AppLocalizer.ERR_SUBSCRIPTION_UPDATE);
                        }    
                    }
                    else
                    {
                        throw new Exception(AppLocalizer.ERR_PAYMENTHISTORY_CREATE);
                    }
                }
                else
                {
                    throw new Exception(AppLocalizer.ERR_STRIPECHARGEOPTION_CREATE);
                }
            }
            catch (StripeException se)
            {
                throw new Exception(se.Message);
            }
            catch(Exception ex)
            {
                //Create refund for the charge.
                CreateStripeRefund(charge.Amount,charge.Id);

                //Remove Paymenthistory record if created.
                if(paymentHistoryID > 0)
                {
                    await _paymentHistoryApplicationService.DeletePaymentHistoryByID(paymentHistoryID);
                }

                throw;
            }
        }

        public async Task<byte[]> GenerateInvoice(long PatmentHistoryID)
        {
            string template = "";

            var paymentHistory = await _paymentHistoryApplicationService.GetPaymentHistoryByID(PatmentHistoryID);

            switch (paymentHistory.ProductName)
            {
                case AppConstant.NotifEye:
                    template = PrepareNotifEyeTemplate(paymentHistory);
                    break;

            }

           return _pdfExportApplicationService.GeneratePDF(template);
        }

        #endregion


        #region PRIVATE MEMBERS     

        private async Task<NotifEyeTransactionInfo> GetNotifEyePaymentDetails()
        {
            var notifEyeTransaction = new NotifEyeTransactionInfo();

            var user = await _iHttpContextProvider.GetCurrentUser();

            if (user != null && user.Account != null && user.Account.Count > 0)
            {
                AccountSensors accountSensor = new AccountSensors();
                accountSensor.AccountID = user.Account[0].AccountID;
                accountSensor.NumSensors = user.Account[0].NumSensors;
                notifEyeTransaction.AccountID = user.Account[0].AccountID;
                notifEyeTransaction.PrimaryUserName = user.Account[0].UserFullName;
                notifEyeTransaction.Address = AppUtility.PrepareAddress(user.Account[0].Address, user.Account[0].Address2, user.Account[0].State, user.Account[0].PostalCode, user.Account[0].Country);
                notifEyeTransaction.Email = user.Account[0].EmailAddress;
                notifEyeTransaction.AccountName = user.Account[0].CompanyName;
                notifEyeTransaction.CustomerID = user.Account[0].AccountID;
                notifEyeTransaction.CustomerName = user.UserName;
                notifEyeTransaction.NumberOfSensor = user.Account[0].NumSensors;
                notifEyeTransaction.OldRenewalDate = user.Account[0].SubscriptionExpiry;
                notifEyeTransaction.NewRenewalDate = user.Account[0].SubscriptionExpiry.AddDays(365);
                var stateTax = await _taxableStateClient.GetTaxableStatebyStateCode(user.Account[0].State);
                
                notifEyeTransaction.SubscriptionAmount = _accountApplicationService.GetSensorAmount(accountSensor) * 100;
                notifEyeTransaction.TaxString = stateTax != null ? stateTax.Tax.ToString() : "0";
                notifEyeTransaction.TaxAmount = await _accountApplicationService.GetTaxAmount(notifEyeTransaction.SubscriptionAmount, user.Account[0].PostalCode) ;
                notifEyeTransaction.TotalAmount  = await _accountApplicationService.GetSensorAnnualFee(accountSensor, user.Account[0].PostalCode) * 100;

            }
            return notifEyeTransaction;
        }

        private StripeChargeCreateOptions CreateStipeChargeOptionForNotify(Transaction transcation, Payment Payment)
        {
            NotifEyeTransactionInfo transInfo = (NotifEyeTransactionInfo)transcation.TransactionInfo;

            return new StripeChargeCreateOptions()
            {
                Amount = (int)transInfo.TotalAmount,
                Currency = "usd",
                Description = string.Format("Charge for {0} for Account {1} paid by {0}", transcation.ProductName, transInfo.AccountID, transInfo.CustomerID),
                SourceTokenOrExistingSourceId = Payment.StripeToken
            };
        }

        private StripeRefund CreateStripeRefund(long Amount, string ChargeID)
        {
            var refundOptions = new StripeRefundCreateOptions()
            {
                Amount = (int)Amount,
                Reason = StripeRefundReasons.Unknown
            };

            var refundService = new StripeRefundService();
            return refundService.Create(ChargeID, refundOptions);
        }

        private async Task<int> UpdateAccountSubscription(Transaction transcation)
        {
            var retVal = 0;

            switch (transcation.ProductName)
            {
                case AppConstant.NotifEye:
                    NotifEyeTransactionInfo transInfo = (NotifEyeTransactionInfo)transcation.TransactionInfo;
                    retVal = await _accountApplicationService.UpdateAccountSubscription(transInfo.AccountID, transInfo.NewRenewalDate);
                    break;
            }

            return retVal;
        }

        private StripeChargeCreateOptions CreateStripeChargeOption(Transaction transcation, Payment Payment)
        {
            if (transcation != null)
            {
                switch (transcation.ProductName)
                {
                    case AppConstant.NotifEye:
                        return CreateStipeChargeOptionForNotify(transcation, Payment);
                        break;
                    default:
                        return null;
                }
            }
            else
            {
                throw new Exception(AppLocalizer.ERR_TRANSACTION_EXPIRED);
            }
        }

        private string PrepareNotifEyeTemplate(PaymentHistory PaymentHistory)
        {
            NotifEyeTransactionInfo transInfo = JsonConvert.DeserializeObject<NotifEyeTransactionInfo>(PaymentHistory.Data);
            var template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Invoice.html"));
            template = template.Replace("{%CompanyName%}", transInfo.AccountName);
            template = template.Replace("{%CompanyAddress%}", transInfo.Address);
            template = template.Replace("{%Email%}", transInfo.Email);
            template = template.Replace("{%NoOfSensors%}", transInfo.NumberOfSensor.ToString());
            template = template.Replace("{%Amount%}", transInfo.TotalAmount.ToString());
            return template;
        }

        #endregion

    }

}
