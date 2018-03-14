using Coopers.BusinessLayer.Database.APIClient.DTO;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.APIClient.Location
{
    public class LocationClient : ILocationClient
    {
        private string LocationEndPoint = ConfigurationManager.AppSettings["MicroServiceAPIEndpoint"] + "locations/";

        /// <summary>
        /// Get the location detials by ID
        /// </summary>
        /// <param name="ID">Unique identifier of the location</param>
        /// <returns>LocationDTO</returns>
        public async Task<LocationDTO> GetLocationByID(long ID)
        {
            LocationDTO res = new LocationDTO();

            HttpResponseMessage response = await new HttpClient().GetAsync(LocationEndPoint + "/" + ID);
            if (response.IsSuccessStatusCode)
            {
                res = (await response.Content.ReadAsAsync<LocationDTO>());
            }
            return await Task.FromResult(res);
        }


        /// <summary>
        /// Get the all the location detials for a user 
        /// </summary>
        /// <param name="ID">Unique identifier of the user</param>
        /// <returns>List of LocationDTO</returns>
        public async Task<List<LocationDTO>> GetLocationByUserID(long ID)
        {
            List<LocationDTO> res = new List<LocationDTO>();

            HttpResponseMessage response = await new HttpClient().GetAsync(LocationEndPoint + "UserLocations/" + ID);
            if (response.IsSuccessStatusCode)
            {
                res = (await response.Content.ReadAsAsync<List<LocationDTO>>());
            }
            return await Task.FromResult(res);
        }


    }
}
