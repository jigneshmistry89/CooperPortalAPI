using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using Coopers.BusinessLayer.Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public interface INetworkApplicationService
    {

        /// <summary>
        /// Returns the list of networks that belong to user.
        /// </summary>
        /// <returns>returns the network list</returns>
        Task<List<Network>> GetNetworkList();

        /// <summary>
        /// Get the NetworkList for a User
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// <returns>List of Network</returns>
        Task<List<Network>> GetNetworkListByUser(string UserName);

        /// <summary>
        /// Adds new wireless sensor network to account
        /// </summary>
        /// <param name="Network">Network create model</param>
        /// <returns>unique identifier for created network</returns>
        Task<long> CreateNetwork(CreateNetwork Network);


        /// <summary>
        /// Removes the network from the system.
        /// </summary>
        /// <param name="Name">Unique identifier of the network</param>
        /// <returns>Success/Failure</returns>
        Task<object> DeleteNetwork(long NetworkID);

        /// <summary>
        /// Update the Network and the NetworkLocation record
        /// </summary>
        /// <param name="Network">Network info to update</param>
        /// <returns>No of records</returns>
        Task<int> UpdateNetwork(UpdateNetwork Network);

    }
}
