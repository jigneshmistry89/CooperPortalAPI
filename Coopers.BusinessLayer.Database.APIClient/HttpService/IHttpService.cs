using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Database.APIClient
{
    public interface IHttpService
    {

        Task<T> GetAsAsync<T>(string Method, string queryParam);

        Task<T> PostAsAsync<T>(string Method, object Body);

        Task<T> PutAsAsync<T>(string Method, object Body);

        Task<T> DeleteAsAsync<T>(string Method, string queryParam);

    }
}
