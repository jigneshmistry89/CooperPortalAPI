namespace Coopers.BusinessLayer.Database.APIClient.DTO
{
    public class NetworkLocation
    {
        public long CSNetID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public bool IsActive { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }


    }

}
