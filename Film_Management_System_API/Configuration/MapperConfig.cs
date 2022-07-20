using AutoMapper;
using Film_Management_System_API.DataModels;
using Film_Management_System_API.Models;

namespace Film_Management_System_API.Configuration
{
    public class MapperConfig:Profile
    {

        public MapperConfig()
        {
            CreateMap<FilmDTO, Film>().ReverseMap();
        }
    }
}
