using CrudOperations.Model;
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

        [HttpGet]
        public async Task<IActionResult> GetAllRecord()
        {
            GetAllRecordResponse response = new GetAllRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Succesfully Saved ";

            try
            {
                response = await _crudOperationDL.GetAllRecord ();

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecordByID([FromQuery]string ID)
        {
            GetRecordByIDResponse response = new GetRecordByIDResponse();

            try
            {
                response = await _crudOperationDL.GetRecordByID(ID);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecordByName([FromQuery] string Name)
        {
            GetRecordByNameResponse response = new GetRecordByNameResponse();

            try
            {
                response = await _crudOperationDL.GetRecordByName(Name);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecordById(InsertRecordRequest request)
        {
            UpdateRecordByIdResponse response = new UpdateRecordByIdResponse();

            try
            {
                response = await _crudOperationDL.UpdateRecordById(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return Ok(response);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateSalaryById(UpdateSalaryByIdRequest request)
        {
            UpdateRecordByIdResponse response = new UpdateRecordByIdResponse();
            try
            {
                response = await _crudOperationDL.UpdateSalaryById(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecordById(DeleteRecordByIdRequest request)
        {
            DeleteRecordByIdResponse response = new DeleteRecordByIdResponse();
            try
            {
                response = await _crudOperationDL.DeleteRecordById(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

    }
}
