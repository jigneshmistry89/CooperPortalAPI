using Coopers.BusinessLayer.Database.APIClient.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.APIClient.Location
{
    public interface ILocationClient
    {

        /// <summary>
        /// Get the location detials by ID
        /// </summary>
        /// <param name="ID">Unique identifier of the location</param>
        /// <returns>LocationDTO</returns>
        Task<LocationDTO> GetLocationByID(long ID);

        /// <summary>
        /// Get the all the location detials for a user 
        /// </summary>
        /// <param name="ID">Unique identifier of the user</param>
        /// <returns>List of LocationDTO</returns>
        Task<List<LocationDTO>> GetLocationByUserID(long ID);

    }
}
