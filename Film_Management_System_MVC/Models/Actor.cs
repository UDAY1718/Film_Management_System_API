namespace Film_Management_System_MVC.Models
{
    public class Actor
    {
        public Actor()
        {
            Films = new HashSet<Film>();
        }

        public decimal ActorId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public virtual ICollection<Film> Films { get; set; }
    }
}

