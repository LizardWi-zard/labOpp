using System.ComponentModel.DataAnnotations;

namespace labOpp.Model
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Application> Applications { get; set; }
        public ICollection<ApplicationDraft> Drafts { get; set; }
    }
}
