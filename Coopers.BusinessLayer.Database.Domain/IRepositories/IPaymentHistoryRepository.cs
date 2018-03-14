using Coopers.BusinessLayer.Database.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.Domain.IRepositories
{

    public interface IPaymentHistoryRepository : IRepository<PaymentHistory, long>
    {
        Task<List<PaymentHistory>> GetPaymentHistoryList(long AccountID);
    }
}
