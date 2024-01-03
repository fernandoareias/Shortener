using System;
namespace Encurtador.API.Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}

