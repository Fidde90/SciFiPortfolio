using SciFiPortfolio.Enums;

namespace SciFiPortfolio.Models
{
    public class ImageTopBottomSection : ContentSection
    {
        public List<Paragraph> Paragraphs { get; set; } = [];

        public List<Image> Images { get; set; } = [];

        public ImageTopBottomPosition ImageTopBottomPosition { get; set; }
    }
}
