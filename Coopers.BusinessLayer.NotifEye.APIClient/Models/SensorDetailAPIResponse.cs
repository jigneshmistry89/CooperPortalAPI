using Coopers.BusinessLayer.NotifEye.APIClient.DTO;

namespace Coopers.BusinessLayer.NotifEye.APIClient.Models
{
    internal class SensorDetailAPIResponse
    {
        public string Method { get; set; }

        public SensorDetail Result { get; set; }
    }
}
