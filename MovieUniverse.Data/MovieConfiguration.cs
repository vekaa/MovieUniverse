using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieUniverse.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieUniverse.Data
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(m => m.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(m => m.ShortDescription)
                .HasMaxLength(1000);

            builder.Property(m => m.Genres).HasConversion(
                g => JsonConvert.SerializeObject(g, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                g => JsonConvert.DeserializeObject<List<Genre>>(g, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }
}
