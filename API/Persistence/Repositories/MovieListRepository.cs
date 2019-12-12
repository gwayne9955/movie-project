using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using movie_project.API.Domain.Repositories;
using movie_project.Models;
using movie_project.Data;

namespace movie_project.API.Persistence.Repositories
{
    public class MovieListRepository : BaseRepository, IMovieListRepository
    {
        public MovieListRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MovieList>> ListAsync()
        {
            return await _context.MovieLists.ToListAsync();
        }

        public async Task AddAsync(MovieList movieList)
        {
            await _context.MovieLists.AddAsync(movieList);
        }

        public async Task<MovieList> FindByIdAsync(int id)
        {
            return await _context.MovieLists.FindAsync(id);
        }

        public void Update(MovieList movieList)
        {
            _context.MovieLists.Update(movieList);
        }

        public void Remove(MovieList movieList)
        {
            _context.MovieLists.Remove(movieList);
        }
    }
}
