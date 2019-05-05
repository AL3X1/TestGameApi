using Microsoft.EntityFrameworkCore;
using RedRiftTestTask.Infrastructure.Entities;

namespace RedRiftTestTask.Infrastructure.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        
        public DbSet<Lobby> Lobbies { get; set; }
        
        public DbSet<Player> Players { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}