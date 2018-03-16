using System;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class PaymentHistory
    {
        public long ID { get; set; }

        public string StripeChargeID { get; set; }

        public DateTime HistoryDate { get; set; }

        public long CustomerID { get; set; }

        public string ProductName { get; set; }

        public long AccountID { get; set; }

        public string Data { get; set; }

    }

    public class PaymentHistoryInfo
    {
        public long ID { get; set; }

        public string StripeChargeID { get; set; }

        public string Type { get; set; }

        public string ProductName { get; set; }

        public long CustomerID { get; set; }

        public string CustomerName { get; set; }

        public long AccountID { get; set; }

        public string AccountName { get; set; }

        public string OldRenewalDate { get; set; }

        public string NewRenewalDate { get; set; }

        public string HistoryDate { get; set; }

        public double SubscriptionAmount { get; set; }

        public string TaxString { get; set; }

        public double TaxAmount { get; set; }

        public double TotalAmount { get; set; }

        public string InvoiceDownloadLink { get; set; }

    }

    public class ManualPaymentHistory
    {
        public long AccountSubscriptionID { get; set; }

        public DateTime OldExpirationDate { get; set; }

        public DateTime NewExpirationDate { get; set; }

        public DateTime ChangeDate { get; set; }

        public string UserName { get; set; }
    }

}
