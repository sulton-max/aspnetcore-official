using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    public class ErrorsControlller : CustomControllerBase
    {
        public ErrorsControlller(IWebHostEnvironment hostEnvironment, ILogger<ErrorsControlller> logger) : base(hostEnvironment, logger)
        {
        }

        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError() => Problem();

        [Route("/error/dev")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleDevError([FromServices]IHostEnvironment environment)
        {
            if (!environment.IsDevelopment())
                return NotFound();

            var exceptionHanlderFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            return Problem(detail: exceptionHanlderFeature.Error.StackTrace, title: exceptionHanlderFeature.Error.Message);
        }
    }
}
