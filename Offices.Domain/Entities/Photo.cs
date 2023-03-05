using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Offices.Domain.Entities
{
    public class Photo
    {
		[BsonId]
		[BsonRepresentation(BsonType.Int32)]
		public int Id { get; set; }

		[BsonElement("PhotoUrl")]
		public string PhotoUrl { get; set; } = String.Empty;

		[BsonElement("PhotoName")]
		public string PhotoName { get; set; } = String.Empty;
    }
}
