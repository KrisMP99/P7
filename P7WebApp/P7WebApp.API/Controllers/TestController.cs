using Microsoft.AspNetCore.Mvc;

namespace P7WebApp.API.Controllers
{
    public class TestController : BaseController
    {
        private readonly ILogger _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetTest()
        {
            _logger.LogInformation("Test page visited at {DT}", 
                DateTime.UtcNow.ToLongTimeString());
            return Ok("It works!");
        }
    }
}