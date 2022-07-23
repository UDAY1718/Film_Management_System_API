namespace Film_Management_System_MVC.Models
{
    public class Category
    {
        public Category()
        {
            Films = new HashSet<Film>();
        }

        public decimal CategoryId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Film> Films { get; set; }
    }
}

