using Domain.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [FormatFilter]
    public class StatsController : CustomControllerBase
    {
        public StatsController
        (
            IWebHostEnvironment hostEnvironment,
            ILogger<StatsController> logger
        ) : base(hostEnvironment, logger)
        {
        }

        /// <summary>
        /// Gets statistics in URL specified format
        /// </summary>
        /// <returns>Specified format file</returns>
        /// <response code="200">Returns generated file</response>
        [HttpGet("{id:int}.{format?}")]
        [ProducesResponseType(typeof(Stats), StatusCodes.Status200OK)]
        public IActionResult GetURLFormat()
        {
            return Ok(new Stats { Total = 6 });
        }

        /// <summary>
        /// Gets statistics in excel format
        /// </summary>
        /// <returns>Excel format file</returns>
        /// <response code="200">Returns generated Excel file</response>
        /// <remarks>
        /// 
        ///     GET stats/
        ///     stats.xlsx
        /// 
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(Stats), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(new Stats { Total = 6 });
        }
    }
}
