using System;
using Encurtador.API.Models;

namespace Encurtador.API.Data.Repositories.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company?> GetByCNPJ(string cnpj);
    }
}

