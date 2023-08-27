using CrudOperations.Model;
using CrudOperationWithMongoDB.Model;
using System.Threading.Tasks;

namespace CrudOperationWithMongoDB.DataAccesLayer
{
    public interface ICrudOperationDL
    {
        Task<InsertRecordResponse> InsertRecord(InsertRecordRequest request);
        Task<GetAllRecordResponse> GetAllRecord();
        Task<GetRecordByIDResponse> GetRecordByID(string ID);
        Task<GetRecordByNameResponse> GetRecordByName(string Name);
        Task<UpdateRecordByIdResponse> UpdateRecordById(InsertRecordRequest request);
        Task<UpdateRecordByIdResponse> UpdateSalaryById(UpdateSalaryByIdRequest request);
        Task<DeleteRecordByIdResponse> DeleteRecordById(DeleteRecordByIdRequest request);
    }
}
