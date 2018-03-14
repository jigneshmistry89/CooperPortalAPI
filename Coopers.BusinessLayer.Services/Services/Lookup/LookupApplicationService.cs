using Coopers.BusinessLayer.NotifEye.APIClient;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public class LookupApplicationService : ILookupApplicationService
    {

        #region PRIVATE MEMBERS

        private readonly ILookupClient _lookupClient;

        #endregion


        #region CONSTRUCTOR

        public LookupApplicationService(ILookupClient lookupClient)
        {
            _lookupClient = lookupClient;
        }

        #endregion


        #region PUBLIC MEMBERS     

        /// <summary>
        /// Returns the list of supported Application Names and Id's.
        /// </summary>
        /// <returns>application info</returns>
        public async Task<object> GetApplications()
        {
            return await _lookupClient.GetApplications();
        }

        #endregion


        #region PRIVATE MEMBERS     


        #endregion


    }
}
