using SciFiPortfolio.Enums;

namespace SciFiPortfolio.Models.ContentSections
{
    public class ImageWithPositionSection : ContentSection
    {
        public List<Paragraph> Paragraphs { get; set; } = [];

        public Image DesktopImage { get; set; } = new();

        public Image MobileImage { get; set; } = new();

        public ImagePosition ImagePosition { get; set; }
    }
}
