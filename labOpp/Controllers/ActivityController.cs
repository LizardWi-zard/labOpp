using Microsoft.AspNetCore.Mvc;

namespace labOpp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly ILogger<ActivityController> _logger;

        public ActivityController(ILogger<ActivityController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ActivityController> Get()
        {
            return null;
        }

        [HttpPost]
        public IEnumerable<ActivityController> Post()
        {
            return null;
        }
    }
}
