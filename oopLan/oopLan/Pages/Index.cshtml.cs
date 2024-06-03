using Microsoft.AspNetCore.Mvc.RazorPages;
using labOpp.Model;
using System;


namespace oopLan.Pages
{
	public class IndexModel : PageModel
    {
		public List<OutputApplication> applicationsList { get; set; } = new List<OutputApplication>();

		public IndexModel()
        {
            OnGet();
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
            {

            }


            


		}
    }
}
