using System;
using Encurtador.API.Models.Common;
using Encurtador.API.Models.ValueObjects;

namespace Encurtador.API.Models
{
    public class Company : Entity
    {
        protected Company()
        {

        }

        public Company(string name, CNPJ cnpj)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            if (cnpj is null)
                throw new ArgumentException(nameof(cnpj));

            Name = name;
            CNPJ = cnpj;
        }

        public string Name{
            get;
            private set;
        }

        public CNPJ CNPJ
        {
            get;
            private set;
        }
    }
}

