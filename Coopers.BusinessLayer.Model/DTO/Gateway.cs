using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class Gateway
    {
        public long GatewayID { get; set; }

        public long NetworkID { get; set; }

        public string Name { get; set; }

        public int GatewayTypeID { get; set; }

        public long SerialNumber { get; set; }

        public string MacAddress { get; set; }

        public string RadioBand { get; set; }

        public string APNFirmwareVersion { get; set; }

        public string GatewayFirmwareVersion { get; set; }

        public int PowerSourceID { get; set; }

        public int CustomerID { get; set; }
    }
}
