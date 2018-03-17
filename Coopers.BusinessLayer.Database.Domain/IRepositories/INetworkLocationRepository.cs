using Coopers.BusinessLayer.Database.Domain.Models;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.Domain.IRepositories
{
    public interface INetworkLocationRepository : IRepository<NetworkLocation, long>
    {
        /// <summary>
        /// Update the Entity
        /// </summary>
        /// <param name="NetworkLocation">Model</param>
        /// <returns>No of records updated</returns>
        Task<int> UpdateNetworkLocation(NetworkLocation NetworkLocation);
    }
}
