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
        /// Update the Gateway
        /// </summary>
        /// <param name="UpdateGateway">Gateway Model</param>
        /// <returns>No of records updated</returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> UpdateGateway(UpdateGateway UpdateGateway)
        {
            return Ok(await _gatewayApplicationService.UpdateGateway(UpdateGateway));
        }

        /// <summary>
        /// Bulk update Gateways
        /// </summary>
        /// <param name="UpdateGateways">Bulk Update Gateway Model</param>
        /// <returns>Result of the bulk Update</returns>
        [HttpPut]
        [Route("BulkUpdate")]
        [ResponseType(typeof(List<GatewayBulkResponse>))]
        public async Task<IHttpActionResult> BulkUpdateGateway(List<UpdateGateway> UpdateGateways)
        {
            return Ok(await _gatewayApplicationService.BulkUpdateGateway(UpdateGateways));
        }


        /// <summary>
        /// Assigns gateway to the specified network
        /// </summary>
        /// <param name="GatewayID">Identifier of gateway to move</param>
        /// <param name="NetworkID">Identifier of network on your account</param>
        /// <returns>Success/Failure</returns>
        [HttpPut]
        [Route("{GatewayID}/AssignTo")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> AssignGateway(long GatewayID, long NetworkID)
        {
            return Ok(await _gatewayApplicationService.AssignGateway(GatewayID, NetworkID));
        }

        /// <summary>
        /// Assigns gateways to the specified network
        /// </summary>
        /// <param name="GatewayBulkAssign">GatewayBulkAssign model</param>
        /// <returns>list of GatewayBulkResponse</returns>
        [HttpPut]
        [Route("BulkAssignTo")]
        [ResponseType(typeof(List<GatewayBulkResponse>))]
        public async Task<IHttpActionResult> BulkAssignGateway(GatewayBulkAssign GatewayBulkAssign)
        {
            return Ok(await _gatewayApplicationService.BulkAssignGateway(GatewayBulkAssign));
        }

        #endregion


        #region DELETE | APIs

        /// <summary>
        /// Removes the gateway object from the network.
        /// </summary>
        /// <param name="GatewayID">Unique identifier of the gateway</param>
        /// <returns>Success/Failure</returns>
        [HttpDelete]
        [Route("Remove/{GatewayID}")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> RemoveGateway(long GatewayID)
        {
            return Ok(await _gatewayApplicationService.RemoveGateway(GatewayID));
        }

        /// <summary>
        /// Removes the gateways from the network.
        /// </summary>
        /// <param name="GatewayIDs">Lis of gateway ids</param>
        /// <returns>list of GatewayBulkResponse model</returns>
        [HttpDelete]
        [Route("BulkRemove")]
        [ResponseType(typeof(List<GatewayBulkResponse>))]
        public async Task<IHttpActionResult> BulkRemoveGateway(List<long> GatewayIDs)
        {
            return Ok(await _gatewayApplicationService.BulkRemoveGateway(GatewayIDs));
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
