using System;
using System.Runtime.Serialization;

namespace Encurtador.API.DTOs
{
    [DataContract]
    public class AuthenticationDTO
    {

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string CNPJ { get; set; }
    }
}

