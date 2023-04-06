using Application.Services;
using BookStore.API.Conventions;
using Domain.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiConventionType(typeof(CustomDefaultConventions))]
    public class ContactsController : CustomControllerBase
    {
        private readonly IEntityServiceBase<Contact> _contactsService;

        public ContactsController
        (
            IWebHostEnvironment hostEnvironment, 
            ILogger logger,
            IEntityServiceBase<Contact> contactsService
            
        ) : base(hostEnvironment, logger)
        {
            _contactsService = contactsService;

        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> Get()
        {
            return Ok(await _contactsService.GetAsync(10, 1));
        }

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(CustomDefaultConventions), "GetById")]
        public IActionResult GetById()
        {
            throw new InvalidOperationException("Contacts model is corrupted");
            return Ok();
        }
    }
}
