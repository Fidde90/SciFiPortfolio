using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Entities;
using SciFiPortfolio.Enums;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Data.Seeders
{
    public class ReceptakutenBackendPageSeeder
    {
        private readonly SciFiContext _context;
        private readonly ILogger<PageSeeder> _logger;

        public ReceptakutenBackendPageSeeder(SciFiContext context, ILogger<PageSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            var receptakuten = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == "receptakuten");
            var receptaktenBackend = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == "receptakuten-backend");

            if (receptakuten is not null && receptaktenBackend is null)
            {
                receptaktenBackend = new PageEntity
                {
                    Title = "Backend",
                    Slug = "receptakuten-backend",
                    Published = true,
                    PublishedDate = DateTime.UtcNow,
                    ParentPageId = receptakuten.Id
                };

                _context.Pages.Add(receptaktenBackend);
                await _context.SaveChangesAsync();
                _logger.LogInformation(":::::: Receptakuten Child Page - BackEnd Seeded ::::::");
            }

            if (receptakuten is not null && receptaktenBackend is not null)
            {
                var content = await _context.PageContents.FirstOrDefaultAsync(x => x.PageId == receptaktenBackend.Id);

                if (content is null)
                {
                    var receptakutenBackendContent = new PageContentEntity
                    {
                        PageId = receptaktenBackend.Id,
                        Content =
                        {
                            Sections =
                            {
                                new HeroSection
                                {
                                    Heading = "Receptakuten",
                                    TextColor = "sci-fi-glow",
                                },
                                new ImageWithPositionSection
                                {
                                    Heading = "",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Backenden är utvecklad enligt principerna för Clean Architecture i tankarna men följer inte fullt ut men försöker hålla ansvar och beroenden tydliga och " +
                                            "separerade mellan olika lager (Bytte namn från Meal Menu till Receptakuten när projektet närmade sig sitt slut)."
                                        },
                                    },
                                    DesktopImage = { ImageUrl = "images/receptakuten-layers.png" },
                                    ImagePosition = ImagePosition.Bottom,
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-top border-primary",
                                    SpaceBottom = false,
                                    PaddingsCss = "pt-3"
                                },
                                new ImageCenterSection
                                {
                                    ParagraphsTop =
                                    {
                                        new()
                                        {
                                            Text = "API-lagret fungerar som systemets yttersta gränssnitt och ansvarar för routing, autentisering, " +
                                            "middleware, konfiguration av tjänster och mottagning av inkommande förfrågningar. Controllers innehåller " +
                                            "minimalt med logik och fungerar främst som ett lager för validering och transformering av inkommande data."
                                        },

                                        new()
                                        {
                                            Text = "När en förfrågan når API:t valideras den först genom särskilda Request Models med egna regler " +
                                            "för datavalidering. Därefter omvandlas informationen till DTO-objekt som skickas vidare till applikationslagret."
                                        },
                                    },
                                    DesktopImages = { new() { ImageUrl = "images/ControllerDtoMapping.png" } },
                                    ParagraphsBottom =
                                    {
                                        new()
                                        {
                                            Text = "Applikationslagret innehåller all affärslogik och är systemets kärna." +
                                            "Här finns tjänster, DTO:er, entiteter, regler och processer som beskriver hur verksamheten fungerar." +
                                            "Lagret är medvetet byggt utan beroenden till databaser, externa tjänster eller tekniska implementationer, " +
                                            "vilket gör det enkelt att testa och vidareutveckla."
                                        },
                                        new()
                                        {
                                            Text = "Infrastructure-lagret ansvarar för den faktiska kommunikationen med databasen och externa system. " +
                                            "Här finns repositories, Entity Framework-konfigurationer, filhantering och integrationer mot externa tjänster såsom Azure Communication Services. " +
                                            "Genom att använda interfaces mellan lagren kan implementationer bytas ut utan att affärslogiken påverkas."
                                        },
                                    },
                                    BackgroundColorCss = "section-bg",
                                    SpaceBottom = false,
                                    BordersCss = "border-bottom border-primary"
                                },
                                new ImageCenterSection
                                {
                                    SubHeading = "Databas och datamodell",
                                    ParagraphsTop =
                                    {
                                        new()
                                        {
                                            Text = "Databasen är utvecklad enligt en Code First-strategi med Entity Framework Core. Datamodellen definieras " +
                                            "i kod och migreras därefter automatiskt till PostgreSQL genom migrations. "
                                        },
                                        new()
                                        {
                                            Text = "Databasen innehåller ett flertal relationer mellan användare, grupper, recept, matscheman och inköpslistor. " +
                                            "Denna struktur gör det möjligt att hantera både personliga receptsamlingar och gruppbaserade funktioner på ett konsekvent sätt. Ett exempel på relationer nedan:"
                                        },
                                    },
                                    ParagraphsBottom =
                                    {
                                        new()
                                        {
                                            Text = "För att förbättra användarupplevelsen redan från start används seedning av data. " +
                                            "Exempelrecept och grundläggande enheter för ingredienshantering skapas automatiskt när systemet initialiseras."
                                        }
                                    },
                                    DesktopImages = { new() { ImageUrl = "images/recept-diagram.png" } },
                                    BackgroundColorCss = "",
                                    SpaceBottom = false,

                                },
                                new ListSection
                                {
                                    SubHeading = "Säkerhet",
                                    Paragraphs =
                                    [
                                        new()
                                        {
                                            Text = "Säkerhet har varit en central del av projektets design."
                                        },
                                        new()
                                        {
                                            Text = "Autentisering sker genom ASP.NET Identity tillsammans med JWT-baserad autentisering. " +
                                            "Access tokens och refresh tokens lagras i HttpOnly-cookies för att minska risken för exponering via klientskript."
                                        },
                                        new()
                                        {
                                            Text = "Autentisering sker genom ASP.NET Identity tillsammans med JWT-baserad autentisering. " +
                                            "Access tokens och refresh tokens lagras i HttpOnly-cookies för att minska risken för exponering via klientskript."
                                        },
                                        new()
                                        {
                                            Text = "Systemet använder flera säkerhetslager för att skydda API:t, bland annat:"
                                        }
                                    ],
                                    ListItems =
                                    {
                                        new() { Text = "JWT Authentication" },
                                        new() { Text = "Refresh Tokens" },
                                        new() { Text = "Cross-Site Scripting" },
                                        new() { Text = "Rate Limiting" },
                                        new() { Text = "Inputvalidering" },
                                        new() { Text = "Roll- och behörighetskontroller" },
                                    },
                                    SpaceBottom = false,
                                    BackgroundColorCss = "section-bg",
                                    PaddingsCss = "pt-3",
                                    BordersCss = "border-primary border-top"
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Utöver klientbaserade begränsningar verifieras samtliga rättigheter även på serversidan för att " +
                                            "säkerställa att obehöriga användare inte kan kringgå systemets regler genom manipulerade anrop."
                                        },
                                        new()
                                        {
                                            Text = "För att aktivera nya konton krävs e-postverifiering och samma mekanism används vid glömt lösenord."
                                        }
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-bottom border-primary"
                                },
                                new ImageWithPositionSection
                                {
                                    SubHeading = "Realtidsfunktioner",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "För funktioner som kräver omedelbar återkoppling används Server-Sent Events (SSE). " +
                                            "Ett exempel är gruppinbjudningar där mottagaren får uppdateringar direkt från servern utan att " +
                                            "klienten behöver skicka återkommande förfrågningar.",
                                        },
                                        new()
                                        {
                                            Text = "Detta ger snabbare återkoppling samtidigt som belastningen på API:t minskar jämfört med traditionell polling.",
                                        }
                                    },
                                    DesktopImage = { ImageUrl = "images/inbjudning.png" },
                                    ImagePosition = ImagePosition.Bottom,
                                    BackgroundColorCss = "",
                                    PaddingsCss = "",

                                },
                                new ImageCenterSection
                                {
                                    SubHeading = "Bildhantering",
                                    ParagraphsTop =
                                    {
                                        new()
                                        {
                                            Text = "Användaruppladdade bilder bearbetas på serversidan med hjälp av ImageMagick via Magick.NET. Där kontroll av accepterade bildtypr kontrolleras, storlek osv"
                                        },

                                    },
                                    DesktopImages = { new() { ImageUrl = "images/allowed-images.png" } },
                                    ParagraphsBottom =
                                    {
                                        new()
                                        {
                                            Text = "Vid uppladdning genomförs konvertering och storlek/kvallitets optimering för att minska lagringsutrymme och förbättra laddningstider. " +
                                            "Bilderna lagras därefter på servern och kopplas till respektive recept. Nedan kan man se att Clean Code mönster följs med flera små metoder " +
                                            "används för att ge klarare förståelse när man läser koden, detta upprepas över hela projektet."
                                        },
                                    },
                                    BackgroundColorCss = "section-bg",
                                    SpaceBottom = false,
                                    BordersCss = "border-primary border-top",
                                    PaddingsCss = "pt-3"
                                },
                                new ImageSection
                                {
                                    DesktopImages =
                                    {
                                        new() { ImageUrl = "images/save-image.png" }
                                    },
                                    BackgroundColorCss = "section-bg",
                                    PaddingsCss = "pb-3",
                                    BordersCss = "border-primary border-bottom",
                                    SpaceBottom = false,
                                },
                                new ListSection
                                {
                                    SubHeading = "Automatisering och bakgrundsjobb",
                                    Paragraphs =
                                    [
                                        new() { Text = "Systemet använder Quartz.NET för schemalagda bakgrundsjobb som körs oberoende av användaraktivitet." },
                                        new() { Text = "Dessa jobb används bland annat för att:" }
                                    ],
                                    ListItems =
                                    {
                                        new() { Text = "Hantera avslutade matscheman" },
                                        new() { Text = "Rensa utgångna refresh tokens" },
                                        new() { Text = "Identifiera inaktiva användare" },
                                        new() { Text = "Ta bort konton som aldrig aktiverats" },
                                    },
                                    SpaceBottom = true,
                                    PaddingsCss = "",
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new() { Text = "Dessa jobb configureras med hjälp av nycklar och triggers där man sedan anger när de ska köras, Här kan ni se ett exempel på ett av dess jobb: " },
                                    },
                                    PaddingsCss = "",
                                    SpaceBottom = false,
                                },
                                new FlexSection
                                {
                                    ImagesLeft = [new() { ImageUrl = "images/quartz-conf-2.png" }],
                                    ImagesRight = [new() { ImageUrl = "images/quartz-job-ex-3.png" }],
                                    SpaceBottom = false,
                                    PaddingsCss = ""
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Genom att flytta denna typ av arbete till separata processer kan API:t fokusera" +
                                            " på användarrelaterade förfrågningar samtidigt som återkommande underhåll sker automatiskt."
                                        },
                                    },
                                    PaddingsCss = "",
                                },
                                new TextSection
                                {
                                    SubHeading = "Loggning och felhantering",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "För övervakning och felsökning används Serilog med stöd för både konsolloggning och filbaserad loggning."
                                        },
                                        new()
                                        {
                                            Text = " Systemet använder en central Global Exception Handler som fångar upp oväntade fel och returnerar konsekventa felmeddelanden till klienten. " +
                                            "Detta minskar mängden duplicerad felhantering i applikationen och förenklar felsökning."
                                        },
                                        new()
                                        {
                                            Text = "I särskilda situationer där återhämtning är möjlig används riktade try/catch-block för att hantera specifika undantag," +
                                            "exempelvis vid filhantering och borttagning av resurser från servern."
                                        },
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-primary border-all",
                                },
                            }
                        }
                    };

                    _context.PageContents.Add(receptakutenBackendContent);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation(":::::: Receptakuten Backend PageContent Seeded ::::::");
                }
            }
        }
    }
}
