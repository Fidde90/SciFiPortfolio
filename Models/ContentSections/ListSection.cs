namespace SciFiPortfolio.Models.ContentSections
{
    public class ListSection : ContentSection
    {
        public List<Paragraph>? Paragraphs { get; set; } 

        public List<ListItem> ListItems { get; set; } = [];
    }
}
