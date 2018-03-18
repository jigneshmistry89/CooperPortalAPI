using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coopers.BusinessLayer.Database.Domain.Models
{
    [Table("AccountLocation")]
    public partial class AccountLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AccountID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public bool IsActive { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
