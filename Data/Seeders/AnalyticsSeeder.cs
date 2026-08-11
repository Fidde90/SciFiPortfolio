using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Entities;
using SciFiPortfolio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SciFiPortfolio.Data.Seeders
{
    public class AnalyticsSeeder : ISeeder
    {
        private readonly SciFiContext _context;

        public AnalyticsSeeder(SciFiContext sciFiContext)
        {
            _context = sciFiContext;
        }

        public async Task SeedAsync()
        {

            if(!await _context.Visitors.AnyAsync())
            {
                var theOnlyTableRow = new VisitorEntity
                {
                    VisitorCount = 0
                };

                _context.Add(theOnlyTableRow);
                await _context.SaveChangesAsync();
            }
        }
    }
}
