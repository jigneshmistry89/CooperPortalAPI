using System.Collections.Generic;

namespace Coopers.BusinessLayer.Database.Models.DTO
{
    public class LocationDTO
    {
        public long ID { get; set; }

        public string ProfileName { get; set; }

        public long AccountID { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public List<long> NetworkIDs { get; set; }
    }
}
