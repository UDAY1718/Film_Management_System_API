using Film_Management_System_API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Film_Management_System_API.FilmDAO
{
    public class MovieDAO: BaseDAO
    {
        public Film GetFilmByTitle(string t)
        {

            Film f = new Film();
           
                using (SqlConnection con = new SqlConnection(CnString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT Title, Release_Year, Rating FROM FILMS WHERE CONVERT(VARCHAR, Title) = '" + t + "'  ", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        f.Title = rdr.GetString("Title");
                        f.ReleaseYear = rdr.GetString("Release_Year");
                        f.Rating = Convert.ToInt32(rdr[2]);

                    }
                    con.Close();
                }
            return f;
        }
           

            
        }
    }

