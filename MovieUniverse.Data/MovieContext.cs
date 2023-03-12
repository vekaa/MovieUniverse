using Microsoft.EntityFrameworkCore;
using MovieUniverse.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieUniverse.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext()
        {

        }
        public MovieContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MovieUniverse");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
        }
    }
}
