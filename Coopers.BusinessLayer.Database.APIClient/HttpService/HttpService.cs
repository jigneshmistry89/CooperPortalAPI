using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.APIClient
{
    public class HttpService : IHttpService
    {

        #region PRIVATE MEMBERS

        #endregion


        #region CONSTRUCTOR

        public HttpService()
        {
        }

        #endregion

        public async Task<T> GetAsAsync<T>(string Method,string queryParam)
        {
            string Path = string.Format("{0}{1}?{2}",
                            ConfigurationManager.AppSettings["MicroServiceAPIEndpoint"], Method, queryParam);

            HttpResponseMessage response = await new HttpClient().GetAsync(Path);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return (await response.Content.ReadAsAsync<T>());
                }
                catch (Exception ex)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            else
            {
                throw PrepareHttpException(response, Method);
            }
        }

        public async Task<T> PostAsAsync<T>(string Method, object Body)
        {
            string path = string.Format("{0}{1}", ConfigurationManager.AppSettings["MicroServiceAPIEndpoint"], Method);

            var httpClient = new HttpClient();

            var response = await httpClient.PostAsync(path, new StringContent(JsonConvert.SerializeObject(Body),Encoding.UTF8,"application/json"));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return (await response.Content.ReadAsAsync<T>());
                }
                catch (Exception)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            else
            {
                throw PrepareHttpException(response, Method, await  response.Content.ReadAsStringAsync());
            }
        }

        public async Task<T> PutAsAsync<T>(string Method, object Body)
        {
            string path = string.Format("{0}{1}", ConfigurationManager.AppSettings["MicroServiceAPIEndpoint"], Method);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpClient.PutAsync(path, new StringContent(JsonConvert.SerializeObject(Body),Encoding.UTF8,"application/json"));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return (await response.Content.ReadAsAsync<T>());
                }
                catch (Exception)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
            else
            {
                throw PrepareHttpException(response, Method); ;
            }
        }

        public async Task<T> DeleteAsAsync<T>(string Method, string queryParam)
        {

            string Path = string.Format("{0}{1}?{2}",
                                ConfigurationManager.AppSettings["MicroServiceAPIEndpoint"], Method, queryParam);

            var httpClient = new HttpClient();
           
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpClient.DeleteAsync(Path);
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadAsAsync<T>());
            }
            else
            {
                throw PrepareHttpException(response, Method);
            }
        }

        #region PRIVATE METHODS

        private Exception PrepareHttpException(HttpResponseMessage Response,string Method,string Message = "")
        {
            var ex = new Exception();
            ex.Data.Add("Method", Method);
            ex.Data.Add("StatusCode", Response.StatusCode);
            if (string.IsNullOrEmpty(Message))
            {
                ex.Data.Add("Message", Response.ReasonPhrase);
            }
            else
            {
                ex.Data.Add("Message", Response.ReasonPhrase + Message);
            }
            
            return ex;
        }

        private Exception PrepareHttpException(string Reason,HttpStatusCode code, string Method)
        {
            var ex = new Exception();
            ex.Data.Add("Method", Method);
            ex.Data.Add("StatusCode", code);
            ex.Data.Add("Message", Reason);
            return ex;
        }


        #endregion

    }
}
