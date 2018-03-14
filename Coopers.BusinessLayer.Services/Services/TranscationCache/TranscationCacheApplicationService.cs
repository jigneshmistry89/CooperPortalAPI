using Coopers.BusinessLayer.Localizer;
using Coopers.BusinessLayer.Model.DTO;
using System;
using System.Configuration;
using System.Runtime.Caching;

namespace Coopers.BusinessLayer.Services.Services
{
    public class TranscationCacheApplicationService : ITranscationCacheApplicationService
    {

        #region PRIVATE MEMBERS

        private ObjectCache cache;
        private CacheItemPolicy cachePolicy;

        #endregion


        #region CONSTRUCTOR

        public TranscationCacheApplicationService()
        {
            cache = MemoryCache.Default;
            cachePolicy = new CacheItemPolicy();
            cachePolicy.AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["CacheExpirationTime"]));
        }

        #endregion


        #region PUBLIC MEMBERS     

        /// <summary>
        /// insert a cache entry into the cache 
        /// </summary>
        /// <param name="Transcation">Transcation Object to cache</param>
        /// <returns>True/False</returns>
        public bool AddTransaction(Transaction Transcation)
        {
            CacheItem item = new CacheItem(Transcation.ID.ToString(), Transcation);
            return cache.Add(item, cachePolicy);
        }

        public Transaction GetTransaction(string TranscationID)
        {
            var cacheItem = (cache.GetCacheItem(TranscationID));

            if(cacheItem != null)
            {
                return (Transaction)cacheItem.Value;
            }
            else
            {
                throw new Exception(AppLocalizer.ERR_TRANSACTION_EXPIRED);
            }
        }

        #endregion


        #region PRIVATE MEMBERS     


        #endregion

    }
}
