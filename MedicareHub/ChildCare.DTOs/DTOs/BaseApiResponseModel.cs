
using System.Net;

namespace ChildCare.DTOs
{
    public class BaseApiResponseModel<T>
    {
        public HttpStatusCode? StatusCode { get; set; } = HttpStatusCode.OK;
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public string[]? Errors { get; set; }
    }
}
