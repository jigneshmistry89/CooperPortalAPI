using Coopers.BusinessLayer.NotifEye.APIClient.HttpService;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient
{
    public class LookupClient : ILookupClient
    {

        #region PRIVATE MEMBERS

        private readonly IHttpService _httpService;

        #endregion


        #region CONSTRUCTOR

        public LookupClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion


        #region NOTIFEYEAPI

        /// <summary>
        /// Returns the list of supported Application Names and Id's.
        /// </summary>
        /// <returns>application info</returns>
        public async Task<object> GetApplications()
        {
            return await _httpService.GetAsAsync<object>("GetApplicationID", "", false,true); 
        }

        #endregion

        #region PRIVATE MEMBERS    

        #endregion


    }
}
