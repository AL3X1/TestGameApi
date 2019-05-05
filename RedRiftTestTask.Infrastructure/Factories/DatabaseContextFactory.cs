using Microsoft.EntityFrameworkCore.Design;
using RedRiftTestTask.Infrastructure.Contexts;

namespace RedRiftTestTask.Infrastructure.Factories
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            return new ApplicationDbContext(DbContextOptionsFactory.Get<ApplicationDbContext>());
        }
    }
}