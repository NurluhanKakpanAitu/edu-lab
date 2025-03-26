using Application;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult ResponseOk<T>(T? value) => Ok(ApiResponse<T>.CreateSuccess(value));
    
    protected IActionResult ResponseOk() => Ok(ApiResponse<object>.CreateSuccess(null));
    
    protected IActionResult ResponseFail(string message) => Ok(ApiResponse<object>.CreateFailure(message));
    
    
}