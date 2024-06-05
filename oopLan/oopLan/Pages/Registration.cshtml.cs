using labOpp.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;


namespace oopLan.Pages
{
    public class RegistrationModel : PageModel
    {

		[BindProperty]
		public string userName { get; set; }

		[BindProperty]
		public string userEmail { get; set; }

		public HttpStatusCode responseStatusCode;

		public void OnGet()
        {

        }

		public async Task OnPostAddAccount()
		{
			var content = new StringContent(userName, Encoding.UTF8, "application/json");

			using (HttpClient client = new HttpClient())
			{
				var response = await client.PostAsync($"https://localhost:7096/CreateNewUser?name={userName}&mail={userEmail}", content);
				responseStatusCode = response.StatusCode;
				if (responseStatusCode != System.Net.HttpStatusCode.Created)
				{
					ViewData["Message"] = "OLIIbIBKA! Eta pochta uzhe zanyata. Vvedite druguyu";
				}
				Console.WriteLine(responseStatusCode);
            }
		}
	}
}
