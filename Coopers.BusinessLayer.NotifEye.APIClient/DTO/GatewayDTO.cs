using System;

namespace Coopers.BusinessLayer.NotifEye.APIClient.DTO
{
    public class GatewayDTO
    {
        public long GatewayID { get; set; }

        public long NetworkID { get; set; }

        public string Name { get; set; }

        public string GatewayType { get; set; }

        public short Heartbeat { get; set; }

        public bool IsDirty { get; set; }

        public DateTime LastCommunicationDate { get; set; }

        public string LastInboundIPAddress { get; set; }

        public string MacAddress { get; set; }

    }
}
