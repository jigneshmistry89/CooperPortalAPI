using Coopers.BusinessLayer.Database.Domain.IRepositories;
using System.Threading.Tasks;
using System.Web.Http;

namespace Coopers.BusinessLayer.Database.API.Controllers
{
    [RoutePrefix("api/Locations")]
    [AllowAnonymous]
    public class LocationsController : ApiController
    {


        #region PRIVATE MEMBERS

        private readonly ILocationRepository _locationRepository;

        #endregion


        #region CONSTRUCTOR

        public LocationsController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        #endregion


        #region PUBLIC MEMBERS       

        #region GET | APIs

        [HttpGet]
        [Route("{ID}")]
        public async Task<IHttpActionResult> GetLocationByID(int ID)
        {
            return Ok(await _locationRepository.GetLocationByID(ID));
        }


        [HttpGet]
        [Route("UserLocations/{UserID}")]
        public async Task<IHttpActionResult> GetLocationByUserID(int UserID)
        {
            return Ok(await _locationRepository.GetLocationByUserID(UserID));
        }

        #endregion

        #endregion


    }
}
