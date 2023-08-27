using System.ComponentModel.DataAnnotations;

namespace CrudOperations.Model
{
    public class UpdateSalaryByIdRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public int Salary { get; set; }
    }

    public class UpdateRecordByIdResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
