using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public interface ILookupClient
    {

        /// <summary>
        /// Returns the list of supported Application Names and Id's.
        /// </summary>
        /// <returns>application info</returns>
        Task<object> GetApplications();


    }
}
