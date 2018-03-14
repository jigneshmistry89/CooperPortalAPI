namespace Coopers.BusinessLayer.Model.DTO
{
    public class UserLocationSummary
    {
        public long ID { get; set; }

        public string Title { get; set; }
       
        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public int ActiveSensors { get; set; }

        public int Alerts { get; set; }

        public int MissedCommunication { get; set; }

        public int LowSignal { get; set; }

        public int LowBattery { get; set; }

    }

}
