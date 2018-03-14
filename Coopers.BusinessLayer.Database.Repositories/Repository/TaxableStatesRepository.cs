using Coopers.BusinessLayer.Database.Domain.IRepositories;
using Coopers.BusinessLayer.Database.Domain.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Coopers.BusinessLayer.Database.Repositories.Repository
{
    public class TaxableStatesRepository : Repository<TaxableStates, long>, ITaxableStatesRepository
    {


        #region PRIVATE VARIABLE

        private readonly DbSet<TaxableStates> _dbset;
        private readonly DbContext _context;

        #endregion


        #region CONSTRUCTOR

        public TaxableStatesRepository(DbContext context) : base(context)
        {
            _context = context;
            _dbset = context.Set<TaxableStates>();
        }

        #endregion


        #region INTERFACE IMPLEMENTATION          

        public async Task<TaxableStates> GetTaxableStatebyStateCode(string StateCode)
        {
            TaxableStates ts = await _dbset.Where(x => x.StateCode == StateCode || x.StateName == StateCode).FirstOrDefaultAsync();
            if(ts != null)
            {
                return ts;
            }
            else
            {
                throw new Exception("Unable to find the State with given StateCode");
            }
        }

        #endregion


        #region OVERRIDEN IMPLEMENTATION

        #endregion


    }
}
