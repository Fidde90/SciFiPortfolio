using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Models
{
    public class Page
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string Slug { get; set; } = null!;

        public bool Published { get; set; } = false;

        public List<ContentSection> Sections { get; set; } = [];

        public DateTime PublishedDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
