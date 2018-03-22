using AutoMapper;
using Coopers.BusinessLayer.Database.APIClient.DTO;
using Coopers.BusinessLayer.Database.APIClient.Location;
using Coopers.BusinessLayer.Model.Interface;
using Coopers.BusinessLayer.NotifEye.APIClient;
using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using Coopers.BusinessLayer.Services.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public class NetworkApplicationService : INetworkApplicationService
    {


        #region PRIVATE MEMBERS

        private readonly INetworkClient _networkClient;
        private readonly IMapper _mapper;
        private readonly INetworkLocationClient _networkLocationClient;

        #endregion


        #region CONSTRUCTOR

        public NetworkApplicationService(INetworkClient networkClient, IMapper mapper, INetworkLocationClient networkLocationClient)
        {
            _networkClient = networkClient;
            _networkLocationClient = networkLocationClient;
            _mapper = mapper;
        }

        #endregion


        #region PUBLIC MEMBERS     


        /// <summary>
        /// Returns the list of networks that belong to user.
        /// </summary>
        /// <returns>returns the network list</returns>
        public async Task<List<Network>> GetNetworkList()
        {
            return await _networkClient.GetNetworkList();
        }


        /// <summary>
        /// Get the NetworkList for a User
        /// </summary>
        /// <param name="UserName">UserName</param>
        /// <returns>List of Network</returns>
        public async Task<List<Network>> GetNetworkListByUser(string UserName)
        {
            return await _networkClient.GetNetworkListByUser(UserName);
        }


        /// <summary>
        /// Adds new wireless sensor network to account
        /// </summary>
        /// <param name="Network">model of the newtork to be created</param>
        /// <returns>unique identifier for created network</returns>
        public async Task<long> CreateNetwork(CreateNetwork Network)
        {
            //Create a nework using the notifEye DB
            var network = await _networkClient.CreateNetwork(Network.Name);

            //prepare the networklocation model.
            var netLocation = _mapper.Map<NetworkLocation>(Network);
            netLocation.CSNetID = network.NetworkID;

            //create a Networklocation in DB
            await _networkLocationClient.CreateNetworkLocation(netLocation);

            return network.NetworkID;
        }


        /// <summary>
        /// Removes the network from the system.
        /// </summary>
        /// <param name="Name">Unique identifier of the network</param>
        /// <returns>Success/Failure</returns>
        public async Task<object> DeleteNetwork(long NetworkID)
        {
            return await _networkClient.DeleteNetwork(NetworkID);
        }


        /// <summary>
        /// Update the Network and the NetworkLocation record
        /// </summary>
        /// <param name="Network">Network info to update</param>
        /// <returns>No of records</returns>
        public async Task<int> UpdateNetwork(UpdateNetwork Network)
        {
            //prepare the networklocation model.
            var network = _mapper.Map<Model.DTO.Network>(Network);

            //Update the Network 
            var result = await _networkClient.UpdateNetwork(network);

            if (result> 0)
            {
                //prepare the networklocation model.
                var netLocation = _mapper.Map<NetworkLocation>(Network);

                //create a Networklocation in DB
                result  = await _networkLocationClient.UpdateNetworkLocation(netLocation);
            }

            return result;
        }


        #endregion


        #region PRIVATE MEMBERS     


        #endregion


    }
}
