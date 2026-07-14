using SciFiPortfolio.Enums;

namespace SciFiPortfolio.Models
{
    public class ImageWithPositionSection : ContentSection
    {
        public List<Paragraph> Paragraphs { get; set; } = [];

        public List<Image> Images { get; set; } = [];

        public ImagePosition ImagePosition { get; set; }
    }
}
