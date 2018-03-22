using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Coopers.BusinessLayer.Model.DTO;

namespace Coopers.BusinessLayer.Database.APIClient
{
    public class TaxableStateClient : ITaxableStateClient
    {

        #region PRIVATE MEMBER

        private string TaxableStateClientEndPoint = "TaxableState/";
        private readonly IHttpService _httpService;

        #endregion


        #region CONSTRUCTOR

        public TaxableStateClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion


        #region PUBLIC MEMBERS

        public async Task<TaxableStates> GetTaxableStatebyStateCode(string StateCode)
        {
            try
            {
                return await _httpService.GetAsAsync<TaxableStates>(TaxableStateClientEndPoint, string.Format("StateCode={0}", StateCode));
            }
            catch (Exception)
            {
                return null;
            }
             
        }

        #endregion

    }

}
