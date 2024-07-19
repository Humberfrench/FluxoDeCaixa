using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace FluxoCaixa.Api.Lancamento.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class CacheAttribute : Attribute
    {
        //[Cache("myData", 2)] // Cache for 2 hours

        private IMemoryCache _cache;
        private readonly string _cacheKey;
        private readonly TimeSpan _expiration;
        //private readonly IServiceProvider _serviceProvider;
        public CacheAttribute(string cacheKey, int expirationInMinutes = 15)
        {

            _cacheKey = cacheKey;
            _expiration = TimeSpan.FromMinutes(expirationInMinutes);
        }

        public async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> func, IServiceProvider _serviceProvider)
        {
            _cache = (IMemoryCache)_serviceProvider.GetService(typeof(IMemoryCache));
            if (_cache.TryGetValue(_cacheKey, out IActionResult result))
            {
                return result;
            }
            else
            {
                result = await func();
                _cache.Set(_cacheKey, result, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _expiration
                });
                return result;
            }
        }
    }
}
