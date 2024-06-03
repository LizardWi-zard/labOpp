﻿using labOpp.Context;
using labOpp.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using System.Net;

namespace labOpp
{
    public class ApplicationsProvider : IApplicationProvider
    {
        private const bool NoDb = true; // менять если нет дб
        private const string baseApplicationReturn = "[{\"applicationID\": \"c438395f-d525-4bc8-b680-6232417a6aa4\", \"userID\": \"ddfea950-d878-4bfe-a5d7-e9771e830cbd\",    \"activityTypeID\": \"Report\",    \"title\": \"Новые фичи C# vNext\",    \"shortDescription\": \"Расскажу что нас ждет в новом релизе!\",    \"plan\": \"очень много текста... прямо детальный план доклада!\",    \"submissionDate\": \"2024-04-01T00:00:00\"  },  {    \"applicationID\": \"6c12a8ff-29db-4f9c-a178-3f3991b1b20d\",    \"userID\": \"c53a612b-0e56-4748-84b6-d12b806205fb\",    \"activityTypeID\": \"Discussion\",    \"title\": \"Новые фичи \",    \"shortDescription\": \"Расскажу прикол\",    \"plan\": \"очень много текста... прямо детальный план доклада!\",    \"submissionDate\": \"2024-04-01T00:00:00\"  },  {    \"applicationID\": \"6fe0ccd0-53f7-43b6-9b38-1f2a27b41bee\",    \"userID\": \"c53a612b-0e56-4748-84b6-d12b806205fb\",    \"activityTypeID\": \"Report\",    \"title\": \"Приколы\",    \"shortDescription\": \"мало!\",    \"plan\": \"123451231\",    \"submissionDate\": \"2024-04-01T00:00:00\"  },  {    \"applicationID\": \"1d791bc7-8b3b-471e-a4cb-8b35a17a3eba\",    \"userID\": \"dabbd68f-4afe-4032-854b-de66d9c151de\",    \"activityTypeID\": \"Masterclass\",    \"title\": \"Приготовление пиццы\",    \"shortDescription\": \"Будем делать сырный бортик\",    \"plan\": \"Много текста про сырный бортик\",    \"submissionDate\": \"2024-03-13T00:00:00\"  },  {    \"applicationID\": \"9c4e89bf-69ce-49a6-83c2-589c9be89795\",    \"userID\": \"dabbd68f-4afe-4032-854b-de66d9c151de\",    \"activityTypeID\": \"Discussion\",    \"title\": \"Про черное\",    \"shortDescription\": \"Говорим о черном\",    \"plan\": \"Тескст про черное\",    \"submissionDate\": \"2024-02-23T00:00:00\"  }]";
        private const string baseActivityReturn = "[{\"id\": 1,    \"name\": \"Report\",    \"description\": \"Доклад, 35-45 минут\"  },  {    \"id\": 2,    \"name\": \"Masterclass\",    \"description\": \"Мастеркласс, 1-2 часа\"  },  {    \"id\": 3,    \"name\": \"Discussion\",    \"description\": \"Дискуссия / круглый стол, 40-50 минут\"  }]";

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
            if(NoDb)
            {
                List<Activity> output = JsonConvert.DeserializeObject<List<Activity>>(baseActivityReturn).ToList();
                return new DbResponse() { Status = HttpStatusCode.OK, Data = output };
            }
            else
            {
                var activities = await _context.Activities.ToListAsync();

                if (activities == null || activities.Count == 0)
                {
                    return new DbResponse() { Status = HttpStatusCode.NotFound };
                }

                return new DbResponse() { Status = HttpStatusCode.OK, Data = activities };
            }

        }

        public async Task<DbResponse> GetApplications()
        {
            if (NoDb)
            {
                List<Application> output = JsonConvert.DeserializeObject<List<Application>>(baseApplicationReturn).ToList();
                return new DbResponse() { Status = HttpStatusCode.OK, Data = output };
            }
            else
            {
                var activities = await _context.Applications.ToListAsync();

                if (activities == null || activities.Count == 0)
                {
                    return new DbResponse() { Status = HttpStatusCode.NotFound };
                }

                return new DbResponse() { Status = HttpStatusCode.OK, Data = activities };
            }
        }

        public async Task<DbResponse> AddApplication(Application newApplication)
        {
            _context.Applications.Add(newApplication);
            await _context.SaveChangesAsync();

            return new DbResponse() { Status = HttpStatusCode.Created, Data = newApplication };
        }
    }
}
