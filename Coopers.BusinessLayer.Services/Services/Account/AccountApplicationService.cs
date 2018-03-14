using Coopers.BusinessLayer.NotifEye.APIClient;
using System.Threading.Tasks;
using Coopers.BusinessLayer.Model.DTO;
using System.Linq;
using System.Collections.Generic;
using System;
using Coopers.BusinessLayer.Localizer;
using AutoMapper;
using Coopers.BusinessLayer.Utility;
using Coopers.BusinessLayer.Database.APIClient;

namespace Coopers.BusinessLayer.Services.Services
{
    public class AccountApplicationService : IAccountApplicationService
    {


        #region PRIVATE MEMBERS

        private readonly IAccountClient _accountClient;
        private readonly ITaxableStateClient _taxableStateClient;
        private readonly INetworkClient _networkClient;
        private readonly ISensorClient _sensorClient;
        private readonly IGatewayClient _gatewayClient;
        private readonly IMapper _mapper;

        #endregion


        #region CONSTRUCTOR

        public AccountApplicationService(ITaxableStateClient taxableStateClient, IAccountClient accountClient, ISensorClient sensorClient, IGatewayClient gatewayClient, INetworkClient networkClient,IMapper mapper)
        {
            _sensorClient = sensorClient;
            _taxableStateClient = taxableStateClient;
            _accountClient = accountClient;
            _networkClient = networkClient;
            _gatewayClient = gatewayClient;
            _mapper = mapper;
        }

        #endregion


        #region PUBLIC MEMBERS     

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="Account">Account model</param>
        /// <returns>Success info/Error List </returns>
        //public async Task<object> CreateAccount(NotifEye.APIClient.DTO.Account Account)
        //{
        //    return await _accountClient.CreateAccount(Account);
        //}

        public async Task<AccountDetails> GetAccountByID(long AccountID)
        {
            return await _accountClient.GetAccountByID(AccountID);
        }

        public async Task<AccountDetails> GetAccountByCustomerID(long CustomerID)
        {
            return await _accountClient.GetAccountByCustomerID(CustomerID);
        }

        /// <summary>
        /// Get the Account summary
        /// </summary>
        /// <returns></returns>
        public async Task<AccountSummary> GetAccountSummary()
        {
            AccountSummary accountSummary = new AccountSummary();

            var accountList = await _accountClient.GetAccountList();
            var numSensorByAccs = await _accountClient.GetNumSensorByAccount();

            accountSummary.Status = await CalculateCustomerStatusSummary(accountList, numSensorByAccs);
            accountSummary.Customers = await PrepareCuctomersSummary(accountList, numSensorByAccs);

            return accountSummary;
        }

        /// <summary>
        /// Get the users list for a given account
        /// </summary>
        /// <param name="AccountID">Unique identofier for the account</param>
        /// <returns>List of Users</returns>
        public async Task<List<UserInfo>> GetAccountUserList(long AccountID)
        {
            return await _accountClient.GetAccountUserList(AccountID);
        }

        /// <summary>
        /// Get the customer status summary
        /// </summary>
        /// <returns>status summary model</returns>
        public async Task<List<CustomerStatus>> GetCustomerStatusSummary()
        {
            var accountList = await _accountClient.GetAccountList();
            var numSensorByAccs = await _accountClient.GetNumSensorByAccount();

            return await CalculateCustomerStatusSummary(accountList, numSensorByAccs);
        }

        /// <summary>
        /// Get the customers list
        /// </summary>
        /// <returns>List of customer summary model</returns>
        public async Task<List<Customer>> GetCustomers()
        {
            var accountList = await _accountClient.GetAccountList();
            var numSensorByAccs = await _accountClient.GetNumSensorByAccount();

            return await PrepareCuctomersSummary(accountList, numSensorByAccs);
        }

