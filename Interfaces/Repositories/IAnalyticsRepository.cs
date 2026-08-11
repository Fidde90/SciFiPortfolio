namespace SciFiPortfolio.Interfaces.Repositories
{
    public interface IAnalyticsRepository
    {
        Task<int> IncrementVisitorCountAsync();
    }
}
