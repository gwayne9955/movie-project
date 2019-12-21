using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using movie_project.API.Domain.Repositories;
using movie_project.Models;
using movie_project.Data;
using System.Linq;

namespace movie_project.API.Persistence.Repositories
{
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context) : base(context) { }

        public async Task AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
        }

        public async Task<Movie> FindByIdAsync(int id, string imdbID)
        {
            return await _context.Movies
                                 .Where(m => m.imdbID.Equals(imdbID))
                                 .FirstOrDefaultAsync(m => m.MovieListRefId == id);
            // Since Include changes the method return, we can't use FindAsync
        }

        public void Update(Movie movie)
        {
            _context.Movies.Update(movie);
        }

        public void Remove(Movie movie)
        {
            _context.Movies.Remove(movie);
        }
    }
}
