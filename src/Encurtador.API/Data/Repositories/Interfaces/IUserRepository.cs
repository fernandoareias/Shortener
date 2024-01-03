using System;
using Encurtador.API.Models;

namespace Encurtador.API.Data.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}

