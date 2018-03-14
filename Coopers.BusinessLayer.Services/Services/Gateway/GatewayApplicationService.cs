using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient;
using Coopers.BusinessLayer.Utility;
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
            return await _gatewayClient.GetGatewayList(Name, NetworkID, ApplicationID, Status);
        }

        /// <summary>
        /// Assigns gateway to the specified network
        /// </summary>
        /// <param name="GatewayID">Identifier of gateway to move</param>
        /// <param name="NetworkID">Identifier of network on your account</param>
        /// <returns>true/false</returns>
        public async Task<bool> AssignGateway(long GatewayID, long NetworkID)
        {
            var CheckDigit = CheckDigitGenerator.GenerateSecurityCode(GatewayID.ToString());
            return await _gatewayClient.AssignGateway(GatewayID, NetworkID, CheckDigit);
        }

        /// <summary>
        /// Removes the gateway object from the network.
        /// </summary>
        /// <param name="GatewayID">Unique identifier of the gateway</param>
        /// <returns>true/false</returns>
        public async Task<bool> RemoveGateway(long GatewayID)
        {
            return await _gatewayClient.RemoveGateway(GatewayID);
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

        #endregion


        #region PRIVATE MEMBERS     


        #endregion


    }
}
