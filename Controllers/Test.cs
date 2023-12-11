using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RobloxDatabaseAPI.Controllers
{
    [ApiController]
    public class Test : ControllerBase
    {
        private readonly ILogger<Test> _logger;

        public Test(ILogger<Test> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("GetTest")]
        public string Get()
        {
            _logger.Log(LogLevel.Information, "Hii!");
            return "Hello, World!";
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteTest")]
        public void Delete()
        {
            _logger.Log(LogLevel.Information, "Yoink!");
        }
    }
}
