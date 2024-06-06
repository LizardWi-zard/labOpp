using labOpp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace oopLan.Pages
{
	public class CreateApplicationModel : PageModel
    {
		[BindProperty]
		public string userEmail { get; set; }

		[BindProperty]
		public string ApplicationName { get; set; }

		[BindProperty]
		public string ApplicationActivity { get; set; }

		[BindProperty]
		public string ApplicationPlatform { get; set; }

		[BindProperty]
		public string ApplicationDescription { get; set; }

		[BindProperty]
		public string DatePlan { get; set; }


		public List<Activity> activities { get; set; } = new List<Activity>();
		public List<Platform> platforms { get; set; } = new List<Platform>();

		public CreateApplicationModel()
		{
		}

		public async Task CreateNewApplication()
        {
			var applicationGuid = Guid.NewGuid();

			var userId = GetUserId(userEmail).Result;

			var activityId = Guid.Parse(ApplicationActivity);
			var platformId = Guid.Parse(ApplicationPlatform);

			var newApplication = new Application
			{
				ApplicationID = applicationGuid,
				UserID = userId,
				ActivityTypeID = activityId,
				PlatformId = platformId,
				Title = ApplicationName,
				ShortDescription = ApplicationDescription,
				Plan = "Под редакцию",
				SubmissionDate = DateTime.Parse(DatePlan).ToUniversalTime().AddDays(1)
			};

			var json = JsonSerializer.Serialize(newApplication);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			using (HttpClient client = new HttpClient())
			{
				var response = await client.PostAsync("https://localhost:7096/CreateNewApplication", content);

                Console.WriteLine(response.StatusCode);
			}
			
		}

		public async Task<Guid> GetUserId(string mail)
		{
			try
			{
				using (HttpClient _http = new HttpClient())
				{
					var toparce = await _http.GetFromJsonAsync<DbResponse>($"https://localhost:7096/GetUserId?userMail={mail}");

					return Guid.Parse(toparce.Data.ToString());
				}
			}
			catch (Exception ex)
			{
				return Guid.Empty;
			}
		}

		public async Task OnGet()
		{
			try
			{
				using (HttpClient _http = new HttpClient())
				{
					activities = await _http.GetFromJsonAsync<List<Activity>>("https://localhost:7096/Activities");
					platforms = await _http.GetFromJsonAsync<List<Platform>>("https://localhost:7096/Platforms");
				}
			}
			catch (Exception ex)
			{

			}
		}
	}
}
