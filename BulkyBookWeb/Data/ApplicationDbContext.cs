using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;
//entity framework core is a modern object-database mapper for tracking changes, updates or schema migration
//entity framework core manages the database for you
namespace BulkyBookWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //maps or set the data model fields to the database table fields to create table automatically
        public DbSet<Category> Categories { get; set; }
    }
}
