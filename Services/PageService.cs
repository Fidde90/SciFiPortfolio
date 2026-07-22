using SciFiPortfolio.Interfaces.Repositories;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepo;

        public PageService(IPageRepository pageRepo)
        {
            _pageRepo = pageRepo;
        }

        public async Task<Page> GetPageAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return null!;

            var dbPage = await _pageRepo.GetPageBySlugAsync(slug);

            if(dbPage is null)
                return null!;

            var pageContentSections = dbPage.PageContent?.Content.Sections;

            if (pageContentSections is null || !pageContentSections.Any())
                return null!;

            var cardSection = pageContentSections.OfType<CardSection>().FirstOrDefault();

            if(cardSection is not null)
            {
                var cardIds = cardSection.CardIds;
                var cards = await GetProjectCardsAsync(cardIds);
                cardSection.Cards = cards;
            }

            var page = new Page
            {
                Id = dbPage.Id,
                Title = dbPage.Title,
                Slug = dbPage.Slug,
                Published = dbPage.Published,
                Sections = pageContentSections,
                PublishedDate = dbPage.PublishedDate ?? DateTime.MinValue,
                CreatedAt = dbPage.CreatedAt,
                UpdatedAt = dbPage.UpdatedAt,
            };

            return page;
        }

        public async Task<List<ProjectCard>> GetProjectCardsAsync(List<string> cardIds)
        {
            var cards_db = await _pageRepo.GetProjcetCardsAsync(cardIds);

            if (cards_db is null || !cards_db.Any())
                return [];

            var cards = new List<ProjectCard>();

            foreach (var c in cards_db)
            {
                var card = new ProjectCard
                {
                    Title = c.Title,
                    Image = 
                    { 
                        ImageUrl = c.ImageUrl, 
                        AltText = c.ImageAltText 
                    },
                    Hyperlink = 
                    { 
                        LinkUrl = c.Hyperlink.LinkUrl, 
                        LinkText = c.Hyperlink.LinkText, 
                        IconName = c.Hyperlink.IconName 
                    },
                    AppLink = 
                    { 
                        LinkUrl = c.AppLink.LinkUrl, 
                        LinkText = c.AppLink.LinkText, 
                        PageId = c.AppLink.PageId, 
                        PageSlug = c.AppLink.PageSlug 
                    },
                };

                foreach (var t in c.Tags)
                {
                    var tag = new Tag
                    {
                        TagText = t.Text
                    };

                    card.Tags.Add(tag);
                }

                cards.Add(card);
            }

            return cards;
        }
    }
}
