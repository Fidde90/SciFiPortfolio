using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Entities;

namespace SciFiPortfolio.Data.Seeders
{
    public class MiscSeeder
    {
        private readonly SciFiContext _context;

        public MiscSeeder(SciFiContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            //Order is important!

            //1
            await SeedTagsAsync();
            //2
            await SeedProjectCardsAsync(); 
        }

        public async Task SeedProjectCardsAsync()
        {
            var tagLookup = await _context.Tags
            .ToDictionaryAsync(x => x.Name);

            var vpsHostingId = await _context.Pages.Where(x => x.Slug == "vps-hosting").Select(x => x.Id).FirstOrDefaultAsync();
            var receptakutenId = await _context.Pages.Where(x => x.Slug == "receptakuten").Select(x => x.Id).FirstOrDefaultAsync();
            var sciFiPortId = await _context.Pages.Where(x => x.Slug == "sci-fi-portfolio").Select(x => x.Id).FirstOrDefaultAsync(); ;

            var cards = new List<ProjectCardEntity>()
            {
                new()
                {
                    Title = "SPA Applikation",
                    UniqueName = "receptakuten",
                    ImageUrl = "images/recept-bil-ub.png",
                    ImageAltText = "receptakuten image",
                    Hyperlink = { LinkText = "Testa den här", LinkUrl = "https://receptakuten.net", IconName = "fa-solid fa-arrow-right" },
                    AppLink = { LinkText = "Läs mer om appen", LinkUrl = "/Projects/Receptakuten", PageSlug = "receptakuten", PageId = receptakutenId },
                    Tags = {
                        tagLookup[".net-api"],
                        tagLookup["vue-js"],
                        tagLookup["postgresql"],
                        tagLookup["identity"],
                    }
                },
                new()
                {
                    Title = "Vps hosting",
                    UniqueName = "vps-hosting",
                    ImageUrl = "images/server.png",
                    ImageAltText = "image of a server",
                    AppLink = { LinkText = "Läs mer", LinkUrl = "Projects/Hosting", PageSlug = "vps-hosting", PageId = vpsHostingId },
                    Tags = {
                        tagLookup["nginx"],
                        tagLookup["ubuntu-server"],
                        tagLookup["docker"],
                        tagLookup["hostinger"],
                    }
                },
                new()
                {
                    Title = "Sci-fi portfolio",
                    UniqueName = "sci-fi-portfolio",
                    ImageUrl = "images/port-thumb.png",
                    ImageAltText = "a sci-fi image",
                    AppLink = { LinkText = "Läs mer", LinkUrl = "Projects/sci-fi-portfolio", PageSlug = "sci-fi-portfolio", PageId = sciFiPortId },
                    Tags = {
                        tagLookup["razor-pages"],
                        tagLookup["vanilla-javascript"],
                        tagLookup["ssr"],
                        tagLookup["custom-cms"]
                    }
                }
            };

            var existingCards = await _context.ProjectCards
               .Select(x => x.UniqueName)
               .ToListAsync();

            var newCards = cards
                .Where(x => !existingCards.Contains(x.UniqueName))
                .ToList();

            if (newCards.Count <= 0)
                return;

            _context.ProjectCards.AddRange(newCards);
            await _context.SaveChangesAsync();
        }

        public async Task SeedTagsAsync()
        {
            var tags = new List<TagEntity>()
            {
                new() { Name = ".net-api", Text = ".Net API" },
                new() { Name = "vue-js", Text = "Vue.js" },
                new() { Name = "postgresql", Text = "PostgreSql" },
                new() { Name = "identity", Text = "Identity" },
                new() { Name = "nginx", Text = "NginX" },
                new() { Name = "ubuntu-server", Text = "Ubuntu Server" },
                new() { Name = "docker", Text = "Docker" },
                new() { Name = "hostinger", Text = "Hostinger" },
                new() { Name = "razor-pages", Text = "Razor pages" },
                new() { Name = "vanilla-javascript", Text = "Vanilla javascript" },
                new() { Name = "ssr", Text = "SSR" },
                new() { Name = "custom-cms", Text = "Custom CMS" },
            };

            var existingNames = await _context.Tags
                .Select(x => x.Name)
                .ToListAsync();

            var newTags = tags
                .Where(x => !existingNames.Contains(x.Name))
                .ToList();

            if (newTags.Count <= 0)
                return;

            _context.Tags.AddRange(newTags);
            await _context.SaveChangesAsync();
        }
    }
}
