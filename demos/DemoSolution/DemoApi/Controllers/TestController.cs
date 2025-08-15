using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class TestController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        Console.WriteLine($"User: {User.Identity?.Name}");

        return $"Werkt! {DateTime.Now.ToShortTimeString()}";
    }
}
