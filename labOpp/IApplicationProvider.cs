﻿using labOpp.Model;

namespace labOpp
{
    public interface IApplicationProvider
    {
        Task<DbResponse> CreateApplication(Application application);
        Task<DbResponse> EditApplication(Guid id, Application application);
        Task<DbResponse> DeleteApplication(Guid id);

        Task<DbResponse> SubmittingApplication(Guid id);
        Task<DbResponse> GetApplicationsSubmittedAfter(DateTime dateTime);
        Task<DbResponse> GetApplicationsUnsubmittedOlder(DateTime dateTime);
        Task<DbResponse> GetUsersUnsubmittedApplication(Guid id);
        Task<DbResponse> GetApplicationById(Guid id);
        Task<DbResponse> GetActivities();

        Task<DbResponse> GetAllApplications();
    }
}
