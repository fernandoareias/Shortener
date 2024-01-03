using System;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Encurtador.API.Data.Proxy
{
    public class RedisProxy
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<RedisProxy> _logger;

        public RedisProxy(IDistributedCache distributedCache, ILogger<RedisProxy> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async void SetAsync<TInput>(string id, TInput input) where TInput : class
        {
            try
            {
                var options = new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromHours(1)
                };

                await _distributedCache.SetAsync(id, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(input)), options);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[EXCEPTION][REDIS][SET] => Exception {ex}");
            }
        }

        public async Task<TOutput?> GetAsync<TOutput>(string key) where TOutput : class
        {
            try
            {
                var bytes = await _distributedCache.GetAsync(key);

                return bytes != null ? JsonConvert.DeserializeObject<TOutput>(Encoding.UTF8.GetString(bytes)) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[EXCEPTION][REDIS][GET] => Exception {ex}");
                return null;
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                await _distributedCache.RemoveAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[EXCEPTION][REDIS][REMOVE] => Exception {ex}");
            }
        }
    }
}

