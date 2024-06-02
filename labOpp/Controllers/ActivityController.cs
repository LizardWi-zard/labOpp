using labOpp.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
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

        // создание заявки
        [HttpPost]
        public async Task<ActionResult<string>> CreateApplication([FromBody] string json)
        {
            var application = new Application();

            try
            {
                application = JsonConvert.DeserializeObject<Application>(json);
            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest.ToString();
            }

            var response = new DbResponse();


            var output = response.Data;

            return JsonConvert.SerializeObject(output);
        }

        // редактирование заявки
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> EditApplication([FromRoute] string id, [FromBody] string json)
        {
            var application = new Application();

            try
            {
                application = JsonConvert.DeserializeObject<Application>(json);
            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest.ToString();
            }

            DbResponse response = new DbResponse();

            var output = response.Data;

            return JsonConvert.SerializeObject(output);
        }

        // удаление заявки
        [HttpDelete("{id}")]
        public async Task<ActionResult<HttpStatusCode>> DeleteDocument(string id)
        {
            var response = await _getApplication.DeleteApplication(Guid.Parse(id));

            return response.Status;
        }

        // отправка заявки на рассмотрение программным комитетом
        [HttpPost("{id}/submit")]
        public async Task<ActionResult<string>> SubmittingApplication(string id)
        {
            var isGuidParsed = Guid.TryParse(id, out var guid);

            if (!isGuidParsed)
            {
                return HttpStatusCode.BadRequest.ToString();
            }

            var response = await _getApplication.SubmittingApplication(guid);

            return response?.Data?.ToString() ?? HttpStatusCode.NotFound.ToString();
        }

        // получение заявки по идентификатору
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetApplicationById(string id)
        {
            var isGuidParsed = Guid.TryParse(id, out var guid);

            if (!isGuidParsed)
            {
                return HttpStatusCode.BadRequest.ToString();
            }

            var response = await _getApplication.GetApplicationById(guid);

            return response?.Data?.ToString() ?? HttpStatusCode.NotFound.ToString();
        }

        //получение заявок поданных после указанной даты и получение заявок не поданных и старше определенной даты
        [HttpGet]
        public async Task<ActionResult<string>> GetApplicationsSubmittedAfter([FromQuery] string? submittedAfter, [FromQuery] string? unsubmittedOlder)
        {
            var response = new DbResponse();
            string output;

            if (submittedAfter != null && unsubmittedOlder != null)
            {
                return "Unable to work with two filters";
            }
            if (submittedAfter != null && unsubmittedOlder == null)
            {
                response = await _getApplication.GetApplicationsSubmittedAfter(DateTime.Parse(submittedAfter));
            }
            else if (unsubmittedOlder != null && submittedAfter == null)
            {
                response = await _getApplication.GetApplicationsUnsubmittedOlder(DateTime.Parse(unsubmittedOlder));
            }

            if (response.Status == HttpStatusCode.OK && response.Data == null)
            {
                return "{ }";
            }

            output = response.Data.ToString();

            return output;
        }

        // получение текущей не поданной заявки для указанного пользователя
        [HttpGet("/users/{user}/currentapplication")]
        public async Task<ActionResult<string>> GetUsersUnsubmittedApplication(string user)
        {
            var response = await _getApplication.GetUsersUnsubmittedApplication(Guid.Parse(user));

            string output = response.Data.ToString();

            return output;
        }

        //получение списка возможных типов активности
        [HttpGet("/Activities")]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            var response = await _getApplication.GetActivities();

            var output = response.Data as List<Activity>; ;

            return output;
        }

     
    }
}
