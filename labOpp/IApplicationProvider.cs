using labOpp.Model;
using Microsoft.AspNetCore.Builder;

namespace labOpp
{
    public interface IApplicationProvider
    {
        Task<DbResponse> GetActivities();
        Task<DbResponse> GetApplications();
        Task<DbResponse> AddApplication(Application newApplication);
    }
}
