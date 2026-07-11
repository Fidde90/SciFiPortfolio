using SciFiPortfolio.Enums;

namespace SciFiPortfolio.Models
{
    public class ImageLeftRightSection : ContentSection
    {
        public List<Paragraph> Paragraphs { get; set; } = [];

        public List<Image> Images { get; set; } = [];

        public ImageLeftRightPosition ImageLeftRightPosition { get; set; }
    }
}
