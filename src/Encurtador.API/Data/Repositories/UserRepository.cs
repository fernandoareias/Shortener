using System;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.Models;
using MongoDB.Driver;

namespace Encurtador.API.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            var data = await DbSet.FindAsync(Builders<User>.Filter.Eq("Email.Endereco", email));
            return data.SingleOrDefault();
        }
         
    }
}

