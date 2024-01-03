using System;
using System.Security.Cryptography;
using System.Text;
using Encurtador.API.Models.Common;
using Encurtador.API.Models.ValueObjects;

namespace Encurtador.API.Models
{
    public class User : Entity
    {
        protected User()
        {

        }

        public User(Email email, Password password, Company company)
        {
            if (email is null)
                throw new ArgumentException(nameof(email));

            if (password is null)
                throw new ArgumentException(nameof(password));

            if (company is null)
                throw new ArgumentException(nameof(company));

            Email = email;
            Password = password;
            Company = company;
        }

        public Email Email
        {
            get;
            private set;
        }

        public Password Password
        {
            get;
            private set;
        }

        public Company Company
        {
            get;
            private set;
        } 

    }
}

