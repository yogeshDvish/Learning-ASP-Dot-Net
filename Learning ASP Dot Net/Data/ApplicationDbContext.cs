using Learning_ASP_Dot_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Learning_ASP_Dot_Net.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //this will create a table called Students in the database
        public DbSet<Student> Students { get; set; }
    }
}
