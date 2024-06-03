using System.ComponentModel.DataAnnotations;

namespace labOpp.Model
{
    public class Activity
    {
        [Key]
        public Guid ActivityID { get; set; }

        public string Name { get; set; }
    }
}
