using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace VolunteerManagement.Infrastructure.Data
{
    public class VolunteerManagementDbContextFactory : IDesignTimeDbContextFactory<VolunteerManagementDbContext>
    {
        public VolunteerManagementDbContext CreateDbContext(string[] args)
        {
            // Configura la conexión de base de datos
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<VolunteerManagementDbContext>();
            optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));

            return new VolunteerManagementDbContext(optionsBuilder.Options);
        }
    }
}
