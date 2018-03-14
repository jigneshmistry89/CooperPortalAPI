using System.Net;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class ErrorInfo
    {
        public string Method { get; set; }

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }

}
