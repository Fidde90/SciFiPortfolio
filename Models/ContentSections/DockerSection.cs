namespace SciFiPortfolio.Models.ContentSections
{
    public class DockerSection : ContentSection
    {
        public Paragraph LeftParagraph { get; set; } = new();

        public Paragraph RightParagraph { get; set; } = new();
    }
}
