using System.ComponentModel.DataAnnotations;

namespace labOpp.Model
{
    public class ApplicationDraft
    {
        [Key]
        public Guid DraftID { get; set; }
        public Guid UserID { get; set; }
        public string ActivityTypeID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Plan { get; set; }
        public DateTime SubmissionDate { get; set; }

        public User User { get; set; }
        public Activity Activity { get; set; }

    }
}
