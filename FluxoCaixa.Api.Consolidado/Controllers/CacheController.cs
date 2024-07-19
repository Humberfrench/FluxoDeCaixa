using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FluxoCaixa.Api.Consolidado.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]


    public class CacheController : Controller
    {
        private readonly IMemoryCache _cache;

        public CacheController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IActionResult GetData()
        {
            string cacheKey = "myData";
            string data;

            if (_cache.TryGetValue(cacheKey, out data))
            {
                return Ok(data);
            }
            else
            {
                data = GetDataFromDatabase();
                _cache.Set(cacheKey, data, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
                });
                return Ok(data);
            }
        }

        private string GetDataFromDatabase()
        {
            // Lógica de negócios para obter os dados do banco de dados
            return "";
        }
    }
}
