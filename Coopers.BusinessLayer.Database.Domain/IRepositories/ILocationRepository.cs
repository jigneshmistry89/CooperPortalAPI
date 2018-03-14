using Coopers.BusinessLayer.Database.Domain.Models;
using Coopers.BusinessLayer.Database.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.Domain.IRepositories
{
    public interface ILocationRepository : IRepository<Location, long>
    {

        Task<LocationDTO> GetLocationByID(long ID);

        Task<List<LocationDTO>> GetLocationByUserID(long ID);

    }
}
