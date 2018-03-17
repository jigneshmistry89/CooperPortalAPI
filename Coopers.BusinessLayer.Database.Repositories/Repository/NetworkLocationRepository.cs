using Coopers.BusinessLayer.Database.Domain.IRepositories;
using Coopers.BusinessLayer.Database.Domain.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace Coopers.BusinessLayer.Database.Repositories.Repository
{
    public class NetworkLocationRepository : Repository<NetworkLocation, long>, INetworkLocationRepository
    {


        #region PRIVATE VARIABLE

        private readonly DbSet<NetworkLocation> _dbset;
        private readonly DbContext _context;

        #endregion


        #region CONSTRUCTOR

        public NetworkLocationRepository(DbContext context) : base(context)
        {
            _context = context;
            _dbset = context.Set<NetworkLocation>();
        }

        #endregion


        #region INTERFACE IMPLEMENTATION          

        /// <summary>
        /// Update the Entity
        /// </summary>
        /// <param name="NetworkLocation">Model</param>
        /// <returns>No of records updated</returns>
        public async Task<int> UpdateNetworkLocation(NetworkLocation NetworkLocation)
        {
            //Get the Entity
            var entity = _dbset.Where(x => x.CSNetID == NetworkLocation.CSNetID).FirstOrDefault();

            //Modify the entity
            if (entity != null)
            {
                entity.Address = NetworkLocation.Address;
                entity.Address2 = NetworkLocation.Address2;
                entity.City = NetworkLocation.City;
                entity.Country = NetworkLocation.Country;
                entity.IsActive = NetworkLocation.IsActive;
                entity.Latitude = NetworkLocation.Latitude;
                entity.Longitude = NetworkLocation.Longitude;
                entity.Name = NetworkLocation.Name;
                entity.PostalCode = NetworkLocation.PostalCode;
                entity.State = NetworkLocation.State;

                //Update the entiry
                return await UpdateEntityAsync(entity);
            }

            return 0;
        }

        #endregion


        #region OVERRIDEN IMPLEMENTATION

        public override string GetPrimaryKeyColumnName()
        {
            return "CSNetID";
        }

        #endregion


    }
}
