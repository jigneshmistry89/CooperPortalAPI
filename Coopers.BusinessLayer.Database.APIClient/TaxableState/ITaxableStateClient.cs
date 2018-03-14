using Coopers.BusinessLayer.Model.DTO;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.APIClient
{
    public interface ITaxableStateClient
    {

        /// <summary>
        /// Get the State by the StateCode
        /// </summary>
        /// <param name="StateCode">StateCode</param>
        /// <returns>TaxableState model</returns>
        Task<TaxableStates> GetTaxableStatebyStateCode(string StateCode);

    }
}
