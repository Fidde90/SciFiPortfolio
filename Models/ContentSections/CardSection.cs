using System.Text.Json.Serialization;

namespace SciFiPortfolio.Models.ContentSections
{
    public class CardSection : ContentSection
    {
        public List<string> CardIds { get; set; } = [];

        [JsonIgnore]
        public List<ProjectCard> Cards { get; set; } = [];
    }
}
