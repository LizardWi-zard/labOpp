using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Dynamic;
using System.Text.Json;
using System.Text;
using static System.Net.WebRequestMethods;
using labOpp.Model;
using System;
using Microsoft.AspNetCore.Builder;


namespace oopLan.Pages
{
	public class IndexModel : PageModel
    {
        public Guid currentDeletedApplication;

		public List<OutputApplication> applicationsList { get; set; } = new List<OutputApplication>();

        public IndexModel()
        {
            OnGet();
        }
        public IActionResult IndexModelRefresh() 
        {
            return Page();
        }

		public async Task OnPostDeleteApplication(Guid applicationID)
		{
            currentDeletedApplication = applicationID;

			using (HttpClient client = new HttpClient())
			{
				var response = await client.DeleteAsync($"https://localhost:7096/DeleteApplication?applicationID={applicationID}");
			}
            IndexModelRefresh();
		}

        

		public async Task OnGet()
        {
            try
            {
                using (HttpClient _http = new HttpClient())
                {
                    applicationsList = await _http.GetFromJsonAsync<List<OutputApplication>>("https://localhost:7096/OutputApplications");
                }
            }
            catch (Exception ex)

            


		}
    }
}
