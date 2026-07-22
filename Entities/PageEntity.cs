using System.ComponentModel.DataAnnotations;

namespace SciFiPortfolio.Entities
{
    public class PageEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 

        public string? ParentPageId { get; set; }

        public PageEntity? ParentPage { get; set; }

        public PageContentEntity? PageContent { get; set; }

        public ICollection<PageEntity> ChildPages { get; set; } = [];

        [MaxLength(100)]
        public string? Title { get; set; }

        [Required]
        public string Slug { get; set; } = null!;

        public bool Published { get; set; } = false;

        public DateTime? PublishedDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
