namespace labOpp.Model
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Навигационные свойства
        public ICollection<Application> Applications { get; set; }
        public ICollection<ApplicationDraft> Drafts { get; set; }
    }
}
