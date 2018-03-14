using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public interface INetworkClient
    {
  
        /// <summary>
        /// Returns the list of networks that belong to user.
        /// </summary>
        /// <returns>returns the network list</returns>
        Task<List<Network>> GetNetworkList();

        /// <summary>
        /// Get the newtowrks for a given account
        /// </summary>
        /// <param name="AccountID">Unique identifier for the account</param>
        /// <returns>List of network</returns>
        Task<List<Model.DTO.Network>> GetNetworkListByAccountID(long AccountID);

        /// <summary>
        /// Adds new wireless sensor network to account
        /// </summary>
        /// <param name="Name">Name of network to be added</param>
        /// <returns>Network model</returns>
        Task<Network> CreateNetwork(string Name);

        /// <summary>
        /// Removes the network from the system.
        /// </summary>
        /// <param name="Name">Unique identifier of the network</param>
        /// <returns>Success/Failure</returns>
        Task<object> DeleteNetwork(long NetworkID);

    }
}
