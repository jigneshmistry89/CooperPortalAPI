using System;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class Network
    {
        public long CSNetID { get; set; }

        public string Name { get; set; }

        public bool SendNotifications { get; set; }

    }

    public class NetworkPermission
    {
        public long NetworkID { get; set; }

        public string NetworkName { get; set; }

        public bool CanAccess { get; set; }

    }

}
