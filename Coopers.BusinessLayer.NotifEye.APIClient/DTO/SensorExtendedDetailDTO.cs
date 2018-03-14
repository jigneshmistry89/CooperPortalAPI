using System;

namespace Coopers.BusinessLayer.NotifEye.APIClient.DTO
{
    public class SensorExtendedDetail
    {
        /// <summary>
        /// Unique identifier of the sensor
        /// </summary>
        public long SensorID { get; set; }

        public long ReportInterval { get; set; }

        public long ActiveStateInterval { get; set; }

        public long InactivityAlert { get; set; }

        public long MeasurementsPerTransmission { get; set; }

        public long MinimumThreshold { get; set; }

        public long MaximumThreshold { get; set; }

        public long Hysteresis { get; set; }

        public string Tag { get; set; }

        public long MonnitApplicationID { get; set; }

        public long CSNetID { get; set; }

        public string SensorName { get; set; }

        public DateTime LastCommunicationDate { get; set; }

        public DateTime NextCommunicationDate { get; set; }

        public long LastDataMessageID { get; set; }

        public long PowerSourceID { get; set; }

        public string Status { get; set; }

        public bool CanUpdate { get; set; }

        public string CurrentReading { get; set; }

        public short BatteryLevel { get; set; }

        public short SignalStrength { get; set; }

        public bool AlertsActive { get; set; }
    }
}
