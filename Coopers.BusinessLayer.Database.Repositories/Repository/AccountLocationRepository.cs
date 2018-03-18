using Coopers.BusinessLayer.Database.Domain.IRepositories;
using Coopers.BusinessLayer.Database.Domain.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace Coopers.BusinessLayer.Database.Repositories.Repository
{
    public class AccountLocationRepository : Repository<AccountLocation, long>, IAccountLocationRepository
    {


        #region PRIVATE VARIABLE

        private readonly DbSet<AccountLocation> _dbset;
        private readonly DbContext _context;

        #endregion


        #region CONSTRUCTOR

        public AccountLocationRepository(DbContext context) : base(context)
        {
            _context = context;
            _dbset = context.Set<AccountLocation>();
        }

        #endregion


        #region INTERFACE IMPLEMENTATION          

        public async Task<AccountLocation> GetAccountLocationByID(long AccountID)
        {
            if (_dbset.Any(x => x.AccountID == AccountID))
            {
                return await GetEntityByIdAsync(AccountID);
            }
            else
            {
                return null;
            }
        }

        public async Task<int> UpdateAccountLocation(AccountLocation AccountLocation)
        {
            if (_dbset.Any(x => x.AccountID == AccountLocation.AccountID))
            {
                return await UpdateEntityAsync(AccountLocation);
            }
            else
            {
                return 0;
            }
        }
        
        #endregion


        #region OVERRIDEN IMPLEMENTATION

        public override string GetPrimaryKeyColumnName()
        {
            return "AccountID";
        }

        #endregion


    }
}
