using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class UpdateGateway
    {
        [Required]
        public long GatewayID { get; set; }

        [Required]
        public long NetworkID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string GatewayType { get; set; }

        [Required]
        public long SerialNumber { get; set; }

        [Required]
        public string MacAddress { get; set; }

        [Required]
        public int HeartBeat { get; set; }

    }

    public class GatewayBulkAssign
    {
        [Required]
        public List<long> GatewayIDs { get; set; }

        [Required]
        public long NetworkID { get; set; }

    }

    public class GatewayBulkResponse
    {
        public long GatewayID { get; set; }

        public string Result { get; set; }
    }
}
