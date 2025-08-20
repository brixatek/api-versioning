using Microsoft.AspNetCore.Mvc;

namespace ApiVersioningDemo.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/users")]
public class UsersController : ControllerBase
{
    // Header-based versioning example
    [HttpGet]
    [MapToApiVersion("1.0")]
    public ActionResult GetUsersV1()
    {
        return Ok(new { Version = "1.0", Users = new[] { "John", "Jane" } });
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public ActionResult GetUsersV2()
    {
        return Ok(new 
        { 
            Version = "2.0", 
            Users = new[] 
            { 
                new { Id = 1, Name = "John", Email = "john@example.com" },
                new { Id = 2, Name = "Jane", Email = "jane@example.com" }
            }
        });
    }
}