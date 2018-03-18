using Coopers.BusinessLayer.Database.APIClient.DTO;
using Coopers.BusinessLayer.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.APIClient.Location
{
    public interface IAccountLocationClient
    {

        /// <summary>
        /// Create a accountLocation record
        /// </summary>
        /// <param name="AccountLocation">AccountLocation model</param>
        /// <returns>Id of the newly created AccountLocation</returns>
        Task<long> CreateAccountLocation(AccountLocation AccountLocation);

        /// <summary>
        /// Get the accountLocation details by AccountID
        /// </summary>
        /// <param name="AccountID">Unique indentofier for the Account</param>
        /// <returns>AccountLocation Model</returns>
        Task<AccountLocation> GetAccountLocationByID(long AccountID);

        /// <summary>
        /// Update a AccountLocation record
        /// </summary>
        /// <param name="AccountLocation">AccountLocation model</param>
        /// <returns>No of records Update</returns>
        Task<int> UpdateAccountLocation(AccountLocation AccountLocation);
    }
}
