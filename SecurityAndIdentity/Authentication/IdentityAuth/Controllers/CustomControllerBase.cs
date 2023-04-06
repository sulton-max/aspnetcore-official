using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAuth.Controllers;

[Authorize]
[Route("api/[controller]")]
public class CustomControllerBase : ControllerBase
{
}