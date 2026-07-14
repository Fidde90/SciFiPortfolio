using System.ComponentModel.DataAnnotations;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Entities
{
    public class PageEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Title { get; set; }

        [Required]
        public string Slug { get; set; } = null!;

        public bool Published { get; set; } = false;

        [Required]
        public JsonPageContent PageContent { get; set; } = new();

        public DateTime? PublishedDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
