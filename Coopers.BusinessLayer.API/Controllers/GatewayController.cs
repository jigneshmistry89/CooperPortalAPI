using Coopers.BusinessLayer.API.Filters;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Services.Services;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Coopers.BusinessLayer.API.Controllers
{
    /// <summary>
    /// Gateway Endpoint
    /// </summary>
    [RoutePrefix("api/Gateway")]
    [AuthenticationFilter]
    public class GatewayController : ApiController
    {

        #region PRIVATE MEMBERS

        private IGatewayApplicationService _gatewayApplicationService;

        #endregion


        #region CONSTRUCTOR

        public GatewayController(IGatewayApplicationService gatewayApplicationService)
        {
            _gatewayApplicationService = gatewayApplicationService;
        }

        #endregion


        #region PUBLIC MEMBERS       


        #region GET | APIs

        /// <summary>
        /// Returns the list of gateways that belongs to user.
        /// </summary>
        /// <param name="Name">Filters list to gateways with names containing this string. (case-insensitive)</param>
        /// <param name="NetworkID">Filters list to gateway that belong to this network id</param>
        /// <param name="ApplicationID">Filters list to gateway that are this application type</param>
        /// <param name="Status">Filters list to gateway that match this status</param>
        /// <returns>gateway list</returns>
        [HttpGet]
        [Route("GatewayList")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetGatewayList([FromUri]string Name = "", [FromUri]long NetworkID = 0, [FromUri]short ApplicationID = 0, [FromUri]short Status = 0)
        {
            return Ok(await _gatewayApplicationService.GetGatewayList(Name, NetworkID, ApplicationID, Status));
        }


        #endregion


        #region PUT | APIs


        /// <summary>
        /// Assigns gateway to the specified network
        /// </summary>
        /// <param name="GatewayID">Identifier of gateway to move</param>
        /// <param name="NetworkID">Identifier of network on your account</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{GatewayID}/AssignTo")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> AssignGateway(long GatewayID, long NetworkID)
        {
            return Ok(await _gatewayApplicationService.AssignGateway(GatewayID, NetworkID));
        }

        #endregion


        #region DELETE | APIs

        /// <summary>
        /// Removes the gateway object from the network.
        /// </summary>
        /// <param name="GatewayID">Unique identifier of the gateway</param>
        /// <returns>true/false</returns>
        [HttpDelete]
        [Route("Remove/{GatewayID}")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> RemoveGateway(long GatewayID)
        {
            return Ok(await _gatewayApplicationService.RemoveGateway(GatewayID));
        }

        #endregion


        #region POST | APIs

        /// <summary>
        /// Create a gateway
        /// </summary>
        /// <param name="Gateway">Gateway model</param>
        /// <returns>if created 1 else 0  </returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> CreateGateway(Gateway Gateway)
        {
            return Ok(await _gatewayApplicationService.CreateGateway(Gateway));
        }


        #endregion


        #endregion


    }
}
