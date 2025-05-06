using Microsoft.EntityFrameworkCore;
using PerschoolTaskIMGCrud.Models;

namespace PerschoolTaskIMGCrud.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        
        public DbSet<PopularTeacher> PopularTeachers { get; set; }
    }
}
