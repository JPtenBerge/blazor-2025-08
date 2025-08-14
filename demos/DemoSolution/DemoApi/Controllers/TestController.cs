using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class TestController
{
    [HttpGet]
    public string Get()
    {
        return $"Werkt! {DateTime.Now.ToShortTimeString()}";
    }
}
