﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Encurtador.API.Models.Common
{
    public abstract class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id
        {
            get;
            protected set;
        } = ObjectId.GenerateNewId();


        [BsonElement("CreatedAt")]
        public DateTime CreatedAt
        {
            get;
            protected set;
        } = DateTime.UtcNow;

        [BsonElement("UpdatedAt")]
        public DateTime? UpdatedAt
        {
            get;
            protected set;
        }
    }
}

