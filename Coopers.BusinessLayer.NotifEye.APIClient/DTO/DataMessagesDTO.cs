using System;

namespace Coopers.BusinessLayer.NotifEye.APIClient.DTO
{
    public class DataMessages
    {
        public long MessageID { get; set; }

        public long SensorID { get; set; }

        public DateTime MessageDate { get; set; }

        public short State { get; set; }

        public short SignalStrength { get; set; }

        public short Voltage { get; set; }

        public short Battery { get; set; }

        public double Data { get; set; }

        public string DisplayData { get; set; }

        public double PlotValue { get; set; }

        public bool MetNotificationRequirements { get; set; }

        public long GatewayID { get; set; }

    }
}
