using CrudOperationWithMongoDB.Model;

namespace CrudOperationWithMongoDB.DataAccesLayer
{
    public interface ICrudOperationDL
    {
        public Task<InsertRecordResponse> InsertRecord(InsertRecordRequest request);
    }
}
