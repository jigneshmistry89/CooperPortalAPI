using System;

namespace Coopers.BusinessLayer.NotifEye.APIClient.DTO
{
    public class SensorDetail
    {
        /// <summary>
        /// Unique identifier of the sensor
        /// </summary>
        public long SensorID { get; set; }

        public long ReportInterval { get; set; }

        public short MonnitApplicationID { get; set; }

        public long CSNetID { get; set; }

        public string SensorName { get; set; }

        public DateTime LastCommunicationDate { get; set; }

        public DateTime NextCommunicationDate { get; set; }

        public long LastDataMessageID { get; set; }

        public int PowerSourceID { get; set; }

        public int Status { get; set; }

        public bool CanUpdate { get; set; }

        public string CurrentReading { get; set; }

        public short BatteryLevel { get; set; }

        public short SignalStrength { get; set; }

        public bool AlertsActive { get; set; }

        public long MinimumThreshold { get; set; }

        public long MaximumThreshold { get; set; }

    }

    public class SensorCreate
    {
        public long SensorID { get; set; }

        public long NetworkID { get; set; }

        public long AccountID { get; set; }

        public int MonnitApplicationID { get; set; }

        public string Name { get; set; }

        public string SensorCode { get; set; }

        public long MinimumThreshold { get; set; }

        public long MaximumThreshold { get; set; }

        public long ReportInterval { get; set; }

        public long ActiveStateInterval { get; set; }
    }
}
