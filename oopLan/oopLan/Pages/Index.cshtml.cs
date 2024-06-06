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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;


namespace oopLan.Pages
{
	public class IndexModel : PageModel
	{
		public Guid currentDeletedApplication;

		public List<OutputApplication> applicationsList { get; set; } = new List<OutputApplication>();

		public List<OutputApplication> applicationsOutput { get; set; } = new List<OutputApplication>();

		[BindProperty]
		public string SearchPrompt { get; set; }

		[BindProperty]
		public string DateStart { get; set; }

		[BindProperty]
		public string DateEnd { get; set; }

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

		public async Task OnPostSearchApplications()
		{
			await OnGet();
			if (SearchPrompt != null)
			{
				ViewData["lastSearch"] = SearchPrompt;

				var applicationsSearch = from app in applicationsList
										 where (app.Title.Contains(SearchPrompt, StringComparison.CurrentCultureIgnoreCase) ||
										 app.Activity.Contains(SearchPrompt, StringComparison.CurrentCultureIgnoreCase) ||
										 app.ShortDescription.Contains(SearchPrompt, StringComparison.CurrentCultureIgnoreCase) ||
										 app.Plan.Contains(SearchPrompt, StringComparison.CurrentCultureIgnoreCase) ||
										 app.Platform.Contains(SearchPrompt, StringComparison.CurrentCultureIgnoreCase))
										 select app;

				applicationsList = applicationsSearch.ToList();
			}

			if (DateStart != null || DateEnd != null)
			{
				DateTime dateStart, dateEnd;

				bool dateParsedStart = DateTime.TryParse(DateStart, out dateStart);
				bool dateParsedEnd = DateTime.TryParse(DateEnd, out dateEnd);

				ViewData["lastDateStart"] = dateParsedStart ? dateStart.ToString("yyyy-MM-dd") : null;
				ViewData["lastDateEnd"] = dateParsedEnd ? dateEnd.ToString("yyyy-MM-dd") : null;

				var applications = from app in applicationsList
								   where (app.SubmissionDate >= dateStart &&
								   app.SubmissionDate <= dateEnd)
								   select app;
				applicationsList = applications.ToList();
			}
		}


		public async Task OnGet()
		{
			try
			{
				using (HttpClient _http = new HttpClient())
				{

					applicationsList = await _http.GetFromJsonAsync<List<OutputApplication>>("https://localhost:7096/OutputApplications");

					Console.WriteLine(applicationsList.Count());
				}
			}
			catch (Exception ex)
			{

			}
		}

	}
}

