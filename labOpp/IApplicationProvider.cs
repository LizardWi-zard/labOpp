using labOpp.Model;

namespace labOpp
{
	public interface IApplicationProvider
    {
        Task<DbResponse> GetActivities();
        Task<DbResponse> GetApplications();
        Task<DbResponse> GetUsers();
        Task<DbResponse> GetPlatforms();
        Task<DbResponse> GetOutputApplication();
		Task<DbResponse> AddApplication(Application newApplication);
		Task<DbResponse> AddUser(string name, string mail);
		Task<DbResponse> DeleteApplication(Guid applicationID);
		Task<DbResponse> GetUserId(string userMail);
	}
}
