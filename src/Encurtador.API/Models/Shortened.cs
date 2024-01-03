using System;
using Encurtador.API.Models.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Encurtador.API.Models
{
    public class Shortened : AggregateRoot
    {

        protected Shortened()
        {

        }


        public Shortened(string url, ObjectId userId)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException(nameof(url));
             

            Url = url;
            Code = GenerateCode();
            UserId = userId;
        }

        [BsonElement("Code")]
        public string Code
        {
            get;
            private set;
        }  


        [BsonElement("Url")]
        public string Url{
            get;
            private set;
        }

        [BsonElement("Burned")]
        public bool Burned
        {
            get;
            private set;
        } = false;

        public ObjectId UserId
        {
            get;
            private set;
        }

        public void Burn()
        {
            UpdatedAt = DateTime.Now;
            Burned = true;
        }


        private string GenerateCode()
        {
            char[] characters = Guid.NewGuid().ToString().Replace("-", "").ToCharArray();
            Random random = new Random();

            for (int i = 0; i < characters.Length; i++)
                characters[i] = random.Next(2) == 0 ? char.ToUpper(characters[i]) : char.ToLower(characters[i]);

            return new string(characters, 0, 9);
        }
    }
}

