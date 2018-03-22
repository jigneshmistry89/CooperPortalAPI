using Coopers.BusinessLayer.Database.APIClient.DTO;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace Coopers.BusinessLayer.Database.APIClient.Location
{
    public class NetworkLocationClient : INetworkLocationClient
    {

        #region PRIVATE MEMBER

        private string NetowrkLocationEndPoint = "NetworkLocation/";
        private readonly IHttpService _httpService;

        #endregion

        #region CONSTRUCTOR

        public NetworkLocationClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion

        #region PUBLIC MEMBERS

        /// <summary>
        ///  Get the networkLocation details by CSNetID
        /// </summary>
        /// <param name="CSNetID">NetworkID</param>
        /// <returns>Networklocation Model</returns>
        public async Task<NetworkLocation> GetNetworkLocationByID(long CSNetID)
        {
            return await _httpService.GetAsAsync<NetworkLocation>(NetowrkLocationEndPoint + "/" + CSNetID,"");
        }


        /// <summary>
        /// Create a NewtworkLocation in Db
        /// </summary>
        /// <param name="NetworkLocation">NetworkLocation model</param>
        /// <returns>Id of the created Networklocation</returns>
        public async Task<long> CreateNetworkLocation(NetworkLocation NetworkLocation)
        {
            return await _httpService.PostAsAsync<long>(NetowrkLocationEndPoint, NetworkLocation);
        }


        /// <summary>
        /// Update a NewtworkLocation in Db
        /// </summary>
        /// <param name="NetworkLocation">NetworkLocation model</param>
        /// <returns>No of records updated</returns>
        public async Task<int> UpdateNetworkLocation(NetworkLocation NetworkLocation)
        {
            return await _httpService.PutAsAsync<int>(NetowrkLocationEndPoint, NetworkLocation);
        }

        #endregion

    }

}
