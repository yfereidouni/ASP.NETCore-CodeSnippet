using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ErrorsController : ControllerBase
{

    [Route("/error-development")]
    public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }
        else
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            return Problem(detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }
    }

    [Route("/error")]
    public IActionResult HandleError()
    {
        return Problem();
    }

    [HttpGet("Throw")]
    public IActionResult Throw()
    {
        throw new Exception("Sample Exception");
    }

}
