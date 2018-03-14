using System.Collections.Generic;

namespace Coopers.BusinessLayer.Services.DTO
{
    public class LocationSummary
    {
        public LocationSummary()
        {
            SensorSummary = new List<SensorSummary>();
        }

        public long ID { get; set; }

        public string ProfileName { get; set; }

        public long AccountID { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public List<SensorSummary> SensorSummary { get; set; }
    }

    public class SensorSummary
    {
        public int ActiveSensors { get; set; }

        public int Alerts { get; set; }

        public int MissedCommunication { get; set; }

        public int LowSignal { get; set; }

        public int LowBattery { get; set; }

    }
}
