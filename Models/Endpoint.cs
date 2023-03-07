using Microsoft.AspNetCore.Mvc;

namespace PizzaStore.Models;

public class Endpoint : Controller
{
    [Route("/")]
    public IActionResult Get()
    {
        return File("~/Client/index.html", "text/html");
    }
}