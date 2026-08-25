using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;

        public PageService(IPageRepository pageRepo, IMemoryCache memoryCache)
        {
            _pageRepo = pageRepo;
            _memoryCache = memoryCache;
        }

        public async Task<Page?> GetPageAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                return null!;

            var cacheKey = $"page-{slug}";

            var dbPage = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);
                    Console.WriteLine($"##################################### DATABASE QUERY FOR PAGE -- {slug}");
                    return await _pageRepo.GetPageBySlugAsync(slug);
                });

            if (dbPage is null)
                return null!;

            var pageContentSections = dbPage.PageContent?.Content.Sections;

            if (pageContentSections is not null && pageContentSections.Any())
            {
                var cardSection = pageContentSections.OfType<CardSection>().FirstOrDefault();

                if (cardSection is not null)
                    cardSection.Cards = await GetProjectCardsAsync(cardSection.CardIds);
            }

            var page = ToPageModel(dbPage);

            return page;
        }

        public async Task<List<ProjectCard>> GetProjectCardsAsync()
        {
            var cards_db = await _memoryCache.GetOrCreateAsync("project-card-entities", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);
                Console.WriteLine("########################## DATABASE QUERY FOR ALL CARDS");
                return await _pageRepo.GetProjcetCardsAsync();
            }); 

            if (cards_db is null)
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

        public async Task<List<ProjectCard>> GetProjectCardsAsync(int cardCount)
        {
            string cacheKey = $"project-card-entities-count-{cardCount}";

            var cards_db = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);
                Console.WriteLine($"########################## DATABASE QUERY FOR CARDCOUNT -- {cacheKey}");
                return await _pageRepo.GetProjcetCardsAsync(cardCount);
            });

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

        public async Task<List<ProjectCard>> GetProjectCardsAsync(List<string> cardIds)
        {
            string idsForCacheKey = "";

            foreach (var cardId in cardIds)
                idsForCacheKey += cardId;

            string cacheKey = $"project-card-entities-by-ids-{idsForCacheKey}";

            var cards_db = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7);
                Console.WriteLine($"########################## DATABASE QUERY FOR CARDS BY IDS -- {cacheKey}");
                return await _pageRepo.GetProjcetCardsAsync(cardIds);
            });

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
