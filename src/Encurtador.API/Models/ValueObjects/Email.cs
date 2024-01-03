using System;
using System.Text.RegularExpressions;
using Encurtador.API.Models.ValueObjects.Common;

namespace Encurtador.API.Models.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException("Email address cannot be null or empty.", nameof(endereco));

            Endereco = endereco;


            if (!IsValid())
                throw new ArgumentException("Invalid email address.", nameof(endereco));

        }

        public string Endereco { get; private set; }
         
        public override bool IsValid()
        {
            try
            { 
                var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                return regex.IsMatch(Endereco);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}

