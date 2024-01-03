using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using Encurtador.API.Models;

namespace Encurtador.API.Views.v1
{
    [DataContract]
    public class ShortenedCreateView
    {
        public ShortenedCreateView(Shortened shortened)
        { 
            Url = $"http://localhost:5009/api/v1/burn/{shortened.Code}";
        }

        [DataMember]
        public string Url { get; private set; }
    }
}

