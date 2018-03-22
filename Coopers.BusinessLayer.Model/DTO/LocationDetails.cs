using System;
using System.Collections.Generic;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class LocationDetails
    {
        public LocationDetails()
        {
            Gateways = new List<GatewayDetails>();
            Sensors = new List<SensorDetails>();
        }

        public long ID { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public int NoOfGateways { get; set; }

        public int NoOfSensors { get; set; }

        public List<GatewayDetails> Gateways { get; set; }

        public List<SensorDetails> Sensors { get; set; }

    }

    public class SensorDetails
    {
        public long SensorID { get; set; }
        public int Type { get; set; }
        public int CSNetID { get; set; }
        public string SensorName { get; set; }
        public DateTime LastCommunicationDate { get; set; }
        public DateTime NextCommunicationDate { get; set; }
        public long LastDataMessageID { get; set; }
        public int PowerSourceID { get; set; }
        public int Status { get; set; }
        public bool CanUpdate { get; set; }
        public string CurrentReading { get; set; }
        public float BatteryLevel { get; set; }
        public float SignalStrength { get; set; }
        public bool AlertsActive { get; set; }
        public long MinimumThreshold { get; set; }
        public long MaximumThreshold { get; set; }
        public string Scale { get; set; }
    }

    public class GatewayDetails
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
