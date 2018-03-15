using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient;
using Coopers.BusinessLayer.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public class GatewayApplicationService : IGatewayApplicationService
    {


        #region PRIVATE MEMBERS

        private readonly IGatewayClient _gatewayClient;

        #endregion


        #region CONSTRUCTOR

        public GatewayApplicationService(IGatewayClient gatewayClient)
        {
            _gatewayClient = gatewayClient;
        }

        #endregion


        #region PUBLIC MEMBERS     

        /// <summary>
        /// Returns the list of gateways that belongs to user.
        /// </summary>
        /// <param name="Name">Filters list to gateways with names containing this string. (case-insensitive)</param>
        /// <param name="NetworkID">Filters list to gateway that belong to this network id</param>
        /// <param name="ApplicationID">Filters list to gateway that are this application type</param>
        /// <param name="Status">Filters list to gateway that match this status</param>
        /// <returns>gateway list</returns>
        public async Task<object> GetGatewayList(string Name = "", long NetworkID = 0, short ApplicationID = 0, short Status = 0)
        {
            return await _gatewayClient.GetGatewayList("",Name, NetworkID, ApplicationID, Status);
        }

        /// <summary>
        /// Assigns gateway to the specified network
        /// </summary>
        /// <param name="GatewayID">Identifier of gateway to move</param>
        /// <param name="NetworkID">Identifier of network on your account</param>
        /// <returns>Success/Failure</returns>
        public async Task<string> AssignGateway(long GatewayID, long NetworkID)
        {
            var CheckDigit = CheckDigitGenerator.GenerateSecurityCode(GatewayID.ToString());
            return await _gatewayClient.AssignGateway(GatewayID, NetworkID, CheckDigit);
        }

        /// <summary>
        /// Assigns gateways to the specified network
        /// </summary>
        /// <param name="GatewayBulkAssign">GatewayBulkAssign model</param>
        /// <returns>list of GatewayBulkResponse</returns>
        public async Task<List<GatewayBulkResponse>> BulkAssignGateway(GatewayBulkAssign GatewayBulkAssign)
        {
            List<GatewayBulkResponse> response = new List<GatewayBulkResponse>();

            foreach (var gatewayID in GatewayBulkAssign.GatewayIDs)
            {
                response.Add(new GatewayBulkResponse() { GatewayID = gatewayID, Result = await AssignGateway(gatewayID, GatewayBulkAssign.NetworkID) });
            }

            return response;
        }


        /// <summary>
        /// Removes the gateway object from the network.
        /// </summary>
        /// <param name="GatewayID">Unique identifier of the gateway</param>
        /// <returns>true/false</returns>
        public async Task<string> RemoveGateway(long GatewayID)
        {
            return await _gatewayClient.RemoveGateway(GatewayID);
        }

        /// <summary>
        /// Removes the gateway object from the network.
        /// </summary>
        /// <param name="GatewayID">List of the gateway ids</param>
        /// <returns>list of GatewayBulkResponse mdoel</returns>
        public async Task<List<GatewayBulkResponse>> BulkRemoveGateway(List<long> GatewayIDs)
        {
            List<GatewayBulkResponse> response = new List<GatewayBulkResponse>();

            foreach (var gatwayID in GatewayIDs)
            {
                response.Add(new GatewayBulkResponse() { GatewayID = gatwayID, Result = await RemoveGateway(gatwayID) });
            }

            return response;
        }


        /// <summary>
        /// Create a gateway
        /// </summary>
        /// <param name="Gateway">Gateway model</param>
        /// <returns>if created 1 else 0  </returns>
        public async Task<int> CreateGateway(Gateway Gateway)
        {
            return await _gatewayClient.CreateGateway(Gateway);
        }

        /// <summary>
        /// Update the Gateway
        /// </summary>
        /// <param name="UpdateGateway">Gateway Model</param>
        /// <returns>No of records updated</returns>
        public async Task<int> UpdateGateway(UpdateGateway UpdateGateway)
        {
            return await _gatewayClient.UpdateGateway(UpdateGateway);
        }

        /// <summary>
        /// Update the Gateways
        /// </summary>
        /// <param name="UpdateGateways">List of the updateGateway model</param>
        /// <returns>List of GatewayBulkResponse</returns>
        public async Task<List<GatewayBulkResponse>> BulkUpdateGateway(List<UpdateGateway> UpdateGateways)
        {
            List<GatewayBulkResponse> response = new List<GatewayBulkResponse>();

            foreach (var updateGateway in UpdateGateways)
            {
                response.Add(new GatewayBulkResponse() { GatewayID = updateGateway.GatewayID, Result = await UpdateGateway(updateGateway) == 1 ? "Success" : "Failure" });
            }

            return response;
        }

        #endregion


        #region PRIVATE MEMBERS     


        #endregion


    }
}
