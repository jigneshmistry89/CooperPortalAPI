using Coopers.BusinessLayer.Database.Domain.IRepositories;
using Coopers.BusinessLayer.Database.Domain.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.Database.API.Controllers
{
    /// <summary>
    /// TaxableState Resource
    /// </summary>
    [RoutePrefix("api/TaxableState")]
    [AllowAnonymous]
    public class TaxableStateController : ApiController
    {

        #region PRIVATE MEMBERS

        private readonly ITaxableStatesRepository _taxableStateRepository;

        #endregion


        #region CONSTRUCTOR

        public TaxableStateController(ITaxableStatesRepository taxableStateRepository)
        {
            _taxableStateRepository = taxableStateRepository;
        }

        #endregion


        #region PUBLIC MEMBERS       

        #region GET | APIs

        /// <summary>
        /// Get the State by the StateCode
        /// </summary>
        /// <param name="StateCode">StateCode</param>
        /// <returns>TaxableState model</returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(TaxableStates))]
        public async Task<IHttpActionResult> GetTaxableStateByStateCode(string StateCode)
        {
            return Ok(await _taxableStateRepository.GetTaxableStatebyStateCode(StateCode));
        }

        #endregion

        #region POST | APIs
     

        #endregion

        #endregion


    }
}
