using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.NotifEye.APIClient.HttpService
{
    public interface IHttpService
    {
        Task<T> GetWithUserAsync<T>(string UserName, string Method, string queryParam, bool IsIntegrated, bool IsAnonymous = false);

        Task<T> GetAsAsync<T>(string Method, string queryParam, bool IsIntegrated, bool IsAnonymous = false);

        Task<T> PostAsAsync<T>(string Method, object Body, bool IsAnonymous = false);

        Task<T> PostAsAsyncWithRegistrationToken<T>(string Method, object Body);

        Task<T> PutAsAsync<T>(string Method, object Body);

        Task<T> DeleteAsAsync<T>(string Method, string queryParam);
    }
}
