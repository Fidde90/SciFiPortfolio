using SciFiPortfolio.Interfaces.Repositories;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models;

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
            {
                return null!;
            }

            var dbPage = await _pageRepo.GetPageBySlugAsync(slug);

            if(dbPage is null)
            {
                return null!;
            }

            var page = new Page
            {
                Id = dbPage.Id,
                Title = dbPage.Title,
                Slug = dbPage.Slug,
                Published = dbPage.Published,
                Sections = dbPage.PageContent.Sections,
                PublishedDate = dbPage.PublishedDate ?? DateTime.MinValue,
                CreatedAt = dbPage.CreatedAt,
                UpdatedAt = dbPage.UpdatedAt,
            };

            return page;
        }
    }
}
