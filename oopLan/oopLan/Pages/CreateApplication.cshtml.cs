using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using labOpp.Model;

namespace oopLan.Pages
{
    public class CreateApplicationModel : PageModel
    {
		[BindProperty]
		public string ApplicationName { get; set; }

		[BindProperty]
		public string ApplicationActivity { get; set; }

		[BindProperty]
		public string ApplicationDescription { get; set; }

		public async Task CreateNewApplication()
        {
			var applicationGuid = Guid.NewGuid();

			var newApplication = new Application
			{
				ApplicationID = applicationGuid,
				UserID = applicationGuid,
				ActivityTypeID = Guid.NewGuid(),
				Title = ApplicationName,
				ShortDescription = ApplicationDescription,
				Plan = "Под редакцию",
				SubmissionDate = DateTime.UtcNow
			};

			var json = JsonSerializer.Serialize(newApplication);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			using (HttpClient client = new HttpClient())
			{
				var response = await client.PostAsync("https://localhost:7096/CreateNewApplication", content);
			}

		}
	}
}
