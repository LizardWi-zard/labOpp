using labOpp.Context;
using labOpp.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net;

namespace labOpp
{
    public class ApplicationsProvider : IApplicationProvider
    {
        private readonly ConferenceDbContext _context;
        const string connectionString = "ConnectionStrings:ConnectionString";

        private readonly IConfiguration _configuration;

        public ApplicationsProvider(IConfiguration configuration, ConferenceDbContext context)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _context = context;
        }


        public async Task<DbResponse> GetActivities()
        {
            var activities = await _context.Activities.ToListAsync();

            if (activities == null || activities.Count == 0)
            {
                return new DbResponse() { Status = HttpStatusCode.NotFound };
            }

            return new DbResponse() { Status = HttpStatusCode.OK, Data = activities };
        }

        public async Task<DbResponse> GetApplications()
        {
            var activities = await _context.Applications.ToListAsync();

            if (activities == null || activities.Count == 0)
            {
                return new DbResponse() { Status = HttpStatusCode.NotFound };
            }

            return new DbResponse() { Status = HttpStatusCode.OK, Data = activities };
        }
    }
}
