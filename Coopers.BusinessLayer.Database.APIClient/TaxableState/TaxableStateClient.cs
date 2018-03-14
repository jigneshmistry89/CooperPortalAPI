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

        private string TaxableStateClientEndPoint = ConfigurationManager.AppSettings["MicroServiceAPIEndpoint"] + "TaxableState/";

        #endregion


        #region PUBLIC MEMBERS

        public async Task<TaxableStates> GetTaxableStatebyStateCode(string StateCode)
        {
            HttpResponseMessage response = await new HttpClient().GetAsync(TaxableStateClientEndPoint + "?StateCode=" + StateCode);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadAsAsync<TaxableStates>());
            }
            else
            {
                return null;
            }
        }


        #endregion

    }

}
