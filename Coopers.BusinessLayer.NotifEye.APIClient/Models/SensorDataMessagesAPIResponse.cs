using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using System.Collections.Generic;

namespace Coopers.BusinessLayer.NotifEye.APIClient.Models
{
    internal class SensorDataMessagesAPIResponse
    {
        public string Method { get; set; }

        public List<DataMessages> Result { get; set; }
    }
}
