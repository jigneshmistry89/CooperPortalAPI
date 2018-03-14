using Coopers.BusinessLayer.Model.DTO;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public interface IGatewayApplicationService
    {

        /// <summary>
        /// Returns the list of gateways that belongs to user.
        /// </summary>
        /// <param name="Name">Filters list to gateways with names containing this string. (case-insensitive)</param>
        /// <param name="NetworkID">Filters list to gateway that belong to this network id</param>
        /// <param name="ApplicationID">Filters list to gateway that are this application type</param>
        /// <param name="Status">Filters list to gateway that match this status</param>
        /// <returns>gateway list</returns>
        Task<object> GetGatewayList(string Name = "", long NetworkID = 0, short ApplicationID = 0, short Status = 0);

        /// <summary>
        /// Assigns gateway to the specified network
        /// </summary>
        /// <param name="GatewayID">Identifier of gateway to move</param>
        /// <param name="NetworkID">Identifier of network on your account</param>
        /// <returns>true/false</returns>
        Task<bool> AssignGateway(long GatewayID, long NetworkID);

        /// <summary>
        /// Removes the gateway object from the network.
        /// </summary>
        /// <param name="SensorID">Unique identifier of the gateway</param>
        /// <returns>true/false</returns>
        Task<bool> RemoveGateway(long GatewayID);

        /// <summary>
        /// Create a gateway
        /// </summary>
        /// <param name="Gateway">Gateway model</param>
        /// <returns>if created 1 else 0  </returns>
        Task<int> CreateGateway(Gateway Gateway);

    }
}
