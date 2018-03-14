using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coopers.BusinessLayer.Database.Domain.Models
{
    [Table("PaymentHistory")]
    public partial class PaymentHistory
    {
        [Key]
        public long ID { get; set; }

        public string StripeChargeID { get; set; }

        public DateTime HistoryDate { get; set; }

        public long CustomerID { get; set; }

        public string ProductName { get; set; }

        public long AccountID { get; set; }

        public string Data { get; set; }

    }
}
