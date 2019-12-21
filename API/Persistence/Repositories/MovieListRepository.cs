using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using movie_project.API.Domain.Repositories;
using movie_project.Models;
using movie_project.Data;
using movie_project.API.Domain.Queries;
using System.Linq;

namespace movie_project.API.Persistence.Repositories
{
    public class MovieListRepository : BaseRepository, IMovieListRepository
    {
        public MovieListRepository(ApplicationDbContext context) : base(context) {}

        public async Task<QueryResult<MovieList>> ListAsync(MovieListsQuery query)
        {
            IQueryable<MovieList> queryable = _context.MovieLists
                                                    .Include(m => m.ApplicationUser)
                                                    .Include(m => m.Movies);

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            if (query.ApplicationUserRefId != null && query.ApplicationUserRefId.Length > 0)
            {
                queryable = queryable.Where(m => m.ApplicationUserRefId.Equals(query.ApplicationUserRefId));
            }

            // Here I count all items present in the database for the given query, to return as part of the pagination data.
            int totalItems = await queryable.CountAsync();

            // Here I apply a simple calculation to skip a given number of items, according to the current page and amount of items per page,
            // and them I return only the amount of desired items. The methods "Skip" and "Take" do the trick here.
            int skip = (query.Page - 1) * query.ItemsPerPage;
            int display = Math.Min(totalItems - skip, query.ItemsPerPage);
            List<MovieList> movieLists = await queryable.Skip(skip)
                                                    .Take(display)
                                                    .ToListAsync();

            // Finally I return a query result, containing all items and the amount of items in the database (necessary for client calculations of pages).
            return new QueryResult<MovieList>
            {
                Items = movieLists,
                TotalItems = totalItems,
            };
        }

        public async Task AddAsync(MovieList movieList)
        {
            await _context.MovieLists.AddAsync(movieList);
        }

        public async Task<MovieList> FindByIdAsync(int id)
        {
            return await _context.MovieLists
                                 .Include(m => m.ApplicationUser)
                                 .Include(m=> m.Movies)
                                 .FirstOrDefaultAsync(p => p.MovieListId == id);
            // Since Include changes the method return, we can't use FindAsync
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
