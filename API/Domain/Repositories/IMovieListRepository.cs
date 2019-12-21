using System.Threading.Tasks;
using movie_project.API.Domain.Queries;
using movie_project.Models;

namespace movie_project.API.Domain.Repositories
{
    public interface IMovieListRepository
    {
        Task<QueryResult<MovieList>> ListAsync(MovieListsQuery query);
        Task AddAsync(MovieList movieList);
        Task<MovieList> FindByIdAsync(int id);
        void Update(MovieList movieList);
        void Remove(MovieList movieList);
    }
}
