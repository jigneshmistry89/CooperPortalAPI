using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public interface ILookupApplicationService
    {

        /// <summary>
        /// Returns the list of supported Application Names and Id's.
        /// </summary>
        /// <returns>application info</returns>
        Task<object> GetApplications();

    }
}
