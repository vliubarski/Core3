using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class FileDbModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [Key]
        public string Name { get; set; } = null!;
        public byte[] Content { get; set; } = null!;
        public DateTime UploadDate { get; set; }
        public int Size { get; set; }
    }
}
