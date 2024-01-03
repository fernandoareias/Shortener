using System;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.Models;
using Encurtador.API.Models.Common;
using Microsoft.Extensions.Caching.Distributed;

namespace Encurtador.API.Data.Proxy
{
    public class EncurtadorRepositoryProxy : IShortenerRepository
    {

        private readonly RedisProxy _redisProxy;
        private readonly IShortenerRepository _repository;

        public IUnitOfWork unitOfWork => _repository.unitOfWork;

        public EncurtadorRepositoryProxy(IShortenerRepository repository, IDistributedCache distributedCache, ILogger<RedisProxy> logger)
        {
            _repository = repository;
            _redisProxy = new RedisProxy(distributedCache, logger);
           
        } 
         
      
        public void Add(Shortened obj)
        {
            _repository.Add(obj);
            _redisProxy.SetAsync(obj.Code, obj);
        }

        public async Task<Shortened> GetById(Guid id)
        {
            string idString = id.ToString();
            var entity = await _redisProxy.GetAsync<Shortened>(idString);

            if (entity is null)
            {
                entity = await _repository.GetById(id);
                _redisProxy.SetAsync(idString, entity);
            }

            return entity;
        }

        public Task<IEnumerable<Shortened>> GetAll()
        {
            return _repository.GetAll();
        }

        public async void Update(Shortened obj)
        {
            _repository.Update(obj);
            await _redisProxy.RemoveAsync(obj.Code.ToString());
        }

        public async void Remove(Guid id)
        {
            var entity = await GetById(id);
            _repository.Remove(id);
            await _redisProxy.RemoveAsync(entity.Code);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public async Task<Shortened?> GetByCode(string code)
        {
            var entity = await _redisProxy.GetAsync<Shortened>(code);

            if (entity is null)
            {
                entity = await _repository.GetByCode(code);
                _redisProxy.SetAsync(code, entity);
            }

            return entity;
        }
    }
}

