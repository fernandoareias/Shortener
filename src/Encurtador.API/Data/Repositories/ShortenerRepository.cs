using System;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.Models;
using Encurtador.API.Models.Common;
using MongoDB.Driver;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Encurtador.API.Data.Repositories
{
    public class ShortenerRepository : BaseRepository<Shortened>, IShortenerRepository
    {
        public ShortenerRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<Shortened?> GetByCode(string code)
        {
            var data = await DbSet.FindAsync(Builders<Shortened>.Filter.Eq("Code", code));
            return data.SingleOrDefault();
        }
    }
}

