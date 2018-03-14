using System;

namespace Coopers.BusinessLayer.Model.DTO
{

    public class Transaction
    {

        public Transaction()
        {
            ID = Guid.NewGuid();
        }

        public Transaction(string Name)
        {
            ID = Guid.NewGuid();
            ProductName = Name;
        }

        public Guid ID { get; set; }

        public string ProductName { get; set; }

        public object TransactionInfo { get; set; }

    }


    public class NotifEyeTransactionInfo
    {
        public long CustomerID { get; set; }

        public string CustomerName { get; set; }

        public long AccountID { get; set; }

        public string AccountName { get; set; }

        //public double Amount { get; set; }

        public double SubscriptionAmount { get; set; }

        public string TaxString { get; set; }

        public double TaxAmount { get; set; }

        public double TotalAmount { get; set; }

        //public float Tax { get; set; }

        public int NumberOfSensor { get; set; }

        public DateTime OldRenewalDate { get; set; }

        public DateTime NewRenewalDate { get; set; }

        public string Address { get; set; }

        public string PrimaryUserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
       
    }

}
