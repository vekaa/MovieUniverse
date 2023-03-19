using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieUniverse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieUniverse.Data
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext dbContext;

        public MovieRepository(MovieContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //We do this to avoid unnecessary calls to change tracker
        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await dbContext.Movies.AsNoTracking().ToListAsync();
        }

        public async Task<Movie?> GetMovieById(int id)
        {
            return await dbContext.Movies.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> Create(Movie movie)
        {
            var result = await dbContext.Movies.AddAsync(movie);
            await dbContext.SaveChangesAsync();
            return result.Entity; //Movie
        }

        public async Task<Movie> Update(int oldId, Movie newMovie)
        {
            var movie = await dbContext.Movies.FirstOrDefaultAsync(m => m.Id == oldId);
            if (movie != null)
            {
                movie.ReleaseDate = newMovie.ReleaseDate;
                movie.Title = newMovie.Title;
                movie.Genres = newMovie.Genres;
                movie.ShortDescription = newMovie.ShortDescription;

                await dbContext.SaveChangesAsync();

                return movie;
            }
            else
            {
                return await Task.FromResult<Movie>(null);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var movie = await dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie != null)
            {
                dbContext.Movies.Remove(movie);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
