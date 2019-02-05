using IMDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMDB.Data
{
    public class IMDBDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

        public IMDBDbContext(DbContextOptions<IMDBDbContext>options):base(options)
        {

        }
    }
}
