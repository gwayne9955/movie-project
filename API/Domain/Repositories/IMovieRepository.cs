using System.Threading.Tasks;
using movie_project.Models;

namespace movie_project.API.Domain.Repositories
{
    public interface IMovieRepository
    {
        Task AddAsync(Movie movie);
        Task<Movie> FindByIdAsync(int id, string imdbID);
        void Update(Movie movie);
        void Remove(Movie movie);
    }
}