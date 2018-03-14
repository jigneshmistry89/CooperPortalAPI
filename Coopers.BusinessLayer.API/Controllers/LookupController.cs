using Coopers.BusinessLayer.API.Filters;
using Coopers.BusinessLayer.Services.Services;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.API.Controllers
{
    /// <summary>
    /// Lookup Endpoint
    /// </summary>
    [RoutePrefix("api/Lookup")]
    [AuthenticationFilter]
    public class LookupController : ApiController
    {

        #region PRIVATE MEMBERS

        private ILookupApplicationService _lookupApplicationService;

        #endregion


        #region CONSTRUCTOR

        public LookupController(ILookupApplicationService lookupApplicationService)
        {
            _lookupApplicationService = lookupApplicationService;
        }

        #endregion


        #region PUBLIC MEMBERS       


        #region GET | APIs


        /// <summary>
        /// Returns the list of supported Application Names and Id's.
        /// </summary>
        /// <returns>application info</returns>
        [HttpGet]
        [Route("ApplicationList")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetApplicationList()
        {
            return Ok(await _lookupApplicationService.GetApplications());
        }


        #endregion


        #region PUT | APIs

     
        #endregion


        #region DELETE | APIs
    

        #endregion


        #endregion


    }
}
