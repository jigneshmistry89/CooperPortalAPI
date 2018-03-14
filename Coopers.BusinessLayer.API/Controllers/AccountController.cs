using Coopers.BusinessLayer.API.Filters;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.API.Controllers
{
    /// <summary>
    /// Account Endpoint
    /// </summary>
    [RoutePrefix("api/Account")]
    [AuthenticationFilter]
    public class AccountController : ApiController
    {

        #region PRIVATE MEMBERS

        private IAccountApplicationService _accountApplicationService;

        #endregion


        #region CONSTRUCTOR

        public AccountController(IAccountApplicationService accountApplicationService)
        {
            _accountApplicationService = accountApplicationService;
        }

        #endregion


        #region PUBLIC MEMBERS       


        #region GET | APIs

        [HttpGet]
        [Route("AccountByID")]
        public async Task<IHttpActionResult> GetAccountByID([FromUri]long AccountID)
        {
            return Ok(await _accountApplicationService.GetAccountByID(AccountID));
        }


        [HttpGet]
        [Route("AccountByCustomerID")]
        public async Task<IHttpActionResult> GetAccountByCustomerID([FromUri]long CustomerID)
        {
            return Ok(await _accountApplicationService.GetAccountByCustomerID(CustomerID));
        }

       
        /// <summary>
        /// Get the Account summary
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AccountSummary")]
        [ResponseType(typeof(AccountSummary))]
        public async Task<IHttpActionResult> GetAccountSummary()
        {
            return Ok(await _accountApplicationService.GetAccountSummary());
        }

        /// <summary>
        /// Get the Account Resources for an Account
        /// </summary>
        /// <returns>AccountResource model</returns>
        [HttpGet]
        [Route("{AccountID}/Details")]
        [ResponseType(typeof(AccountResource))]
        public async Task<IHttpActionResult> GetAccountResources(long AccountID)
        {
            return Ok(await _accountApplicationService.GetAccountResources(AccountID));
        }

        /// <summary>
        /// Get the customer status summary
        /// </summary>
        /// <returns>status summary model</returns>
        [HttpGet]
        [Route("CustomerStatusSummary")]
        [ResponseType(typeof(List<CustomerStatus>))]
        public async Task<IHttpActionResult> GetAccountStatusSummary()
        {
            return Ok(await _accountApplicationService.GetCustomerStatusSummary());
        }

        /// <summary>
        /// Get the customers list
        /// </summary>
        /// <returns>List of customer summary model</returns>
        [HttpGet]
        [Route("Customers")]
        [ResponseType(typeof(List<Customer>))]
        public async Task<IHttpActionResult> GetCustomers()
        {
            return Ok(await _accountApplicationService.GetCustomers());
        }

        /// <summary>
        /// Get the Account summary
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{AccountID}/users")]
        [ResponseType(typeof(AccountSummary))]
        public async Task<IHttpActionResult> GetAccountUserList(long AccountID)
        {
            return Ok(await _accountApplicationService.GetAccountUserList(AccountID));
        }

        #endregion


        #region POST | APIs

        /// <summary>
        ///  Creates a new account
        /// </summary>
        /// <param name="Account">Account model</param>
        /// <returns>Success info/Error List</returns>
        //[HttpPost]
        //[Route("")]
        //public async Task<IHttpActionResult> CreateAccount(Account Account)
        //{
        //    return Ok(await _accountApplicationService.CreateAccount(Account));
        //}

        #endregion

        #region PUT | APIs

        /// <summary>
        /// Update the Account
        /// </summary>
        /// <param name="Account">Account Model</param>
        /// <returns>Updated record count</returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> UpdateAccount(UpdateAccount Account)
        {
            return Ok(await _accountApplicationService.UpdateAccount(Account));
        }

        #endregion


        #endregion


    }
}
