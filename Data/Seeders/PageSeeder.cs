using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Interfaces;

namespace SciFiPortfolio.Data.Seeders
{
    public class PageSeeder : ISeeder
    {
        private readonly SciFiContext _context;
        private readonly ILogger<PageSeeder> _logger;

        private readonly ReceptakutenPageSeeder _receptakuten;
        private readonly ReceptakutenFrontendPageSeeder _receptakutenFrontend;
        private readonly ReceptakutenBackendPageSeeder _receptakutenBackend;
        private readonly VpsPageSeeder _vpsPageSeeder;
        private readonly SciFiPageSeeder _sciFiPageSeeder;
        private readonly MiscSeeder _miscSeeder;


        public PageSeeder(SciFiContext context, ILogger<PageSeeder> logger)
        {
            _context = context;
            _logger = logger;

            _receptakuten = new ReceptakutenPageSeeder(_context, _logger);
            _receptakutenFrontend = new ReceptakutenFrontendPageSeeder(_context, _logger);
            _receptakutenBackend = new ReceptakutenBackendPageSeeder(_context, _logger);
            _vpsPageSeeder = new VpsPageSeeder(_context, _logger);
            _sciFiPageSeeder = new SciFiPageSeeder(_context, _logger);
            _miscSeeder = new MiscSeeder(_context);
        }

        public async Task SeedAsync()
        {
            //Order is important!

            //1
            await _receptakuten.SeedAsync();

            //2
            await _receptakutenFrontend.SeedAsync();
            await _receptakutenBackend.SeedAsync();

            await _vpsPageSeeder.SeedAsync();
            await _sciFiPageSeeder.SeedAsync();

            await _miscSeeder.SeedAsync();
        }
    }
}



