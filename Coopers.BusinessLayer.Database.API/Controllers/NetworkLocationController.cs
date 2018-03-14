using Coopers.BusinessLayer.Database.Domain.IRepositories;
using Coopers.BusinessLayer.Database.Domain.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.Database.API.Controllers
{
    [RoutePrefix("api/NetworkLocation")]
    [AllowAnonymous]
    public class NetworkLocationController : ApiController
    {


        #region PRIVATE MEMBERS

        private readonly INetworkLocationRepository _networkLocationRepository;

        #endregion


        #region CONSTRUCTOR

        public NetworkLocationController(INetworkLocationRepository networkLocationRepository)
        {
            _networkLocationRepository = networkLocationRepository;
        }

        #endregion


        #region PUBLIC MEMBERS       

        #region GET | APIs

        /// <summary>
        /// Get the networkLocation details by CSNetID
        /// </summary>
        /// <param name="CSNetID">NetworkID</param>
        /// <returns>Networklocation Model</returns>
        [HttpGet]
        [Route("{CSNetID}")]
        [ResponseType(typeof(NetworkLocation))]
        public async Task<IHttpActionResult> GetNetworkLocationByID(long CSNetID)
        {
            return Ok(await _networkLocationRepository.GetEntityByIdAsync(CSNetID));
        }

        #endregion

        #region POST | APIs

        /// <summary>
        /// Create a networklocation record
        /// </summary>
        /// <param name="NetworkLocation">Networklocation model</param>
        /// <returns>Id of the newly created NetworkLocation</returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(long))]
        public async Task<IHttpActionResult> CreateNetworkLocation([FromBody]NetworkLocation NetworkLocation)
        {
            return Ok(await _networkLocationRepository.CreateEntityAsync(NetworkLocation));
        }

        #endregion

        #endregion


    }
}
