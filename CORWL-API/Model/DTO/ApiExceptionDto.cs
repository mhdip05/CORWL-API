namespace CORWL_API.Model.DTO
{
    public class ApiExceptionDto
    {
#nullable disable
        public ApiExceptionDto(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
