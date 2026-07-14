using SciFiPortfolio.Models;

namespace SciFiPortfolio.ViewModels
{
    public class IndexViewModel
    {
        public IReadOnlyList<Image> TechIconsSlider { get; set; } = [];

        public Page Page { get; set; } = null!;
    }
}
