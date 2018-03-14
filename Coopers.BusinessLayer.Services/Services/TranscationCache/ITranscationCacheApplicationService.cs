using Coopers.BusinessLayer.Model.DTO;
using System.Threading.Tasks;

namespace Coopers.BusinessLayer.Services.Services
{
    public interface ITranscationCacheApplicationService
    {
        /// <summary>
        /// insert a cache entry into the cache 
        /// </summary>
        /// <param name="Transcation">Transcation Object to cache</param>
        /// <returns>True/False</returns>
        bool AddTransaction(Transaction Transcation);


        Transaction GetTransaction(string TranscationID);
    }
}
