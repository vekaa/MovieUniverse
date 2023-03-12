using Microsoft.EntityFrameworkCore;
using MovieUniverse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieUniverse.Data
{
    public class MovieRepository:IMovieRepository
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
    }
}
