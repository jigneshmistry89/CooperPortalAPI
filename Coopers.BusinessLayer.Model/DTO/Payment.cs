using System;
using System.ComponentModel.DataAnnotations;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class Payment
    {
        [Required]
        public string StripeToken { get; set; }

        [Required]
        public string TransactionID { get; set; }

    }

    public class PaymentInfo
    {
        public long PaymentHistoryID { get; set; }

        public Transaction Transaction { get; set; }

    }

}
