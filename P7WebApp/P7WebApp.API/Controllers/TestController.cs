using Microsoft.AspNetCore.Mvc;

public class TestController : BaseController
{
    [HttpGet]
    public IActionResult GetTest()
    {
        return Ok("ur mom");
    }
}