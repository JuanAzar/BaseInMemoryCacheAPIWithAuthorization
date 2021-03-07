using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

using BaseInMemoryArchitecture.BusinessLogic.Contracts;
using BaseInMemoryArchitecture.Common.Contracts;
using BaseInMemoryArchitecture.Common.Models;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace BaseInMemoryArchitecture.BusinessLogic.Implementations
{
    public class BaseService<T> : IBaseService<T> where T : Entity
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly IJsonManager<T> _jsonManager;
        protected readonly string _jsonFilePath;
        protected IList<T> _entitiesList;

        public BaseService(
            IConfiguration configuration,
            IMemoryCache memoryCache,
            IJsonManager<T> jsonManager)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
            _jsonManager = jsonManager;
            _jsonFilePath = $"Data/{typeof(T).Name}.json";

            var cacheKey = $"{this.GetType().Name}";

            _entitiesList = _memoryCache.GetOrCreate(cacheKey, entry => {
                var duration = _configuration.GetChildren().Any(x => x.Key.Equals("MemoryCacheDurationInSeconds")) ? _configuration.GetValue<double>("MemoryCacheDurationInSeconds") : 300;

                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(duration);

                return _jsonManager.GetContent(_jsonFilePath);
            });
        }

        public void Add(T entity)
        {
            var entityId = _entitiesList.Max(x => x.GetId()) + 1;

            entity.SetId(entityId);

            _entitiesList.Add(entity);
        }

        public IList<T> GetAll()
        {
            return _entitiesList
                    .OrderBy(x => x.GetId())
                    .ToList();
        }

        public T GetById(int entityId)
        {
            return _entitiesList
                    .Where(x => x.GetId() == entityId)
                    .SingleOrDefault();
        }

        public void Modify(T entity)
        {
            RemoveById(entity.GetId());

            _entitiesList.Add(entity);
        }

        public void RemoveById(int entityId)
        {
            var entity = _entitiesList
                            .Where(x => x.GetId() == entityId)
                            .SingleOrDefault();

            _entitiesList.Remove(entity);
        }
    }
}
