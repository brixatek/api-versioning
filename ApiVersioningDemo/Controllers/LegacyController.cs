using Microsoft.AspNetCore.Mvc;
using ApiVersioningDemo.Attributes;

namespace ApiVersioningDemo.Controllers;

[ApiController]
[Route("api/legacy")]
[Deprecated(DeprecationDate = "2024-01-01", SunsetDate = "2024-06-01")]
public class LegacyController : ControllerBase
{
    [HttpGet("items")]
    public ActionResult GetLegacyItems()
    {
        return Ok(new 
        { 
            Message = "This endpoint is deprecated. Please use /api/v2.0/products instead.",
            Data = new[] { "Legacy Item 1", "Legacy Item 2" }
        });
    }

    [HttpGet("status")]
    public ActionResult GetStatus()
    {
        return StatusCode(410, new 
        { 
            Error = "Gone",
            Message = "This endpoint has been permanently removed. Use /api/v2.0/products instead.",
            DeprecatedSince = "2024-01-01",
            RemovedOn = "2024-06-01"
        });
    }
}