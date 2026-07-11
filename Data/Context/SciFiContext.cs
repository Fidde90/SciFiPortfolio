using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Entities;
using SciFiPortfolio.Models;
using System.Text.Json;

namespace SciFiPortfolio.Data.Context
{
    public class SciFiContext(DbContextOptions<SciFiContext> options) : DbContext(options) 
    {
        public DbSet<PageEntity> Pages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PageEntity>()
                .Property(x => x.PageContent)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<JsonPageContent>(
                            v,
                            JsonSerializerOptions.Default
                            ) ?? new JsonPageContent()
                    );
        }
    }
}
