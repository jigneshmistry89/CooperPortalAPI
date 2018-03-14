using Coopers.BusinessLayer.Database.Domain.IRepositories;
using Coopers.BusinessLayer.Database.Domain.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.Database.API.Controllers
{
    /// <summary>
    /// PaymentHistory Resource
    /// </summary>
    [RoutePrefix("api/PaymentHistory")]
    [AllowAnonymous]
    public class PaymentHistoryController : ApiController
    {


        #region PRIVATE MEMBERS

        private readonly IPaymentHistoryRepository _paymentHistoryRepository;

        #endregion


        #region CONSTRUCTOR

        public PaymentHistoryController(IPaymentHistoryRepository paymentHistoryRepository)
        {
            _paymentHistoryRepository = paymentHistoryRepository;
        }

        #endregion


        #region PUBLIC MEMBERS       

        #region GET | APIs

        /// <summary>
        /// Get the paymenthistory details by PaymentHistoryID
        /// </summary>
        /// <param name="PaymentHistoryID">unique identofied for the PaymentHistory</param>
        /// <returns>PaymentHistory Model</returns>
        [HttpGet]
        [Route("{PaymentHistoryID}")]
        [ResponseType(typeof(PaymentHistory))]
        public async Task<IHttpActionResult> GetPaymentHistoryID(long PaymentHistoryID)
        {
            return Ok(await _paymentHistoryRepository.GetEntityByIdAsync(PaymentHistoryID));
        }

        
        [HttpGet]
        [Route("List")]
        [ResponseType(typeof(PaymentHistory))]
        public async Task<IHttpActionResult> GetPaymentHistoryList(int AccountID)
        {
            return Ok(await _paymentHistoryRepository.GetPaymentHistoryList(AccountID));
        }

        #endregion

        #region POST | APIs

        /// <summary>
        /// Create a PaymentHistory Record
        /// </summary>
        /// <param name="PaymentHistory"></param>
        /// <returns>PaymentHistory ID</returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(long))]
        public async Task<IHttpActionResult> CreatePaymentHistory(PaymentHistory PaymentHistory)
        {
            await _paymentHistoryRepository.CreateEntityAsync(PaymentHistory);
            return Ok(PaymentHistory.ID);
        }

        #endregion

        #region DELETE | APIs

        /// <summary>
        /// Delete a PaymentHistory Record
        /// </summary>
        /// <param name="PaymentHistoryID">Unique identifier for the PaymentHistory</param>
        /// <returns>No of record Updated</returns>
        [HttpDelete]
        [Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> DeletePaymentHistory([FromUri]long PaymentHistoryID)
        {
            return Ok(_paymentHistoryRepository.DeleteEntityByID(PaymentHistoryID));
        }

        #endregion


        #endregion


    }
}
