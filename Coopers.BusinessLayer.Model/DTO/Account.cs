using System;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class Account
    {
        public long AccountID { get; set; }

        public string CompanyName { get; set; }

        public int TimeZoneID { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public int ResellerID { get; set; }

        public string UserName { get; set; }

        public long UserID { get; set; }

        public string  FullName { get; set; }

        public string EmailAddress { get; set; }

        public string SMSNumber { get; set; }

        public long ExternalSMSProviderID { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public long SubscriptionID { get; set; }

        public string SubscriptionType { get; set; }

        public DateTime SubscriptionExpiry { get; set; }

    }

    public class UpdateAccount
    {
        public long AccountID { get; set; }

        //public int TimeZoneID { get; set; }

        public int ResellerID { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }

}