        public async Task<AccountResource> GetAccountResources(long AccountID)
        {
            AccountResource AccountResource = new AccountResource();

            var AccountDetails = await _accountClient.GetAccountByID(AccountID);

            if (AccountDetails != null && AccountDetails.AccountData != null && AccountDetails.AccountData.Count > 0)
            {
                AccountResource.Users = await _accountClient.GetAccountUserListForUser(AccountID, AccountDetails.AccountData[0].UserName);

                AccountResource.Networks = await _networkClient.GetNetworkListByAccountID(AccountID);

                foreach (var network in AccountResource.Networks)
                {
                    var sensors = await _sensorClient.GetSensorListByNetworkID(network.CSNetID);
                    AccountResource.Sensors.AddRange(_mapper.Map<List<SensorInfo>>(sensors));

                    var gateways = await _gatewayClient.GetGatewayListByNetworkID(network.CSNetID);
                    AccountResource.Gateways.AddRange(_mapper.Map<List<GatewayInfo>>(gateways));
                }
            }
         
            List<AccountSensors> AccountNumSensors = new List<AccountSensors>
                                                            {
                                                                new AccountSensors
                                                                    {
                                                                        AccountID = AccountDetails.AccountData[0].AccountID,
                                                                        NumSensors = AccountDetails.NumSensors
                                                                    }
                                                            };
            AccountResource.Customer =  (await PrepareCuctomersSummary(AccountDetails.AccountData, AccountNumSensors))[0];
            AccountResource.Customer.NumberOfGateways = AccountDetails.NumGateways;
            return AccountResource;
        }

        /// <summary>
        /// Update the Account ExpirationDate for a given Account
        /// </summary>
        /// <param name="AccountID">unique identofication for the Account</param>
        /// <param name="ExpirationDate">New Expiration Date for the Account</param>
        /// <returns>Record Updated Count</returns>
        public async Task<int> UpdateAccountSubscription(long AccountID, DateTime ExpirationDate)
        {
            return await _accountClient.UpdateAccountSubscription(AccountID, ExpirationDate.ToString("yyyy-MM-dd"));
        }

        /// <summary>
        /// Get the Manual PaymentHistory for the Account
        /// </summary>
        /// <param name="AccountID">Unique identofier for the Account</param>
        /// <returns>List of PaymentHistories</returns>
        public async Task<List<ManualPaymentHistory>> GetAccountSubscriptionHistory(long AccountID)
        {
            return await _accountClient.GetAccountSubscriptionHistory(AccountID);
        }


        /// <summary>
        /// Update the Account
        /// </summary>
        /// <param name="Account">Account Model</param>
        /// <returns>Updated record count</returns>
        public async Task<int> UpdateAccount(UpdateAccount Account)
        {
            return await _accountClient.UpdateAccount(Account);
        }

        #endregion


            #region PRIVATE MEMBERS     

        public async Task<double> GetSensorAnnualFee(AccountSensors AccountSensors,string PostanCode)
        {
            double amount = GetSensorAmount(AccountSensors);

            var taxAmount = await GetTaxAmount(amount, PostanCode);

            return amount + taxAmount;
        }

        public double GetSensorAmount(AccountSensors AccountSensors)
        {
            double Amount = 0;

            if (AccountSensors.NumSensors <= 24)
            {
                Amount = 100;
            }
            else if (AccountSensors.NumSensors >= 25 && AccountSensors.NumSensors <= 49)
            {
                Amount = 150;
            }
            else if (AccountSensors.NumSensors >= 50 && AccountSensors.NumSensors <= 74)
            {
                Amount = 200;
            }
            else if (AccountSensors.NumSensors >= 75 && AccountSensors.NumSensors <= 99)
            {
                Amount = 225;
            }
            else if (AccountSensors.NumSensors >= 100)
            {
                Amount = 100;
            }

            return Amount;
        }

        public async Task<double> GetTaxAmount(double Amount,string PostanCode)
        {
            double retVal = 0;

            TaxableStates stateTax = new TaxableStates();

            if (!string.IsNullOrEmpty(PostanCode))
            {
                stateTax = await _taxableStateClient.GetTaxableStatebyStateCode(PostanCode);
            }

            if (stateTax != null && stateTax.Tax > 0)
            {
                retVal = ((Amount * stateTax.Tax) / 100);
            }

            return retVal;
        }

        private CustomerStatusEnum GetCustomerStatus(Account Account)
        {
            if(Account.SubscriptionExpiry.Date < DateTime.Now.Date)
            {
                return CustomerStatusEnum.Overdue;
            }
            else if (Account.SubscriptionExpiry.Date > DateTime.Now.Date
                        && Account.SubscriptionExpiry.Date < DateTime.Now.Date.AddDays(60))
            {
                return CustomerStatusEnum.Due;
            }
            else if (Account.ActivationDate > DateTime.Now.Date.AddDays(-60))
            {
                return CustomerStatusEnum.New;
            }
            else
            {
                return CustomerStatusEnum.Renew;
            }
        }

