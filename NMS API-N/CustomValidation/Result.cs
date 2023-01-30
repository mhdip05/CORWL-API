using Microsoft.AspNetCore.Mvc;

namespace NMS_API_N.CustomValidation
{
    public class Result
    {
#nullable disable
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
