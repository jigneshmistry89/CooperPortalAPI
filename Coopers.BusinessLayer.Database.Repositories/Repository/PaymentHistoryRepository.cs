using Coopers.BusinessLayer.Database.Domain.IRepositories;
using Coopers.BusinessLayer.Database.Domain.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace Coopers.BusinessLayer.Database.Repositories.Repository
{
    public class PaymentHistoryRepository : Repository<PaymentHistory, long>, IPaymentHistoryRepository
    {


        #region PRIVATE VARIABLE

        private readonly DbSet<PaymentHistory> _dbset;
        private readonly DbContext _context;

        #endregion


        #region CONSTRUCTOR

        public PaymentHistoryRepository(DbContext context) : base(context)
        {
            _context = context;
            _dbset = context.Set<PaymentHistory>();
        }

        #endregion


        #region INTERFACE IMPLEMENTATION          

        public async Task<List<PaymentHistory>> GetPaymentHistoryList(int Offset,int PageSize,long AccountID)
        {
            return await _dbset.Where(x=>x.AccountID == AccountID).OrderByDescending(x => x.ID).Skip(Offset).Take(PageSize).ToListAsync();
        }

        #endregion


        #region OVERRIDEN IMPLEMENTATION

        #endregion


    }
}
