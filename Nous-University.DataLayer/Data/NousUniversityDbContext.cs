using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nous_University.DataLayer.Entities;

namespace Nous_University.DataLayer.Data;

public class NousUniversityDbContext : DbContext
{
    public NousUniversityDbContext(DbContextOptions<NousUniversityDbContext> options) : base(options)
    {
        
    }



    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Student> Students { get; set; }
}