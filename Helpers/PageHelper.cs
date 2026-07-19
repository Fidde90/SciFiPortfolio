using SciFiPortfolio.Models;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Helpers
{
    public class PageHelper 
    {

        /// <summary>
        ///     Get a list without a HeroSection.
        ///     returns an empty list if the Page parameter is null.
        /// </summary>
        /// <param name="page">a list of ContentSections</param>
        /// <returns>returns a new IReadOnlyList<ContentSection> without a HeroSection</returns>
        public static List<ContentSection> FilterHero(Page page)
        {
            if(page is not null)
            {
                return page.Sections = page.Sections
                         .Where(s => s is not HeroSection)
                         .ToList();
            }
            return [];
        }

        public static HeroSection? GetHeroSection(Page page)
        {
            if(page is not null)
            {
                return page.Sections.OfType<HeroSection>().FirstOrDefault();
            }

            return null;
        }
    }
}
