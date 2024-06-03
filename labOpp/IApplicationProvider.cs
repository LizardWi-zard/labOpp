using labOpp.Model;
using Microsoft.AspNetCore.Builder;

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
		Task<DbResponse> AddUser(User newUser);
		Task<DbResponse> DeleteApplication(Guid applicationID);
    }
}
