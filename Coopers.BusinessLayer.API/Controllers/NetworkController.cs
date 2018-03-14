using Coopers.BusinessLayer.API.Filters;
using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using Coopers.BusinessLayer.Services.DTO;
using Coopers.BusinessLayer.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.API.Controllers
{
    /// <summary>
    /// Network Endpoint
    /// </summary>
    [RoutePrefix("api/Network")]
    [AuthenticationFilter]
    public class NetworkController : ApiController
    {

        #region PRIVATE MEMBERS

        private INetworkApplicationService _networkApplicationService;

        #endregion


        #region CONSTRUCTOR

        public NetworkController(INetworkApplicationService networkApplicationService)
        {
            _networkApplicationService = networkApplicationService;
        }

        #endregion


        #region PUBLIC MEMBERS       

        #region GET | APIs

       
        /// <summary>
        /// Returns the list of networks that belong to user.
        /// </summary>
        /// <returns>returns the network list</returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(List<Network>))]
        public async Task<IHttpActionResult> GetNetworkList()
        {
            return Ok(await _networkApplicationService.GetNetworkList());
        }

        #endregion

        #region POST | APIs

        /// <summary>
        /// Adds new wireless sensor network to account
        /// </summary>
        /// <param name="Network">Network create model</param>
        /// <returns>unique identifier for the newly created Network</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateNetwork([FromBody]CreateNetwork Network)
        {
            return Ok(await _networkApplicationService.CreateNetwork(Network));
        }


        #endregion


        #region DELETE | APIs

        /// <summary>
        /// Removes the network from the system.
        /// </summary>
        /// <param name="Name">Unique identifier of the network</param>
        /// <returns>Success/Failure</returns>
        [HttpDelete]
        [Route("{NetworkID}")]
        public async Task<IHttpActionResult> DeleteNetwork(long NetworkID)
        {
            return Ok(await _networkApplicationService.DeleteNetwork(NetworkID));
        }

        #endregion

        #endregion


    }
}
