using Microsoft.AspNetCore.Mvc;
using static ServiceApp.API.Utils.Constant;

namespace ServiceApp.API.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        public BaseController()
        { }

        protected IActionResult Success(object response = null) => Json(new
        {
            StatusCode = ResponseStatusCode.Success,
            Data = response
        });


        protected IActionResult SuccessPagination(int count, object response = null) => Json(new
        {
            StatusCode = ResponseStatusCode.Success,
            Data = new
            {
                response,
                count
            }
        });

        protected IActionResult Error(ResponseStatusCode statusCode, string errorMessage = null) => Json(new
        {
            StatusCode = statusCode,
            Message = errorMessage
        });
    }
}
