namespace SciFiPortfolio.Models.ContentSections
{
    public class ImageSection : ContentSection
    {
        public List<Image> DesktopImages { get; set; } = [];
        public List<Image> MobileImages { get; set; } = [];
    }
}
