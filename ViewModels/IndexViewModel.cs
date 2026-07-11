using SciFiPortfolio.Models;

namespace SciFiPortfolio.ViewModels
{
    public class IndexViewModel
    {
        public IReadOnlyList<Image> TechIconsSlider { get; set; } = [];

        public IReadOnlyList<ContentSection> ContentSections { get; set; } = [];

        public IReadOnlyList<ProjectCard> ProjectCards { get; set; } = [];

    }
}
