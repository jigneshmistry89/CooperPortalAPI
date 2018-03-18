using Coopers.BusinessLayer.Database.Domain.Models;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.Domain.IRepositories
{
    public interface IAccountLocationRepository : IRepository<AccountLocation, long>
    {
        Task<int> UpdateAccountLocation(AccountLocation AccountLocation);

        Task<AccountLocation> GetAccountLocationByID(long AccountID);
    }
}
