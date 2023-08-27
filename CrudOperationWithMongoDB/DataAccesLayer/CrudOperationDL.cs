using CrudOperations.Model;
using CrudOperationWithMongoDB.Model;
using MongoDB.Bson;
using MongoDB.Driver;


namespace CrudOperationWithMongoDB.DataAccesLayer
{
    public class CrudOperationDL : ICrudOperationDL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<InsertRecordRequest> _mongoCollection;
        private readonly IMongoCollection<InsertRecordRequest> _booksCollection;

        public CrudOperationDL(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetValue<string>("DatabaseSetting:ConnectionString");
            var dbName = _configuration.GetValue<string>("DatabaseSetting:DatabaseName");
            _mongoClient = new MongoClient(connectionString);
            var _MongoDatabase = _mongoClient.GetDatabase(dbName);
            _mongoCollection = _MongoDatabase.GetCollection<InsertRecordRequest>(_configuration.GetValue<string>("DatabaseSetting:CollectionName"));
        }
        //post
        public async Task<InsertRecordResponse> InsertRecord(InsertRecordRequest request)
        {
            InsertRecordResponse response = new InsertRecordResponse();

            try
            {
                request.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                request.UpdatedDate = string.Empty;

                await _mongoCollection.InsertOneAsync(request);
                response.IsSuccess = true; // Eğer başarılıysa bu bilgiyi ayarla
                response.Message = "Record successfully inserted.";

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
        //GET
        public async Task<GetAllRecordResponse> GetAllRecord()
        {
            GetAllRecordResponse response = new GetAllRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully";

            try
            {
                response.data = new List<InsertRecordRequest>();
                response.data = await _mongoCollection.Find(x => true).ToListAsync();
                if(response.data.Count == 0)
                {
                    response.Message = " No record Found";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs"+ ex.Message;

            }

            return response;
        }
        //GETID
        public async Task<GetRecordByIDResponse> GetRecordByID(string ID)
        {
            GetRecordByIDResponse response = new GetRecordByIDResponse();
            response.IsSuccess = true;
            response.Message = "Fetch Data Successfully By ID";

            try
            {
                response.data = await _mongoCollection.Find(x => (x.Id == ID)).FirstOrDefaultAsync();

                if (response.data == null)
                {
                    response.Message = "Invalid Id please enter valid id ";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs: " + ex.Message;
            }

            return response;
        }
        //GETNAME
        public async Task<GetRecordByNameResponse> GetRecordByName(string Name)
        {
            GetRecordByNameResponse response = new GetRecordByNameResponse();
            response.IsSuccess = true;
            response.Message = "Fetch Data Successfully By Name";

            try
            {
                response.data = new List<InsertRecordRequest>();
                response.data = await _mongoCollection.Find(x => (x.FirstName == Name)||x.LastName==Name).ToListAsync();

                if (response.data.Count==0)
                {
                    response.Message = "Invalid Id please enter valid Name ";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs: " + ex.Message;
            }

            return response;
        }

        public async Task<UpdateRecordByIdResponse> UpdateRecordById(InsertRecordRequest request)
        {
            UpdateRecordByIdResponse response = new UpdateRecordByIdResponse();
            response.IsSuccess = true;
            response.Message = "UpdateRecord Successfully By ID";
            try
            {
                GetRecordByIDResponse response1 = await GetRecordByID(request.Id);
                request.CreatedDate = response1.data.CreatedDate;
                request.UpdatedDate = DateTime.Now.ToString();

                var Result = await _mongoCollection.ReplaceOneAsync(x => x.Id == request.Id, request);

                if (!Result.IsAcknowledged)
                {
                    response.Message = "Input id Not found/ Update Not Occured";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "UpdateRecord Occured" + ex.Message;
            }
            return response;
        }
        public async Task<UpdateRecordByIdResponse> UpdateSalaryById(UpdateSalaryByIdRequest request)
        {
            UpdateRecordByIdResponse response = new UpdateRecordByIdResponse();
            response.IsSuccess = true;
            response.Message = "Update Salary Successfully";

            try
            {
                var Filter = new BsonDocument()
                    .Add("Salary", request.Salary)
                    .Add("UpdatedDate", DateTime.Now.ToString());

                var updateDoc = new BsonDocument("$set", Filter);

                var Result = await _booksCollection.UpdateOneAsync(x => x.Id == request.Id, updateDoc);

                if (Result.ModifiedCount == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "No records were updated. Check if the ID exists.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }
        public async Task<DeleteRecordByIdResponse> DeleteRecordById(DeleteRecordByIdRequest request)
        {
            DeleteRecordByIdResponse response = new DeleteRecordByIdResponse();
            response.IsSuccess = true;
            response.Message = "Delete Record Successfully By Id";

            try
            {

                var result = await _booksCollection.DeleteOneAsync(x => x.Id == request.Id);
                if (!result.IsAcknowledged)
                {
                    response.IsSuccess = true;
                    response.Message = "Record Not Found In Database For Deletion, Please Enter Valid Id";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }

    }
}
