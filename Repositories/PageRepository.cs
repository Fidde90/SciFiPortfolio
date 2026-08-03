using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Entities;
using SciFiPortfolio.Interfaces.Repositories;

namespace SciFiPortfolio.Repositories
{
    public class PageRepository : IPageRepository
    {
        private readonly SciFiContext _context; 

        public PageRepository(SciFiContext context)
        {
            _context = context;
        }

        public async Task<PageEntity?> GetPageBySlugAsync(string slug)
        {
            return await _context.Pages
                .Include(p => p.ChildPages)
                .Include(p => p.ParentPage)
                .Include(p => p.PageContent)
                .FirstOrDefaultAsync(page => page.Slug == slug);
        }

        public async Task<List<ProjectCardEntity>?> GetProjcetCardsAsync()
        {
            return await _context.ProjectCards
                .Include(p => p.Tags)
                .ToListAsync();
        }

        public async Task<List<ProjectCardEntity>?> GetProjcetCardsAsync(List<string> cardIds)
        {
            return await _context.ProjectCards
                .Include(p => p.Tags)
                .Where(p => cardIds.Contains(p.Id))
                .ToListAsync();     
        }
    }
}