        private string GetSubscriptionText(int SubType)
        {
            var retVal = "";

            switch ((SubscriptionType)SubType)
            {
                case SubscriptionType.Premier:
                    retVal = AppLocalizer.Premier;
                    break;
                default:
                    retVal = AppLocalizer.Basic;
                    break;
            }
            return retVal;
        }

        private async Task<List<CustomerStatus>> CalculateCustomerStatusSummary(List<Account> Accounts,List<AccountSensors> AccountNumSensors)
        {
            var customerStatusList = new List<CustomerStatus>();

            CustomerStatus overDueCustomer = new CustomerStatus();
            overDueCustomer.Status = AppLocalizer.Overdue;
            overDueCustomer.Title = AppLocalizer.Overdue;
            customerStatusList.Add(overDueCustomer);

            CustomerStatus dueCustomer = new CustomerStatus();
            dueCustomer.Status = AppLocalizer.Due;
            dueCustomer.Title = AppLocalizer.Due;
            customerStatusList.Add(dueCustomer);

            CustomerStatus renewCustomer = new CustomerStatus();
            renewCustomer.Status = AppLocalizer.Renew;
            renewCustomer.Title = AppLocalizer.Renew;
            customerStatusList.Add(renewCustomer);

            CustomerStatus newCustomer = new CustomerStatus();
            newCustomer.Status = AppLocalizer.New;
            newCustomer.Title = AppLocalizer.New;
            customerStatusList.Add(newCustomer);

            foreach (var account in Accounts)
            {
                var numSes = AccountNumSensors.Where(x => x.AccountID == account.AccountID).FirstOrDefault();

                if (numSes != null)
                {
                    switch (GetCustomerStatus(account))
                    {
                        case CustomerStatusEnum.Overdue:

                            overDueCustomer.Count += 1;
                            overDueCustomer.Amount += await GetSensorAnnualFee(numSes, account.PostalCode);
                            break;

                        case CustomerStatusEnum.Due:

                            dueCustomer.Count += 1;
                            dueCustomer.Amount += await GetSensorAnnualFee(numSes, account.PostalCode);
                            break;

                        case CustomerStatusEnum.New:

                            newCustomer.Count += 1;
                            newCustomer.Amount += await GetSensorAnnualFee(numSes, account.PostalCode);
                            break;

                        case CustomerStatusEnum.Renew:

                            renewCustomer.Count += 1;
                            renewCustomer.Amount += await GetSensorAnnualFee(numSes, account.PostalCode);
                            break;
                    }
                }
            }

            return customerStatusList;
        }

        private async Task<List<Customer>> PrepareCuctomersSummary(List<Account> Accounts, List<AccountSensors> AccountNumSensors)
        {
            List<Customer> customers = new List<Customer>();

            foreach (var acc in Accounts)
            {
                var customer = _mapper.Map<Customer>(acc);
                customer.Address += (string.IsNullOrEmpty(acc.Address2) ? "" : acc.Address2) + " " + acc.City + " " + acc.State + " " + acc.PostalCode;
                customers.Add(customer);
                var numSes = AccountNumSensors.Where(x => x.AccountID == acc.AccountID).FirstOrDefault();

                if (numSes != null)
                {
                    customer.NumberOfGateways = 0;
                    customer.NumberOfSensors = numSes.NumSensors;
                    customer.Amount = await GetSensorAnnualFee(numSes, acc.PostalCode);
                }

                //customer.Subscription = GetSubscriptionText(acc.SubscriptionType);
                customer.Subscription = acc.SubscriptionType;
                switch (GetCustomerStatus(acc))
                {
                    case CustomerStatusEnum.Overdue:
                        customer.Status = AppLocalizer.Overdue;
                        break;

                    case CustomerStatusEnum.Due:
                        customer.Status = AppLocalizer.Due;
                        break;

                    case CustomerStatusEnum.New:
                        customer.Status = AppLocalizer.New;
                        break;

                    case CustomerStatusEnum.Renew:
                        customer.Status = AppLocalizer.Renew;
                        break;
                }
            }

            return customers;
        }


        #endregion


    }
}
