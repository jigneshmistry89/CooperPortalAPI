using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public interface ILocationApplicationService
    {
        /// <summary>
        /// Get the Location Summary for a given Location ID
        /// </summary>
        /// <param name="LocationID">Unique ID of the location</param>
        /// <returns>LocationSummary</returns>
        Task<LocationSummary> GetLocationSummaryByID(long ID);

        /// <summary>
        /// Get the location summary for a give user
        /// </summary>
        /// <returns>List of UserLocationSummary</returns>
        Task<List<UserLocationSummary>> GetUserLocationSummary();

        /// <summary>
        /// Get the Location details.
        /// </summary>
        /// <param name="LocationID">Unique ID of the location</param>
        /// <returns>LocationDetails</returns>
        Task<LocationDetails> GetLocationDetails(int ID);

    }
}
