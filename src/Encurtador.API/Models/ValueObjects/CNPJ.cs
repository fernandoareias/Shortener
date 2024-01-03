using System;
using Encurtador.API.Models.ValueObjects.Common;

namespace Encurtador.API.Models.ValueObjects
{
    public class CNPJ : Document
    {
        public string Numero{
            get;
            private set;
        }
        public CNPJ(string numero) : base(numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("CNPJ number cannot be null or empty.", nameof(numero));

            Numero = new string(numero.Where(char.IsDigit).ToArray());

            if (Numero.Length != 14)
                throw new ArgumentException("CNPJ number must contain 14 numeric digits.", nameof(numero));

            if (!IsValid())
                throw new ArgumentException("The Informed CNPJ is invalid.", nameof(numero));
        }

        public override string GetNumberWithMask()
        {
            return string.Format("{0}.{1}.{2}/{3}-{4}",
                Numero.Substring(0, 2),
                Numero.Substring(2, 3),
                Numero.Substring(5, 3),
                Numero.Substring(8, 4),
                Numero.Substring(12, 2));
        }

        public override bool IsValid()
        {
            int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = Numero.Substring(0, 12);

            int soma = 0;
            for (int i = 0; i < 12; i++)
            {
                soma += (tempCnpj[i] - '0') * multiplicadores1[i];
            }

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            tempCnpj += resto;

            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += (tempCnpj[i] - '0') * multiplicadores2[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            tempCnpj += resto;

            return Numero.EndsWith(tempCnpj);
        }
    }
}

