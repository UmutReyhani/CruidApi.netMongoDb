namespace CrudOperationWithMongoDB.Model
{
    public class GetAllRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<InsertRecordRequest> data { get; set; }
    }
}
