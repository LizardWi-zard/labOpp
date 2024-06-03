using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Dynamic;
using static System.Net.WebRequestMethods;

namespace oopLan.Pages
{
    public class IndexModel : PageModel
    {
        //[Inject]
        private HttpClient _http = new HttpClient();
        public List<Application> applications { get; set; } = new List<Application>();


        public IndexModel()
        {
            OnGet();
        }

        public async Task OnGet()
        {
            try
            {
                applications = await _http.GetFromJsonAsync<List<Application>>("https://localhost:7096/Applications");

            }
            catch (Exception ex) 
            {
            
            }

        }
    }

    public class Application
    {
        public Guid ApplicationID { get; set; }
        public Guid UserID { get; set; }
        public string ActivityTypeID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Plan { get; set; }
        public DateTime SubmissionDate { get; set; }
    }
}
