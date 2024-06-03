using System.ComponentModel.DataAnnotations;

namespace labOpp.Model
{
    public class Application
    {
        [Key]
        public Guid ApplicationID { get; set; }
        
        public Guid UserID { get; set; }
        
        public Guid ActivityTypeID { get; set; }

		public Guid PlatformId { get; set; }

		public string Title { get; set; }
        
        public string ShortDescription { get; set; }
        
        public string Plan { get; set; }
       
        public DateTime SubmissionDate { get; set; }       
    }
}
