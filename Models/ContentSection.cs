namespace SciFiPortfolio.Models
{
    public abstract class ContentSection
    {
        public string? Heading { get; set; }

        public string? SubHeading { get; set; }

        public bool SpaceBottm { get; set; } = true;

        public bool BackgroundColor { get; set; } = false;
    }
}
