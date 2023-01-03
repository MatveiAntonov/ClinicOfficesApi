using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Offices.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Office
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("Address")]
        public Address Address { get; set; } = new Address();


        [BsonElement("RegistryPhoneNumber")]
        public string RegistryPhoneNumber { get; set; } = String.Empty;

        [BsonElement("IsActive")]
        public bool IsActive { get; set; }
    }
}
