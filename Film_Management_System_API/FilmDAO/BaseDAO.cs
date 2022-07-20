namespace Film_Management_System_API.FilmDAO
{
    public class BaseDAO
    {
        public static string CnString;
        public BaseDAO()
        {
           CnString = "Data Source=LAPTOP-IGR1158J\\SQLEXPRESS;Initial Catalog=Movies;Integrated Security=True";
        }
    }
}
