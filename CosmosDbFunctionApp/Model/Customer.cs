using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Model
{
    public class Customer
    {
       // [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        public string UniqueId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }


        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public string SSN { get; set; }
    }

    public class MongoCustomer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        public string UniqueId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }


        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public string SSN { get; set; }
    }
}
