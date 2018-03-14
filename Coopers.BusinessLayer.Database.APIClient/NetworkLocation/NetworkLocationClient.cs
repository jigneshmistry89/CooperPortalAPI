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

        private string NetowrkLocationEndPoint = ConfigurationManager.AppSettings["MicroServiceAPIEndpoint"] + "NetworkLocation/";

        #endregion

        #region PUBLIC MEMBERS

        /// <summary>
        ///  Get the networkLocation details by CSNetID
        /// </summary>
        /// <param name="CSNetID">NetworkID</param>
        /// <returns>Networklocation Model</returns>
        public async Task<NetworkLocation> GetNetworkLocationByID(long CSNetID)
        {
            NetworkLocation res = new NetworkLocation();

            HttpResponseMessage response = await new HttpClient().GetAsync(NetowrkLocationEndPoint + "/" + CSNetID);
            if (response.IsSuccessStatusCode)
            {
                res = (await response.Content.ReadAsAsync<NetworkLocation>());
            }
            return await Task.FromResult(res);
        }

        /// <summary>
        /// Create a NewtworkLocation in Db
        /// </summary>
        /// <param name="NetworkLocation">NetworkLocation model</param>
        /// <returns>Id of the created Networklocation</returns>
        public async Task<long> CreateNetworkLocation(NetworkLocation NetworkLocation)
        {
            long res = 0;
            HttpResponseMessage response = await new HttpClient().PostAsJsonAsync(NetowrkLocationEndPoint, NetworkLocation);
            if (response.IsSuccessStatusCode)
            {
                res = (await response.Content.ReadAsAsync<long>());
            }
            return await Task.FromResult(res);
        }

        #endregion

    }

}
