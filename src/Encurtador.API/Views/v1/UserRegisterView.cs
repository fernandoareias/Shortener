using System;
using System.Runtime.Serialization;

namespace Shortener.API.Views.v1
{
    [DataContract]
    public class UserRegisterView
    {
        public UserRegisterView(string token, string email)
        {
            Token = token;
            Email = email;
        }

        protected UserRegisterView()
        {

        }

        [DataMember]
        public string Token{
            get;
            private set;
        }

        [DataMember]
        public string Email{
            get;
            private set;
        }
    }
}

