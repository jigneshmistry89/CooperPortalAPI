
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Model.Interface;
using Coopers.BusinessLayer.NotifEye.APIClient.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient.HttpService
{
    public class HttpService : IHttpService
    {

        #region PRIVATE MEMBERS

        private readonly IHttpContextProvider _httpContextProvider;
        private readonly IAuthenticationClient _authenticationClient;

        #endregion


        #region CONSTRUCTOR

        public HttpService(IHttpContextProvider httpContextProvider, IAuthenticationClient authenticationClient)
        {
            _httpContextProvider = httpContextProvider;
            _authenticationClient = authenticationClient;
           
        }

        #endregion

        public async Task<T> GetWithUserAsync<T>(string UserName,string Method, string queryParam, bool IsIntegrated, bool IsAnonymous = false)
        {
            var token = await _authenticationClient.GetLegacyNotifEyeToken(UserName);
  
            string Path = string.Format("{0}{1}/{2}?{3}",
              ConfigurationManager.AppSettings["NotifEyeAPIEndpoint"], Method, token, queryParam);

            HttpResponseMessage response = await new HttpClient().GetAsync(Path);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return (await response.Content.ReadAsAsync<APIResponse<T>>()).Result;
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

        public async Task<T> GetAsAsync<T>(string Method,string queryParam,bool IsIntegrated,bool IsAnonymous = false)
        {
            if (IsIntegrated)
            {
                return await GetAsyncIntegrated<T>(Method, queryParam, IsAnonymous);
            }
            else
            {
                return await GetAsync<T>(Method, queryParam, IsAnonymous);
            }
        }

        public async Task<T> PostAsAsync<T>(string Method, object Body, bool IsAnonymous = false)
        {
            string path = string.Format("{0}{1}", ConfigurationManager.AppSettings["NotifEyeIntegratedAPIEndpoint"], Method);

            var httpClient = new HttpClient();

            if (!IsAnonymous)
            {
                httpClient.DefaultRequestHeaders.Add("authenticatedtoken", _httpContextProvider.GetIntegratedNotifyToken());
            }

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
                throw PrepareHttpException(response, Method);
            }
        }

        public async Task<T> PostAsAsyncWithRegistrationToken<T>(string Method, object Body)
        {
            string path = string.Format("{0}{1}", ConfigurationManager.AppSettings["NotifEyeIntegratedAPIEndpoint"], Method);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("RegistrationToken", _httpContextProvider.GetRegistrationToken());
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpClient.PostAsync(path, new StringContent(JsonConvert.SerializeObject(Body), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadAsAsync<T>());
            }
            else
            {
                throw PrepareHttpException(response, Method);
            }
        }

        public async Task<T> PutAsAsync<T>(string Method, object Body)
        {
            string path = string.Format("{0}{1}", ConfigurationManager.AppSettings["NotifEyeIntegratedAPIEndpoint"], Method);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("authenticatedtoken", _httpContextProvider.GetIntegratedNotifyToken());
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
                                ConfigurationManager.AppSettings["NotifEyeIntegratedAPIEndpoint"], Method, queryParam);

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("authenticatedtoken", _httpContextProvider.GetIntegratedNotifyToken());
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

        private async Task<T> GetAsync<T>(string Method, string queryParam,bool IsAnonymous = false)
        {
            string Path = "";

            if (!IsAnonymous)
            {
                Path = string.Format("{0}{1}/{2}?{3}",
                            ConfigurationManager.AppSettings["NotifEyeAPIEndpoint"], Method, await GetNotifEyeToken(), queryParam);
            }
            else
            {
                Path = string.Format("{0}{1}?{2}",
                            ConfigurationManager.AppSettings["NotifEyeAPIEndpoint"], Method, queryParam);
            }
 
            HttpResponseMessage response = await new HttpClient().GetAsync(Path);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return (await response.Content.ReadAsAsync<APIResponse<T>>()).Result;
                }
                catch (Exception ex)
                {
                    throw PrepareHttpException(response, Method);
                }
            }
            else
            {
                throw PrepareHttpException(response, Method);
            }
        }

        private async Task<T> GetAsyncIntegrated<T>(string Method, string queryParam, bool IsAnonymous = false)
        {
            var httpClient = new HttpClient();

            if (!IsAnonymous)
            {
                httpClient.DefaultRequestHeaders.Add("authenticatedtoken", _httpContextProvider.GetIntegratedNotifyToken());
            }

            string path = string.Format("{0}{1}?{2}", ConfigurationManager.AppSettings["NotifEyeIntegratedAPIEndpoint"], Method, queryParam);

            HttpResponseMessage response = await httpClient.GetAsync(path);

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

        private async Task<string> GetNotifEyeToken()
        {
            //Get the current User
            var currUser = await _httpContextProvider.GetCurrentUser();

            //Get NotifEye token
            return await _authenticationClient.GetLegacyNotifEyeToken(currUser.UserName);
        }

        private Exception PrepareHttpException(HttpResponseMessage Response,string Method)
        {
            var ex = new Exception();
            ex.Data.Add("Method", Method);
            ex.Data.Add("StatusCode", Response.StatusCode);
            ex.Data.Add("Message", Response.ReasonPhrase);
            return ex;
        }

        #endregion

    }
}
