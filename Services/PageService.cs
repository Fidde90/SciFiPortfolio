using SciFiPortfolio.Entities;
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

        public async Task<Page?> GetPageAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return null!;

            var dbPage = await _pageRepo.GetPageBySlugAsync(slug);

            if(dbPage is null)
                return null!;

            var pageContentSections = dbPage.PageContent?.Content.Sections;

            if (pageContentSections is not null && pageContentSections.Any())
            {
                var cardSection = pageContentSections.OfType<CardSection>().FirstOrDefault();

                if (cardSection is not null)
                {
                    var cardIds = cardSection.CardIds;
                    var cards = await GetProjectCardsAsync(cardIds);
                    cardSection.Cards = cards;
                }
            }

            var page = ToPageModel(dbPage);

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

        public List<Page> ToPageModels(IEnumerable<PageEntity> entities)
        {
            if (entities is null || !entities.Any())
                return [];

            var returnList = new List<Page>();

            foreach (var p in entities)
            {
                var page = new Page
                {
                    Id = p.Id,
                    Title = p.Title,
                    Slug = p.Slug,
                    Published = p.Published,
                    Sections = p.PageContent?.Content.Sections ?? [],
                    ChildPages = ToPageModels(p.ChildPages.ToList()),
                    PublishedDate = p.PublishedDate ?? DateTime.MinValue,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                };

                returnList.Add(page);
            }
           
            return returnList;
        }

        public Page? ToPageModel(PageEntity entity)
        {
            if (entity is null)
                return null;

            var page = new Page
            {
                Id = entity.Id,
                Title = entity.Title,
                Slug = entity.Slug,
                Published = entity.Published,
                Sections = entity.PageContent?.Content.Sections ?? [],
                ChildPages = ToPageModels(entity.ChildPages),
                PublishedDate = entity.PublishedDate ?? DateTime.MinValue,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
            };

            return page;
        }
    }
}
