using labOpp.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;


namespace oopLan.Pages
{
	public class RegistrationModel : PageModel
    {

		[BindProperty]
		public string userName { get; set; }

		[BindProperty]
		public string userEmail { get; set; }

		public void OnGet()
        {

        }

		public async Task AddAccount()
		{
			var content = new StringContent(userName, Encoding.UTF8, "application/json");

			using (HttpClient client = new HttpClient())
			{
				var response = await client.PostAsync($"https://localhost:7096/CreateNewUser?name={userName}&mail={userEmail}", content);

				Console.WriteLine(response.StatusCode);
			}
		}
	}
}
