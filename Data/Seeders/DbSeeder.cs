using SciFiPortfolio.Interfaces;

namespace SciFiPortfolio.Data.Seeders
{
    public class DbSeeder
    {
        private readonly IEnumerable<ISeeder> _seeders;

        public DbSeeder(IEnumerable<ISeeder> seeders)
        {
            _seeders = seeders;
        }

        public async Task SeedAsync()
        {
            foreach (var seeder in _seeders)
            {
                await seeder.SeedAsync();
            }
        }
    }
}
