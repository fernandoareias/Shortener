using System;
using System.Runtime.Serialization;
using Encurtador.API.Models;

namespace Encurtador.API.Views.v1
{
    [DataContract]
    public class CompanyCreatedView
    {

        public CompanyCreatedView(Company company)
        { 
            Url = $"https://localhost:7211/api/v1/company/{company.CNPJ.Numero}";
        }

        [DataMember]
        public string Url { get; private set; }
         
    }
}

