using System.ComponentModel.DataAnnotations;

namespace labOpp.Model
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
