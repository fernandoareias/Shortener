using System;
using System.Security.Cryptography;
using System.Text;
using Encurtador.API.Models.ValueObjects.Common;

namespace Encurtador.API.Models.ValueObjects
{
    public class Password : ValueObject
    {

        public Password(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(nameof(password));

            if (password.Length <= 6)
                throw new ArgumentException("Password must be longer than 6 characters", nameof(password));

            (PasswordHash, Salt) = CreateHashPassword(password);
        }

        public string PasswordHash
        {
            get;
            private set;
        }

        public string Salt
        {
            get;
            private set;
        }


        private (string HashSenha, string Salt) CreateHashPassword(string password, string salt = null)
        {
           

            if (salt == null)
            {
                byte[] saltBytes = new byte[16]; // Gere um salt aleatório se não fornecido
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(saltBytes);
                }
                salt = Convert.ToBase64String(saltBytes);
            }

            using (var sha256 = new SHA256Managed())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var passwordSalted = new byte[passwordBytes.Length + Convert.FromBase64String(salt).Length];
                Buffer.BlockCopy(passwordBytes, 0, passwordSalted, 0, passwordBytes.Length);
                Buffer.BlockCopy(Convert.FromBase64String(salt), 0, passwordSalted, passwordBytes.Length, Convert.FromBase64String(salt).Length);

                var hashPassword = sha256.ComputeHash(passwordSalted);
                var hashPasswordString = BitConverter.ToString(hashPassword).Replace("-", "").ToLower();

                return (hashPasswordString, salt);
            }
        }


        public bool Compare(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(nameof(password));

            var (hashPasswordInformated, _) = CreateHashPassword(password, Salt);
            return hashPasswordInformated == PasswordHash;
        }
    }
}

