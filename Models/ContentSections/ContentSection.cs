using System.Text.Json.Serialization;

namespace SciFiPortfolio.Models.ContentSections
{
    [JsonDerivedType(typeof(TextSection), "text-section")]
    [JsonDerivedType(typeof(ImageCenterSection), "image-center-section")]
    [JsonDerivedType(typeof(ImageWithPositionSection), "positioned-image-section")]
    [JsonDerivedType(typeof(ImageSection), "image-section")]
    [JsonDerivedType(typeof(ListSection), "list-section")]
    [JsonDerivedType(typeof(CardSection), "card-section")]
    [JsonDerivedType(typeof(HeroSection), "hero-section")]
    [JsonDerivedType(typeof(CarouselSection),"carousel-section")]
    public abstract class ContentSection
    {
        public string? Heading { get; set; }

        public string? SubHeading { get; set; }

        public bool SpaceBottm { get; set; } = true;

        public bool BackgroundColor { get; set; } = false;
    }
}
