using Coopers.BusinessLayer.API.Filters;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Services.Services;
using Elmah;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.API.Controllers
{
    /// <summary>
    /// Gateway Endpoint
    /// </summary>
    [RoutePrefix("api/Payment")]
    [AuthenticationFilter]
    public class PaymentController : ApiController
    {

        #region PRIVATE MEMBERS

        private IPaymentApplicationService _paymentApplicationService;

        #endregion


        #region CONSTRUCTOR

        public PaymentController(IPaymentApplicationService paymentApplicationService)
        {
            _paymentApplicationService = paymentApplicationService;
        }

        #endregion


        #region PUBLIC MEMBERS       


        #region GET | APIs

        /// <summary>
        /// Get the payment details for a given Product 
        /// </summary>
        /// <param name="ProductName">Product Name</param>
        /// <returns>Payment Details model</returns>
        [HttpGet]
        [Route("Details")]
        [ResponseType(typeof(Transaction))]
        public async Task<IHttpActionResult> GetPaymentDetails([FromUri]string ProductName = "")
        {
            return Ok(await _paymentApplicationService.GetPaymentDetails(ProductName));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Invoice")]
        public async Task<HttpResponseMessage> DownLoadInvoice(long PaymentHistoryID)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(await _paymentApplicationService.GenerateInvoice(PaymentHistoryID))
            };

            result.Content.Headers.ContentDisposition =
            new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = "Invoice.pdf"
            };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }

        [HttpGet]
        [Route("PaymentHistoryList")]
        [ResponseType(typeof(Transaction))]
        public async Task<IHttpActionResult> PaymentHistoryList(int Offset,int PageSize)
        {
            return Ok(await _paymentApplicationService.GetPaymentHistoryList(Offset, PageSize));
        }


        #endregion


        #region PUT | APIs

        #endregion


        #region DELETE | APIs

        #endregion


        #region POST | APIs

        /// <summary>
        /// Execute the payment
        /// </summary>
        /// <param name="Payment">Payment model</param>
        /// <returns>PaymentHistory ID</returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(long))]
        public async Task<IHttpActionResult> MakePayment(Payment Payment)
        {
            return Ok(await _paymentApplicationService.ExecutePayment(Payment));
        }


        #endregion


        #endregion


    }
}
