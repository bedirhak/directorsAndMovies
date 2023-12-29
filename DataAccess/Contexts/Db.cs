using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DataAccess.Contexts
{
    public class Db : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }

        public Db(DbContextOptions options) : base(options)
        {

        }
    }
}
