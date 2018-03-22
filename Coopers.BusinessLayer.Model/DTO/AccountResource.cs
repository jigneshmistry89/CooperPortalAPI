using System;
using System.Collections.Generic;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class AccountResource
    {
        public AccountResource()
        {
            Networks = new List<Network>();
            Sensors = new List<SensorInfo>();
            Gateways = new List<GatewayInfo>();
        }
        public Customer Customer { get; set; }

        public List<Network> Networks { get; set; }

        public List<SensorInfo> Sensors { get; set; }

        public List<GatewayInfo> Gateways { get; set; }

        public List<UserInfo> Users { get; set; }

        public List<PaymentHistoryInfo> PaymentHistories { get; set; }

    }

    public class SensorInfo
    {
        public long SensorID { get; set; }

        public string SensorName { get; set; }

        public string SensorType { get; set; }

        public long CSNetID { get; set; }

    }

    public class GatewayInfo
    {
        public long GatewayID { get; set; }

        public string Name { get; set; }

        public string GatewayType { get; set; }

        public long CSNetID { get; set; }
    }

    public class UserInfo
    {
        public long UserID { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int SMSCarrierID { get; set; }

        public long ExternalSMSProviderID { get; set; }

        public bool DirectSMS { get; set; }

        public bool RecievesMaintenanceByEmail { get; set; }

        public bool RecievesMaintenanceBySMS { get; set; }

        public bool RecievesNotificaitonsBySMS { get; set; }

        public bool RecievesNotificaitonsByVoice { get; set; }

        public string VoiceNumber { get; set; }

        public string EmailAddress { get; set; }

        public string SMSNumber { get; set; }

        public bool Admin { get; set; }

        public List<long> NetworkPermissions { get; set; }
    }

}
