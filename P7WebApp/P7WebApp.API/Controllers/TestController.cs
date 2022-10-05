using Microsoft.AspNetCore.Mvc;

public class TestController : BaseController
{
    [HttpGet]
    public IActionResult GetTest()
    {
        Console.WriteLine("Test test");
        return Ok("It works!");
    }
}