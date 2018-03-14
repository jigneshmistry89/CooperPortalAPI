using Coopers.BusinessLayer.Database.Domain.Models;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.Domain.IRepositories
{
    public interface ITaxableStatesRepository : IRepository<TaxableStates, long>
    {

        /// <summary>
        /// Get the State by the StateCode
        /// </summary>
        /// <param name="StateCode">StateCode</param>
        /// <returns>TaxableState model</returns>
        Task<TaxableStates> GetTaxableStatebyStateCode(string StateCode);
    }
}
