using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.ViewModels
{
    public class TestViewModel
    {
        public HeroSection? Hero { get; set; }

        public List<ContentSection> Sections { get; set; } = [];

        public bool ShowTitle { get; set; } = true;
    }
}
