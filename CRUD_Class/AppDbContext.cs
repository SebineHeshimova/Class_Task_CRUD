using CRUD_Class.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Class
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Sliders> Sliders { get; set; }
        public DbSet<Features> Features { get; set; }
    }
}
