using System.ComponentModel.DataAnnotations;

namespace Coopers.BusinessLayer.Services.DTO
{
    public class CreateNetwork
    {
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }

    public class UpdateNetwork
    {
        public long NetworkID { get; set; }

        [Required]
        public string Name { get; set; }

        public bool SendNotifications { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
