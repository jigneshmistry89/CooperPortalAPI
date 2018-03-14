using Coopers.BusinessLayer.API.Filters;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Services.DTO;
using Coopers.BusinessLayer.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.API.Controllers
{
    [RoutePrefix("api/Location")]
    [AuthenticationFilter]
    public class LocationsController : ApiController
    {

        #region PRIVATE MEMBERS

        private readonly ILocationApplicationService _locationApplicationService;

        #endregion


        #region CONSTRUCTOR

        public LocationsController(ILocationApplicationService locationApplicationService)
        {
            _locationApplicationService = locationApplicationService;
        }

        #endregion


        #region PUBLIC MEMBERS       

        #region GET | APIs

        /// <summary>
        /// Get the Location Summary for a given Location ID
        /// </summary>
        /// <param name="LocationID">Unique ID of the location</param>
        /// <returns>LocationSummaryDTO</returns>
        [HttpGet]
        [Route("LocationSummary/{LocationID}")]
        [ResponseType(typeof(LocationSummary))]
        public async Task<IHttpActionResult> GetLocationSummaryByID(int LocationID)
        {
            return Ok(await _locationApplicationService.GetLocationSummaryByID(LocationID));
        }

        [AuthenticationFilter]
        /// <summary>
        /// Get the location summary for a give user
        /// </summary>
        /// <param name="UserID">Unique ID of the User</param>
        /// <returns>List of LocationSummaryDTO</returns>
        [HttpGet]
        [Route("UserLocations")]
        [ResponseType(typeof(List<LocationSummary>))]
        public async Task<IHttpActionResult> GetUserLocationSummary()
        {
            return Ok(await _locationApplicationService.GetUserLocationSummary());
        }

        [AuthenticationFilter]
        /// <summary>
        /// Get the Location details.
        /// </summary>
        /// <param name="LocationID">Unique ID of the location</param>
        /// <returns>LocationDetailsDTO</returns>
        [HttpGet]
        [Route("LocationDetails/{LocationID}")]
        [ResponseType(typeof(LocationDetails))]
        public async Task<IHttpActionResult> GetLocationDetails(int LocationID)
        {
            return Ok(await _locationApplicationService.GetLocationDetails(LocationID));
        }

        #endregion

        #endregion


    }
}
