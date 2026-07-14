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
                .FirstOrDefaultAsync(page => page.Slug == slug);
        }
    }
}
