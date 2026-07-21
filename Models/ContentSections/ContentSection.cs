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
    [JsonDerivedType(typeof(DockerSection), "docker-section")]
    [JsonDerivedType(typeof(FlexSection), "flex-section")]

    public abstract class ContentSection
    {
        public string? Heading { get; set; }

        public string? SubHeading { get; set; }

        public bool SpaceBottom { get; set; } = true;

        public string BackgroundColor { get; set; } = "";

        public bool Paddings { get; set; } = true;
    }
}
