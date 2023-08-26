using CrudOperationWithMongoDB.DataAccesLayer;
using CrudOperationWithMongoDB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperationWithMongoDB.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CrudOperationController : ControllerBase
    {
        private readonly ICrudOperationDL _crudOperationDL;

        public CrudOperationController(ICrudOperationDL crudOperationDL)
        {
            _crudOperationDL = crudOperationDL;
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord(InsertRecordRequest request)
        {
            InsertRecordResponse response = new InsertRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Succesfully Saved ";

            try
            {
                response = await _crudOperationDL.InsertRecord(request);

            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message="Exception Occurs : " + ex.Message;
            }
            return Ok (response);
        }
    }
}
