using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.HttpService;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        #endregion


        #endregion


        #region PRIVATE MEMBERS     


        #endregion


    }
}
