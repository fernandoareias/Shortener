using System;
using MongoDB.Driver;

namespace Encurtador.API.Data.Repositories.Interfaces
{
    public interface IMongoContext : IUnitOfWork
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}

