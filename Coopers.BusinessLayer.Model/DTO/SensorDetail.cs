using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class SensorDetail
    {
        public long SensorID { get; set; }

        public long MinimumThreshold { get; set; }

        public long MaximumThreshold { get; set; }

        public long ReportInterval { get; set; }

        public long ActiveStateInterval { get; set; }

        public long InactivityAlert { get; set; }

        public string Tag { get; set; }

        public long SensorType { get; set; }

        public long CSNetID { get; set; }

        public string NetworkName { get; set; }

        public string SensorName { get; set; }

        public DateTime LastCommunicationDate { get; set; }

        public DateTime NextCommunicationDate { get; set; }

        public long LastDataMessageID { get; set; }

        public string Status { get; set; }

        public bool CanUpdate { get; set; }

        public string CurrentReading { get; set; }

        public short BatteryLevel { get; set; }

        public short SignalStrength { get; set; }

        public bool AlertsActive { get; set; }

        public long GatewayID { get; set; }

        public string GatewayType { get; set; }

        public string GatewayName { get; set; }

        public double DataCelsius { get; set; }

        public string DisplayData { get; set; }

        public double DataFahrenheit { get; set; }

        public string CorF { get; set; }

    }

    public class LastDataMessage
    {
        public long MessageID { get; set; }

        public string Data { get; set; }

        public string DisplayData { get; set; }

        public string PlotValue { get; set; }

        public int GatewayID { get; set; }
    }

    public class SensorBulkAssign
    {
        public List<long> SensorIDs { get; set; }

        public long NetworkID { get; set; }

    }

    public class SensorBulkResponse
    {
        public long SensorID { get; set; }

        public string Result { get; set; }
    }

    public class UpdateSensor
    {
        public long SensorID { get; set; }
        public string SensorName { get; set; }
        public double HeartBeat { get; set; }
        public long MinimumThreshold { get; set; }
        public long MaximumThreshold { get; set; }
    }

    public class SensorAttribute
    {
        public long SensorID { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class CreateSensor
    {
        public long SensorID { get; set; }

        public long NetworkID { get; set; }

        public long AccountID { get; set; }

        public int MonnitApplicationID { get; set; }

        public string Name { get; set; }

        public string SensorCode { get; set; }

    }

    public class UpdateSensorNote
    {
        [Required]
        public long SensorID { get; set; }

        [Required]
        public string Note { get; set; }

    }

    public class SensorNoteResponse
    {
        public string Message { get; set; }
    }

}
