using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CustomControllerBase : ControllerBase
    {
        protected ILogger Logger;
        protected IWebHostEnvironment HostEnvironment;

        public CustomControllerBase
        (
            IWebHostEnvironment hostEnvironment,
            ILogger logger
        )
        {
            HostEnvironment = hostEnvironment;
            Logger = logger;
        }
    }
}
