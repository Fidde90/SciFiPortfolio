namespace SciFiPortfolio.Models.ContentSections
{
    public class ImageCenterSection : ContentSection
    {
        public List<Paragraph> ParagraphsTop { get; set; } = [];

        public List<Image> DesktopImages { get; set; } = [];

        public List<Image> MobileImages { get; set; } = [];

        public List<Paragraph> ParagraphsBottom { get; set; } = [];
    }
}
