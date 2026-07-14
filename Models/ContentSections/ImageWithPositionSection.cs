using SciFiPortfolio.Enums;

namespace SciFiPortfolio.Models.ContentSections
{
    public class ImageWithPositionSection : ContentSection
    {
        public List<Paragraph> Paragraphs { get; set; } = [];

        public Image Image { get; set; } = new();

        public ImagePosition ImagePosition { get; set; }
    }
}
