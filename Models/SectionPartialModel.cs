using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Models
{
    public class SectionPartialModel
    {
        public string PartialName { get; set; } = "";
        public ContentSection Section { get; set; } = null!;
    }
}
