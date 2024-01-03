using System;
using System.Runtime.Serialization;

namespace Encurtador.API.DTOs
{
    [DataContract]
    public class SignInDTO
    {

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }
         
    }
}

