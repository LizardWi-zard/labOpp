using System.ComponentModel.DataAnnotations;

namespace labOpp.Model
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Application> Applications { get; set; }
        public ICollection<ApplicationDraft> Drafts { get; set; }
    }
}
