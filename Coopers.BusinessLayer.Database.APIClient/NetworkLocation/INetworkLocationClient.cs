using Coopers.BusinessLayer.Database.APIClient.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.APIClient.Location
{
    public interface INetworkLocationClient
    {

        /// <summary>
        ///  Get the networkLocation details by CSNetID
        /// </summary>
        /// <param name="CSNetID">NetworkID</param>
        /// <returns>Networklocation Model</returns>
        Task<NetworkLocation> GetNetworkLocationByID(long CSNetID);

        /// <summary>
        /// Create a NewtworkLocation in Db
        /// </summary>
        /// <param name="NetworkLocation">NetworkLocation model</param>
        /// <returns>Id of the created Networklocation</returns>
        Task<long> CreateNetworkLocation(NetworkLocation NetworkLocation);
    }
}
