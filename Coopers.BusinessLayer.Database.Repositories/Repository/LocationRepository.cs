using Coopers.BusinessLayer.Database.Domain.IRepositories;
using Coopers.BusinessLayer.Database.Domain.Models;
using Coopers.BusinessLayer.Database.Models.DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.Repositories.Repository
{
    public class LocationRepository : Repository<Location, long>, ILocationRepository
    {


        #region PRIVATE VARIABLE

        private readonly DbSet<Location> _dbset;
        private readonly DbContext _context;

        #endregion


        #region CONSTRUCTOR

        public LocationRepository(DbContext context) : base(context)
        {
            _context = context;
            _dbset = context.Set<Location>();
        }

        #endregion


        #region INTERFACE IMPLEMENTATION          

        public async Task<LocationDTO> GetLocationByID(long ID)
        {
            return await GetFilteredEntities()
                             .Where(location => location.ID == ID)
                             .Select(loc => new LocationDTO
                             {
                                 ID = loc.ID,
                                 AccountID = loc.AccountID,
                                 Address = loc.Address,
                                 Address2 = loc.Address2,
                                 City = loc.City,
                                 Country = loc.Country,
                                 Latitude = loc.Latitude,
                                 Longitude = loc.Longitude,
                                 NetworkIDs = loc.LocationNetworks.Select(nl => nl.NetworkID).ToList(),
                                 PostalCode = loc.PostalCode,
                                 ProfileName = loc.ProfileName,
                                 State = loc.State
                             }).FirstOrDefaultAsync();
        }


        public async Task<List<LocationDTO>> GetLocationByUserID(long ID)
        {
            return await GetFilteredEntities()
                             .Where(location => location.Account.Users.Any(user => user.CustomerID == ID))
                             .Select(loc => new LocationDTO
                             {
                                 ID = loc.ID,
                                 AccountID = loc.AccountID,
                                 Address = loc.Address,
                                 Address2 = loc.Address2,
                                 City = loc.City,
                                 Country = loc.Country,
                                 Latitude = loc.Latitude,
                                 Longitude = loc.Longitude,
                                 NetworkIDs = loc.LocationNetworks.Select(nl => nl.NetworkID).ToList(),
                                 PostalCode = loc.PostalCode,
                                 ProfileName = loc.ProfileName,
                                 State = loc.State
                             }).ToListAsync();
        }

        #endregion


        #region OVERRIDEN IMPLEMENTATION

        #endregion


    }
}
