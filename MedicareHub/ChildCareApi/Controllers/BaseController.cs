
using ChildCare.DTOs;

using ChildCareCore.Resources;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace ChildCareApi.Controllers
{

    [ApiController]
    public class BaseController : ControllerBase
    {
        public UserModel? AuthUser { get; set; }

        protected IActionResult SuccessResult<T>(T model, string message = "The request has been executed successfully")
        {
            return Ok(new BaseApiResponseModel<T>
            {
                Data = model,
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = message,
                IsSuccess = true
            });
        }

        protected IActionResult SuccessWithoutDataResult(string message = "The request has been executed successfully")
        {
            return Ok(new BaseApiResponseModel<object>
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = message,
                IsSuccess = true
            });
        }

        protected IActionResult ModelErrorResult(string message, ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
            return BadRequestErrorResult(message, errors);
        }
        protected IActionResult ExceptionErrorResult(string message, Exception exception)
        {
            return ErrorResult(message, new string[] { (exception.InnerException ?? exception).Message });
        }

        protected IActionResult BadRequestErrorResult()
        {
           

            return BadRequestErrorResult(BaseResponseMessages.INVALID_USER_CREDENTIAL);
        }


        protected IActionResult BadRequestErrorResult(string message, string[]? errorMessages = null)
        {
            return BadRequest(new BaseApiResponseModel<object>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = message,
                Errors = errorMessages,
                IsSuccess = false
            });
        }

        protected IActionResult NotAuthorizedResult(string message, string[]? errorMessages = null)
        {
            return Unauthorized(new BaseApiResponseModel<object>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = message,
                Errors = errorMessages,
                IsSuccess = false
            });
        }

        protected IActionResult ErrorResult(string message, string[]? errorMessages = null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseApiResponseModel<object>
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = message,
                Errors = errorMessages,
                IsSuccess = false
            });
        }

        protected IActionResult LimitExceededErrorResult(string message, string[]? errorMessages = null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new BaseApiResponseModel<object>
            {
                StatusCode = HttpStatusCode.TooManyRequests,
                Message = message,
                Errors = errorMessages,
                IsSuccess = false
            });
        }

        protected IActionResult NotFoundResult(string message, string[]? errorMessages = null)
        {
            return BadRequest(new BaseApiResponseModel<object>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = message,
                Errors = errorMessages,
                IsSuccess = false
            });
        }
    }
}
