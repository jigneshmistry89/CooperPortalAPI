using Coopers.BusinessLayer.Database.Domain.IRepositories;
using Coopers.BusinessLayer.Database.Domain.Models;
using System.Data.Entity;

namespace Coopers.BusinessLayer.Database.Repositories.Repository
{
    public class NetworkLocationRepository : Repository<NetworkLocation, long>, INetworkLocationRepository
    {


        #region PRIVATE VARIABLE

        private readonly DbSet<Location> _dbset;
        private readonly DbContext _context;

        #endregion


        #region CONSTRUCTOR

        public NetworkLocationRepository(DbContext context) : base(context)
        {
            _context = context;
            _dbset = context.Set<Location>();
        }

        #endregion


        #region INTERFACE IMPLEMENTATION          

        #endregion


        #region OVERRIDEN IMPLEMENTATION

        public override string GetPrimaryKeyColumnName()
        {
            return "CSNetID";
        }

        #endregion


    }
}
