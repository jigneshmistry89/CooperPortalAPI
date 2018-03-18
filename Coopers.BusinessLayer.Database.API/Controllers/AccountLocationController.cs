using Coopers.BusinessLayer.Database.Domain.IRepositories;
using Coopers.BusinessLayer.Database.Domain.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.Database.API.Controllers
{
    /// <summary>
    /// AccountLocation
    /// </summary>
    [RoutePrefix("api/AccountLocation")]
    [AllowAnonymous]
    public class AccountLocationController : ApiController
    {


        #region PRIVATE MEMBERS

        private readonly IAccountLocationRepository _accountLocationRepository;

        #endregion


        #region CONSTRUCTOR

        public AccountLocationController(IAccountLocationRepository accountLocationRepository)
        {
            _accountLocationRepository = accountLocationRepository;
        }

        #endregion


        #region PUBLIC MEMBERS       

        #region GET | APIs

        /// <summary>
        /// Get the accountLocation details by AccountID
        /// </summary>
        /// <param name="AccountID">Unique indentofier for the Account</param>
        /// <returns>AccountLocation Model</returns>
        [HttpGet]
        [Route("{AccountID}")]
        [ResponseType(typeof(AccountLocation))]
        public async Task<IHttpActionResult> GetAccountLocationByID(long AccountID)
        {
            return Ok(await _accountLocationRepository.GetAccountLocationByID(AccountID));
        }

        #endregion

        #region POST | APIs

        /// <summary>
        /// Create a accountLocation record
        /// </summary>
        /// <param name="AccountLocation">AccountLocation model</param>
        /// <returns>Id of the newly created AccountLocation</returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(long))]
        public async Task<IHttpActionResult> CreateAccountLocation([FromBody]AccountLocation AccountLocation)
        {
            return Ok(await _accountLocationRepository.CreateEntityAsync(AccountLocation));
        }

        #endregion

        #region PUT | APIs

        /// <summary>
        /// Update a AccountLocation record
        /// </summary>
        /// <param name="AccountLocation">AccountLocation model</param>
        /// <returns>No of records Update</returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> UpdateAccountLocation([FromBody]AccountLocation AccountLocation)
        {
            return Ok(await _accountLocationRepository.UpdateAccountLocation(AccountLocation));
        }

        #endregion

        #endregion


    }
}
