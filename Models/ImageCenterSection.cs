namespace SciFiPortfolio.Models
{
    public class ImageCenterSection : ContentSection
    {
        public List<Paragraph> ParagraphsTop { get; set; } = [];

        public List<Image> Images { get; set; } = [];

        public List<Paragraph> ParagraphsBottom { get; set; } = [];
    }
}
