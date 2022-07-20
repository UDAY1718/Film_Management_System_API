using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Film_Management_System_API.Models;
using Film_Management_System_API.Infrastructure;
using Film_Management_System_API.DataModels;
using AutoMapper;
using Film_Management_System_API.FilmDAO;

namespace Film_Management_System_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IMapper mapper;

        private readonly MoviesContext _context;

        public FilmsController(MoviesContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }


        // GET: api/Films

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            return await _context.Films.ToListAsync();
        }

        [HttpGet("id:int")]
        
        // GET: api/Films/5
        
        
        public IActionResult GetFilm(decimal id)
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            var film = _context.Films.Find(id);

            if (film == null)
            {
                return NotFound();
            }

            return Ok(film);
        }

        
        [HttpGet("movieid")]
        public IActionResult GetFilmByName(string movieid)
        {
            MovieDAO movieDAO = new MovieDAO();
            return Ok(movieDAO.GetFilmByTitle(movieid));
        }
        /* [HttpGet("{Language}")]


        /*[HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Film>>> Search(string name, Film? fil)

        {
            try
            {
                var result = await movieRepository.Search(name, fil);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }*/

        // PUT: api/Films/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        
        public async Task<IActionResult> PutFilm(decimal id, Film film)
        {
            if (id != film.FilmId)
            {
                return BadRequest();
            }

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Films

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       
        [HttpPost]
    
        public async Task<ActionResult<FilmDTO>> PostFilm(FilmDTO film)
        {
           
            
            var f = mapper.Map<Film>(film);
            await _context.Films.AddAsync(f);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilm", new { id = f.FilmId }, f);
        }

        // DELETE: api/Films/5
        
        [HttpDelete("{id}")]
       
        public async Task<IActionResult> DeleteFilm(decimal id)
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmExists(decimal id)
        {
            return (_context.Films?.Any(e => e.FilmId == id)).GetValueOrDefault();
        }
    }
}
