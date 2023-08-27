using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CrudOperationWithMongoDB.Model
{
    public class InsertRecordRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }

        [BsonElement("Name")]
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        public double Salary { get; set; }
    }

    public class InsertRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public static implicit operator InsertRecordResponse(GetAllRecordResponse v)
        {
            throw new NotImplementedException();
        }
    }
}