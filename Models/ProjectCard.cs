namespace SciFiPortfolio.Models
{
    public class ProjectCard
    {
        public string Title { get; set; } = "";

        public Image Image { get; set; } = new();

        public HyperLink Hyperlink { get; set; } = new();

        public AppLink AppLink { get; set; } = new();

        public List<Tag> Tags { get; set; } = [];
    }
}
