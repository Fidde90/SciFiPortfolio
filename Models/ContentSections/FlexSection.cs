namespace SciFiPortfolio.Models.ContentSections
{
    public class FlexSection : ContentSection
    {
        public List<Image>? ImagesLeft { get; set; }

        public List<Image>? ImagesRight { get; set; }

        public List<Paragraph>? ParagraphsLeft { get; set; }

        public List<Paragraph>? ParagraphsRight { get; set; }

    }
}
