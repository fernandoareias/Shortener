using System;
using Encurtador.API.Data.Repositories.Interfaces;
using Encurtador.API.Models;
using MongoDB.Driver;

namespace Encurtador.API.Data.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IMongoContext context) : base(context)
        {
        }

        public async Task<Company?> GetByCNPJ(string cnpj)
        {
            var data = await DbSet.FindAsync(Builders<Company>.Filter.Eq("CNPJ.Numero", cnpj));
            return data.SingleOrDefault();
        }
    }
}

