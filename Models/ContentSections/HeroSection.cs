namespace SciFiPortfolio.Models.ContentSections
{
    public class HeroSection : ContentSection
    {
        public Image? Image { get; set; }

        public string HeroImageSizeCssClass { get; set; } = "large";
    }
}
