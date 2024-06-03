using labOpp.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace labOpp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly ILogger<ActivityController> _logger;
        private readonly IApplicationProvider _getApplication;

        public ActivityController(ILogger<ActivityController> logger, IApplicationProvider getApplication)
        {
            _logger = logger;
            _getApplication = getApplication ?? throw new ArgumentNullException(nameof(getApplication));

        }

        // получение заявок
        [HttpGet("/Applications")]
        public async Task<List<Application>> GetApplication()
        {
            var response = await _getApplication.GetApplications();

            var output = response.Data as List<Application>;

            return output;
        }

        //получение списка возможных типов активности
        [HttpGet("/Activities")]
        public async Task<List<Activity>> GetActivities()
        {
            var response = await _getApplication.GetActivities();

            var output = response.Data as List<Activity>; ;

            return output;
        }


        [HttpPost]
        public async Task<DbResponse> CreateNewApplication(Application newApplication)
        {
            if (newApplication == null)
            {
                return new DbResponse() { Status = HttpStatusCode.BadRequest, Data = string.Empty};
            }

            var response = await _getApplication.AddApplication(newApplication);

            if (response.Status == HttpStatusCode.Created)
            {
                return new DbResponse() { Status = HttpStatusCode.Created, Data = response.Data};
            }

            return new DbResponse() { Status = response.Status, Data = string.Empty };
        }
    }
}
