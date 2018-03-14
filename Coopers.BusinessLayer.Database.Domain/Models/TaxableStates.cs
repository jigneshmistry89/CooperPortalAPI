using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coopers.BusinessLayer.Database.Domain.Models
{
    [Table("TaxableStates")]
    public partial class TaxableStates
    {

        [Key]
        public long ID { get; set; }

        public string StateCode { get; set; }

        public string StateName { get; set; }

        public double Tax { get; set; }

    }
}
