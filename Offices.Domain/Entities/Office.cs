using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Net;

namespace Offices.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Office
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("RegistryPhoneNumber")]
        public string RegistryPhoneNumber { get; set; } = String.Empty;

        [BsonElement("IsActive")]
        public bool IsActive { get; set; }
        
        [BsonElement("City")]
		public string City { get; set; } = string.Empty;

		[BsonElement("Region")]
		public string Region { get; set; } = string.Empty;

		[BsonElement("Street")]
		public string Street { get; set; } = string.Empty;

		[BsonElement("PostalCode")]
		public string PostalCode { get; set; } = string.Empty;

		[BsonElement("HouseNumber")]
		public int HouseNumber { get; set; }

		[BsonRepresentation(BsonType.Int32)]
		public int PhotoId { get; set; }

		[BsonIgnore]
		public Photo Photo { get; set; }

	}
}
