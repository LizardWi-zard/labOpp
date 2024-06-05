using labOpp.Model;
using Microsoft.AspNetCore.Mvc;
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

		[HttpGet("/Platforms")]
		public async Task<List<Platform>> GetPlatforms()
		{
			var response = await _getApplication.GetPlatforms();

			var output = response.Data as List<Platform>;

			return output;
		}

		[HttpGet("/Users")]
		public async Task<List<User>> GetUsers()
		{
			var response = await _getApplication.GetUsers();

			var output = response.Data as List<User>; ;

			return output;
		}

		[HttpGet("/OutputApplications")]
		public async Task<List<OutputApplication>> GetOutputApplications()
		{
			var response = await _getApplication.GetOutputApplication();

			var output = response.Data as List<OutputApplication>;

			return output;
		}


		[HttpPost("/CreateNewApplication")]
        public async Task<DbResponse> CreateNewApplication(Application newApplication)
        {
            if (newApplication == null)
            {
                return new DbResponse() { Status = HttpStatusCode.BadRequest, Data = string.Empty };
            }

            var response = await _getApplication.AddApplication(newApplication);

            if (response.Status == HttpStatusCode.Created)
            {
                return new DbResponse() { Status = HttpStatusCode.Created, Data = response.Data };
            }

            return new DbResponse() { Status = response.Status, Data = string.Empty };
        }

        [HttpDelete("/DeleteApplication")]
        public async Task<DbResponse> DeleteApplication(Guid applicationID)
        {
            if (applicationID == null)
            {
                return new DbResponse() { Status = HttpStatusCode.BadRequest, Data = string.Empty };
            }

            var response = await _getApplication.DeleteApplication(applicationID);

            if (response.Status == HttpStatusCode.NotFound)
            {
                return new DbResponse() { Status = HttpStatusCode.NotFound, Data = string.Empty };
            }
            return new DbResponse() { Status = response.Status, Data = response.Data };
        }

		[HttpPost("/CreateNewUser")]
		public async Task<HttpStatusCode> CreateNewUser(string name, string mail)
		{
			if (name == null || mail == null)
			{
				return  HttpStatusCode.BadRequest;
			}

			var response = await _getApplication.AddUser(name, mail);

			if (response.Status == HttpStatusCode.Created)
			{
				return HttpStatusCode.Created;
			}

			return response.Status;
		}

		[HttpGet("/GetUserId")]
		public async Task<DbResponse> GetUserId(string userMail)
		{
			if (userMail == null)
			{
				return new DbResponse() { Status = HttpStatusCode.BadRequest, Data = string.Empty };
			}

			var response = await _getApplication.GetUserId(userMail);

			if (response.Status == HttpStatusCode.NotFound)
			{
				return new DbResponse() { Status = HttpStatusCode.NotFound, Data = string.Empty };
			}

			return new DbResponse() { Status = response.Status, Data = response.Data };
		}
	}
}
