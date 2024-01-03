using System;
using Encurtador.API.Models;

namespace Encurtador.API.Data.Repositories.Interfaces
{
    public interface IShortenerRepository : IRepository<Shortened>
    {
        Task<Shortened?> GetByCode(string code);
    }
}

