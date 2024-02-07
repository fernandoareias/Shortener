using System;
using System.Runtime.Serialization;

namespace Shortener.API.Views.v1
{ 
    [DataContract]
    public class UserAuthenticateView
    {
        public UserAuthenticateView(string token, string email)
        {
            Token = token;
            Email = email;
        }

        protected UserAuthenticateView()
        {

        }

        [DataMember]
        public string Token
        {
            get;
            private set;
        }

        [DataMember]
        public string Email
        {
            get;
            private set;
        }
    }
}

