using Microsoft.AspNetCore.Mvc;

namespace ApiVersioningDemo.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    public ActionResult GetOrdersV1()
    {
        return Ok(new { Version = "1.0", Orders = new[] { new { Id = 1, Total = 100.50m } } });
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public ActionResult GetOrdersV2()
    {
        return Ok(new { Version = "2.0", Orders = new[] { new { Id = 1, Total = 100.50m, Status = "Completed" } } });
    }

    [HttpGet("query-version")]
    public ActionResult GetOrdersByQuery([FromQuery] string version = "1.0")
    {
        return version switch
        {
            "2.0" => Ok(new { Version = "2.0", Orders = new[] { new { Id = 1, Total = 100.50m, Status = "Completed" } } }),
            _ => Ok(new { Version = "1.0", Orders = new[] { new { Id = 1, Total = 100.50m } } })
        };
    }
}