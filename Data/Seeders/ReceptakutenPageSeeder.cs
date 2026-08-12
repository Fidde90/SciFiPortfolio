using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Entities;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Data.Seeders
{
    public class ReceptakutenPageSeeder
    {
        private readonly SciFiContext _context;
        private readonly ILogger<PageSeeder> _logger;

        public ReceptakutenPageSeeder(SciFiContext context, ILogger<PageSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            var receptakuten = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == "receptakuten");

            if (receptakuten is null)
            {
                receptakuten = new PageEntity
                {
                    Title = "Receptakuten",
                    Slug = "receptakuten",
                    Published = true,
                    PublishedDate = DateTime.UtcNow
                };

                _context.Pages.Add(receptakuten);
                await _context.SaveChangesAsync();

                _logger.LogInformation(":::::: Receptakuten Page Seeded ::::::");
            }

            if (receptakuten is not null)
            {
                var content = await _context.PageContents.FirstOrDefaultAsync(x => x.PageId == receptakuten.Id);

                if (content is null)
                {
                    var receptakutenPageContent = new PageContentEntity
                    {
                        PageId = receptakuten.Id,
                        Content =
                        {
                            Sections =
                            {
                                new HeroSection
                                {
                                    Image = new() { ImageUrl = "images/recept-bil-ub.png" },
                                    HeroImageSizeCssClass = "small",
                                },
                                new TextSection
                                {
                                    Heading = "",
                                    Paragraphs =
                                    {
                                        new()
                                        { Text = "ReceptAkuten är en webbapp där användare kan skapa, spara och dela recept samt få hjälp med att planera sina måltider." +
                                                "Målet med projektet är att göra det enklare att hålla ordning på sina recept och minska tiden som läggs på att planera veckans mat."
                                        },
                                        new()
                                        { Text = "Användare kan skapa egna recept, spara recept från andra användare och anpassa kopior av recept efter egna önskemål utan att ändra originalet." +
                                                "Det finns även möjlighet att skapa grupper där användare kan dela recept och innehåll med exempelvis familj eller vänner."
                                        },
                                        new()
                                        { Text = "Systemet kan använda användarens valda recept för att skapa måltidsplaner och generera inköpslistor baserat på de planerade måltiderna." +
                                                "Projektet har utvecklats med fokus på en tydlig struktur där presentation, affärslogik och datalagring är separerade för att göra systemet" +
                                                "enklare att vidareutveckla och underhålla."
                                        },
                                    },
                                    BackgroundColorCss = "",
                                    SpaceBottom = false,
                                },
                                new ImageCenterSection
                                {
                                    Heading = "Systemarkitektur",
                                    ParagraphsTop =
                                    {
                                        new()
                                        {
                                            Text = "Receptakuten är uppbyggt som en klient-server-applikation där frontend, backend och databas är separerade från varandra. Frontenden är utvecklad " +
                                                "i Vue.js 3 med Composition API och kommunicerar med ett REST API byggt i .NET 8. Systemets data lagras i en PostgreSQL-databas."
                                        },
                                        new()
                                        {
                                            Text = " De olika delarna körs i separata Docker-containrar, vilket gör det enklare att utveckla och uppdatera varje del utan att påverka resten av systemet." +
                                                "Containrarna kommunicerar med varandra via interna nätverk och hanteras som separata tjänster."
                                        },
                                    },
                                    DesktopImages = { new() { ImageUrl = "images/receptakuten-arch-desktop.png" } },
                                    MobileImages = { new() { ImageUrl = "images/receptakuten-arch-mobile.png" } },
                                    ParagraphsBottom =
                                    {
                                        new()
                                        {
                                            Text = "I produktionsmiljön är frontenden tillgänglig via receptakuten.net och backend-API körs på en separat subdomän," +
                                                "api.receptakuten.net. För att kunna testa nya funktioner innan de släpps finns även en separat stagingmiljö med" +
                                                "egna instanser av frontend och backend. Denna miljö är skyddad med Basic Authentication och används för att kontrollera" +
                                                "att förändringar fungerar innan de distribueras till produktion."
                                        },
                                        new()
                                        {
                                            Text = "Utvecklingen sker genom att nya funktioner först skapas i egna feature-brancher och sedan flyttas vidare genom" +
                                                "olika miljöer innan de når produktion. Varje miljö har egna inställningar och miljövariabler för att separera" +
                                                "testmiljön från produktionsmiljön och undvika påverkan på riktig användardata."
                                        },
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                    SpaceBottom = false,
                                },
                            }
                        }
                    };

                    _context.PageContents.Add(receptakutenPageContent);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation(":::::: Receptakuten PageContent Seeded ::::::");
                }
            }
        }
    }
}
