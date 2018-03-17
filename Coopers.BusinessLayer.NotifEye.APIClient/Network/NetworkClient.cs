using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.HttpService;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public class NetworkClient : INetworkClient
    {


        #region PRIVATE MEMBERS

        private readonly IHttpService _httpService;

        #endregion


        #region CONSTRUCTOR

        public NetworkClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion


        #region PUBLIC MEMBERS     


        #region NOTIFEYEAPI

        /// <summary>
        /// Returns the list of networks that belong to user.
        /// </summary>
        /// <returns>returns the network list</returns>
        public async Task<List<Network>> GetNetworkList()
        {
            return await _httpService.GetAsAsync<List<Network>>("NetworkList", "", false, false);
        }


        /// <summary>
        /// Adds new wireless sensor network to account
        /// </summary>
        /// <param name="Name">Name of network to be added</param>
        /// <returns>Network model</returns>
        public async Task<Network> CreateNetwork(string Name)
        {
            return await _httpService.GetAsAsync<Network>("CreateNetwork2", string.Format("name={0}", Name), false, false);
        }


        /// <summary>
        /// Removes the network from the system.
        /// </summary>
        /// <param name="Name">Unique identifier of the network</param>
        /// <returns>Success/Failure</returns>
        public async Task<object> DeleteNetwork(long NetworkID)
        {
            return await _httpService.GetAsAsync<string>("RemoveNetwork", string.Format("networkID={0}", NetworkID), false, false);
        }

        #endregion


        #region INTEGRATED API

        /// <summary>
        /// Get the newtowrks for a given account
        /// </summary>
        /// <param name="AccountID">Unique identifier for the account</param>
        /// <returns>List of network</returns>
        public async Task<List<Model.DTO.Network>> GetNetworkListByAccountID(long AccountID)
        {
            return await _httpService.GetAsAsync<List<Model.DTO.Network>>("csnet/GetNetworkListByAccountID", string.Format("AccountID={0}", AccountID), true, false);
        }

        /// <summary>
        /// Get the network record by ID
        /// </summary>
        /// <param name="NetworkID">unique idetifier for the Network</param>
        /// <returns>Network model</returns>
        public async Task<Model.DTO.Network> GetNetworkByID(long NetworkID)
        {
            var netWork = await _httpService.GetAsAsync<List<Model.DTO.Network>>("csnet/GetNetworkByID", string.Format("NetworkID={0}", NetworkID), true, false);

            if(netWork != null && netWork.Count > 0)
            {
                return netWork[0];
            }

            return null;
        }

        /// <summary>
        /// Update the Network record
        /// </summary>
        /// <param name="Network">Network info to update</param>
        /// <returns>No of records updated</returns>
        public async Task<int> UpdateNetwork(Model.DTO.Network Network)
        {
            string methodName = string.Format("csnet/UpdateGateway?NetworkID={0}&Name={1}&SendNotification={2}", Network.CSNetID, Network.Name, Network.SendNotifications);
            return await _httpService.PutAsAsync<int>(methodName, null);
        }

        #endregion


        #endregion


        #region PRIVATE MEMBERS     


        #endregion


    }
}
