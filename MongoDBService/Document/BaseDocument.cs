using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBService
{
    public class BaseDocument
    {
        [BsonElement("_id")]
        public BsonObjectId Id { get; set; }


        [BsonIgnore]
        public MongoCollection Collection { get; set; }
        [BsonIgnore]
        public string Db { get; set; }
    }
}
