using SciFiPortfolio.Models.ContentSections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SciFiPortfolio.Entities
{
    public class PageContentEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();


        [ForeignKey(nameof(Page))]
        public string PageId { get; set; } = null!;

        public PageEntity Page { get; set; } = null!;

        [Required]
        public JsonPageContent Content { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
