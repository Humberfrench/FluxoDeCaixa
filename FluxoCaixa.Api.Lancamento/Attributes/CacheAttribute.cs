using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace FluxoCaixa.Api.Lancamento.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class CacheAttribute : Attribute
    {
        private readonly string _cacheKey;
        private readonly TimeSpan _expiration;

        public CacheAttribute(string cacheKey, int expirationInMinutes = 15)
        {

            _cacheKey = cacheKey;
            _expiration = TimeSpan.FromMinutes(expirationInMinutes);
        }

        public async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> func, IServiceProvider _serviceProvider)
        {
            var cache = (IMemoryCache)_serviceProvider.GetService(typeof(IMemoryCache));
            if (cache.TryGetValue(_cacheKey, out IActionResult result))
            {
                return result;
            }
            else
            {
                result = await func();
                cache.Set(_cacheKey, result, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _expiration
                });
                return result;
            }
        }
    }
}
