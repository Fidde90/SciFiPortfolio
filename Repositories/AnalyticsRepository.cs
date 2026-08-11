using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
namespace SciFiPortfolio.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly SciFiContext _sciFiContext;

        public AnalyticsRepository(SciFiContext sciFiContext)
        {
            _sciFiContext = sciFiContext;
        }

        public async Task<int> IncrementVisitorCountAsync()
        {
            return await _sciFiContext.Visitors
                .ExecuteUpdateAsync(x => 
                    x.SetProperty(
                        x => x.VisitorCount, 
                        x => x.VisitorCount + 1
                    ));
        }
    }
}
