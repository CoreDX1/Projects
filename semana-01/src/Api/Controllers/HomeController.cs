using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{

    [HttpGet]
    public string Message()
    {
        return "Ejemplo";
    }

}
