using SciFiPortfolio.Models;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.ViewModels
{
    public class HostingViewModel
    {
        public HeroSection? Hero { get; set; }

        public Page Page { get; set; } = null!;

        public bool ShowTitle { get; set; } = true;
    }
}
