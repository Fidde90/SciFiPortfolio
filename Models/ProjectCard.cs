namespace SciFiPortfolio.Models
{
    public class ProjectCard
    {
        public string Title { get; set; } = "";

        public Image Image { get; set; } = new();

        public LinkButton LinkButton { get; set; } = new();

        public Link Link { get; set; } = new();

        public List<Tag> Tags { get; set; } = [];
    }
}
