using CrudOperationWithMongoDB.Model;
using MongoDB.Driver;

namespace CrudOperationWithMongoDB.DataAccesLayer
{
    public class CrudOperationDL : ICrudOperationDL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<InsertRecordRequest> _mongoCollection;

        public CrudOperationDL(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetValue<string>("DatabaseSetting:ConnectionString");
            var dbName = _configuration.GetValue<string>("DatabaseSetting:DatabaseName");
            _mongoClient = new MongoClient(connectionString);
            var _MongoDatabase = _mongoClient.GetDatabase(dbName);
            _mongoCollection = _MongoDatabase.GetCollection<InsertRecordRequest>(_configuration.GetValue<string>("DatabaseSetting:CollectionName"));
        }

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
    }
}