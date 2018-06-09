using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Sombra.Core;
using Sombra.Messaging;
using Sombra.Messaging.Infrastructure;

namespace Sombra.Web.Infrastructure.Messaging
{
    public class CachingRabbitBus : ICachingBus
    {
        private readonly IMemoryCache _cache;

        public CachingRabbitBus(IMemoryCache cache, IBus bus)
        {
            _cache = cache;
            Bus = bus;
        }

        public IBus Bus { get; }

        public async Task<TResponse> RequestAsync<TResponse>(IRequest<TResponse> request, CachingOptions options = CachingOptions.None)
            where TResponse : class, IResponse, new()
        {
            if (options == CachingOptions.SkipCache) return await Bus.RequestAsync(request);

            if (request.GetType().GetCustomAttribute(typeof(CachableAttribute)) is CachableAttribute cacheAttribute)
            {
                var cacheKey = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                {
                    ContractResolver = new AttributeContractResolver<CacheKeyAttribute>()
                });

                if (options == CachingOptions.RenewCache)
                {
                    var response = await Bus.RequestAsync(request);
                    return _cache.Set(cacheKey, response, cacheAttribute.LifeTime);
                }

                if (options == CachingOptions.None)
                {
                    return await _cache.GetOrCreateAsync(cacheKey, async entry =>
                    {
                        entry.SlidingExpiration = cacheAttribute.LifeTime;
                        return await Bus.RequestAsync(request);
                    });
                }
            }

            return await Bus.RequestAsync(request);
        }

        public Task PublishAsync<TEvent>(TEvent message) where TEvent : class, IEvent => Bus.PublishAsync(message);
    }
}