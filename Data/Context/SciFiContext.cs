using Microsoft.EntityFrameworkCore;

namespace SciFiPortfolio.Data.Context
{
    public class SciFiContext : DbContext
    {
        public SciFiContext(DbContextOptions<SciFiContext> options): base(options)
        {
        }












        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
