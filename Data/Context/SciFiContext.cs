using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Entities;
using SciFiPortfolio.Models.ContentSections;
using System.Text.Json;

namespace SciFiPortfolio.Data.Context
{
    public class SciFiContext(DbContextOptions<SciFiContext> options) : DbContext(options) 
    {
        public DbSet<PageEntity> Pages { get; set; }

        public DbSet<PageContentEntity> PageContents { get; set; }

        public DbSet<ProjectCardEntity> ProjectCards { get; set; }

        public DbSet<TagEntity> Tags { get; set; }

        public DbSet<VisitorEntity> Visitors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PageContentEntity>()
                .Property(x => x.Content)
                .HasColumnType("jsonb")
                .HasConversion(
                    content => JsonSerializer.Serialize(content, JsonSerializerOptions.Default),
                    json => JsonSerializer.Deserialize<JsonPageContent>(
                        json,
                        JsonSerializerOptions.Default
                    ) ?? new()
                );

            modelBuilder.Entity<ProjectCardEntity>(card =>
            {
                card.OwnsOne(card => card.Hyperlink);
                card.OwnsOne(c => c.AppLink);
            });

            modelBuilder.Entity<PageEntity>()
                .HasOne(p => p.ParentPage)
                .WithMany(p => p.ChildPages)
                .HasForeignKey(p => p.ParentPageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PageContentEntity>()
                .HasOne(pc => pc.Page)
                .WithOne(p => p.PageContent)
                .HasForeignKey<PageContentEntity>(pc => pc.PageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TagEntity>()
                .HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
