using Microsoft.EntityFrameworkCore;

namespace labOpp.Model
{
    public class Activity
    {
        public int ActivityTypeID { get; set; }
        public string Name { get; set; }

        // Навигационные свойства
        public ICollection<Application> Applications { get; set; }
        public ICollection<ApplicationDraft> Drafts { get; set; }
    }
}
