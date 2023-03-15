using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Nous_University.DataLayer.Data;

public class NousUniversityDbContextFactory : IDesignTimeDbContextFactory<NousUniversityDbContext>
{
    public NousUniversityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NousUniversityDbContext>();
        const string connString =
            @"Data Source=WORKSPACE\SQLEXPRESS;Initial Catalog=NousUniversityTwo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        optionsBuilder.UseSqlServer(connString);
        return new NousUniversityDbContext(optionsBuilder.Options);
        
    }
}