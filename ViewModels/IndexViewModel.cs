using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.ViewModels
{
    public class IndexViewModel
    {
        public string Title { get; set; } = "";

        public HeroSection? Hero {  get; set; } 

        public CarouselSection? Carousel { get; set; }

        public CardSection Cards { get; set; } = new();

        public bool ShowTitle { get; set; } = true;
    }
}
