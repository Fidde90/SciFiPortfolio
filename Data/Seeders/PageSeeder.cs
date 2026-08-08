using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Entities;
using SciFiPortfolio.Enums;
using SciFiPortfolio.Interfaces;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Data.Seeders
{
    public class PageSeeder : ISeeder
    {
        private readonly SciFiContext _context;
        private readonly ILogger<PageSeeder> _logger;

        public PageSeeder(SciFiContext context, ILogger<PageSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await SeedPagesAsync();
        }

        private async Task SeedPagesAsync()
        {
            var index = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == "/");
            var sciFiPort = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == "sci-fi-portfolio");
            var vpsHosting = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == "vps-hosting");
            var receptakuten = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == "receptakuten");
            var receptaktenFrontend = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == "receptakuten-frontend");
            var receptaktenBackend = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == "receptakuten-backend");

            #region Create Page Meta

            if (index is null)
            {
                index = new PageEntity
                {
                    Title = "Home",
                    Slug = "/",
                    Published = true,
                    PublishedDate = DateTime.UtcNow
                };

                _context.Pages.Add(index);
                await _context.SaveChangesAsync();

                _logger.LogInformation(":::::: Index Page Seeded ::::::");
            }

            if (vpsHosting is null)
            {
                vpsHosting = new PageEntity
                {
                    Title = "Serverarkitektur",
                    Slug = "vps-hosting",
                    Published = true,
                    PublishedDate = DateTime.UtcNow
                };

                _context.Pages.Add(vpsHosting);
                await _context.SaveChangesAsync();

                _logger.LogInformation(":::::: Vps Page Seeded ::::::");
            }

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
                if (receptaktenFrontend is null)
                {
                    receptaktenFrontend = new PageEntity
                    {
                        Title = "Frontend",
                        Slug = "receptakuten-frontend",
                        Published = true,
                        PublishedDate = DateTime.UtcNow,
                        ParentPageId = receptakuten.Id
                    };

                    _context.Pages.Add(receptaktenFrontend);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation(":::::: Receptakuten Child Page - FrontEnd Seeded ::::::");
                }

                if (receptaktenBackend is null)
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
            }

            if (sciFiPort is null)
            {
                sciFiPort = new PageEntity
                {
                    Title = "Sci-fi Portfolio",
                    Slug = "sci-fi-portfolio",
                    Published = true,
                    PublishedDate = DateTime.UtcNow
                };

                _context.Pages.Add(sciFiPort);
                await _context.SaveChangesAsync();

                _logger.LogInformation(":::::: Sci-fi Page Seeded ::::::");
            }

            #endregion

            #region Tags, Cards, Lookups

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

            if (newTags.Count > 0)
            {
                _context.Tags.AddRange(newTags);
                await _context.SaveChangesAsync();
            }

            var tagLookup = await _context.Tags
                .ToDictionaryAsync(x => x.Name);

            var cards = new List<ProjectCardEntity>()
            {
                new()
                {
                    Title = "SPA Applikation",
                    UniqueName = "receptakuten",
                    ImageUrl = "images/recept-bil-ub.png",
                    ImageAltText = "receptakuten image",
                    Hyperlink = { LinkText = "Testa den här", LinkUrl = "https://receptakuten.net", IconName = "fa-solid fa-arrow-right" },
                    AppLink = { LinkText = "Läs mer om appen", LinkUrl = "/Projects/Receptakuten", PageSlug = "receptakuten", PageId = receptakuten?.Id },
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
                    AppLink = { LinkText = "Läs mer", LinkUrl = "Projects/Hosting", PageSlug = "vps-hosting", PageId = vpsHosting.Id },
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
                    ImageUrl = "images/portimg.jpeg",
                    ImageAltText = "a sci-fi image",
                    AppLink = { LinkText = "Läs mer", LinkUrl = "Projects/sci-fi-portfolio", PageSlug = "sci-fi-portfolio", PageId = sciFiPort.Id },
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

            if (newCards.Count > 0)
            {
                _context.ProjectCards.AddRange(newCards);
                await _context.SaveChangesAsync();
            }

            var cardLookup = await _context.ProjectCards
                .ToDictionaryAsync(x => x.UniqueName);

            #endregion

            #region Add Sections

            if (index is not null)
            {
                var content = await _context.PageContents.FirstOrDefaultAsync(x => x.PageId == index.Id);

                if (content is null)
                {
                    var homePageContent = new PageContentEntity
                    {
                        PageId = index.Id,
                        Content =
                        {
                            Sections =
                            {
                                new HeroSection
                                {
                                    Heading = "Fredrik Bengtsson",
                                    SubHeading = "Fullstack .Net Utvecklare",
                                    SpaceBottom = true
                                },
                                new CarouselSection
                                {
                                    Images =
                                    {
                                        new() { ImageUrl = "images/vue.svg", AltText = "vue icon" },
                                        new() { ImageUrl = "images/javascript.svg", AltText = "javascript icon" },
                                        new() { ImageUrl = "images/net.svg", AltText = ".net icon" },
                                        new() { ImageUrl = "images/csharp.svg", AltText = "c# icon" },
                                        new() { ImageUrl = "images/azure.svg", AltText = "azure icon" },
                                        new() { ImageUrl = "images/api.svg", AltText = "api icon" },
                                        new() { ImageUrl = "images/docker.svg", AltText = "docker icon" },
                                        new() { ImageUrl = "images/nginx.svg", AltText = "nginX icon" },
                                        new() { ImageUrl = "images/postgres-ub.png", AltText = "postgresql icon" },
                                    },
                                },
                                new TextSection()
                                {
                                    Paragraphs =
                                    {
                                        new() { Text = "Hej! Jag är en .NET Fullstack-utvecklare med ett särskilt intresse för backendutveckling. Jag tycker om att bygga API:er, designa databaser och skapa robusta system som är skalbara, säkra och enkla att underhålla." },
                                        new() { Text = "Även om backend är där jag känner mig mest hemma har jag också erfarenhet av frontendutveckling med Vue.js, JavaScript, HTML och CSS. Det gör att jag kan ta ansvar för hela utvecklingsprocessen – från databas och API till användargränssnitt och användarupplevelse." },
                                        new() { Text = "Jag arbetar regelbundet med Docker för containerisering, Nginx för webbserver- och proxyhantering samt Ubuntu Server för drift och deployment. Kombinationen av utveckling och infrastruktur ger mig en helhetsförståelse för hur moderna webbapplikationer byggs, distribueras och underhålls i produktion." },
                                        new() { Text = "Jag drivs av att lösa problem, lära mig nya tekniker och utveckla lösningar som skapar verkligt värde. Oavsett om det handlar om att optimera prestanda i ett API, bygga nya funktioner eller sätta upp en stabil produktionsmiljö strävar jag alltid efter att leverera kod av hög kvalitet." },
                                        new() { Text = "När jag inte utvecklar utforskar jag gärna nya tekniker och verktyg för att fortsätta utvecklas som utvecklare och hålla mig uppdaterad inom branschen." }
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary"
                                },
                                new CardSection()
                                {
                                    CardIds =
                                    {
                                        cardLookup["receptakuten"].Id,
                                        cardLookup["sci-fi-portfolio"].Id,
                                        cardLookup["vps-hosting"].Id,
                                    }
                                }
                            }
                        }
                    };

                    _context.PageContents.Add(homePageContent);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation(":::::: Index PageContent Seeded ::::::");
                }
            }

            if (vpsHosting is not null)
            {
                var content = await _context.PageContents.FirstOrDefaultAsync(x => x.PageId == vpsHosting.Id);

                if (content is null)
                {
                    var vpsHostingPageContent = new PageContentEntity
                    {
                        PageId = vpsHosting.Id,
                        Content =
                        {
                            Sections =
                            {
                                new HeroSection
                                {
                                    Image = new() { ImageUrl = "images/server.png" },
                                    SpaceBottom = false
                                },
                                new ListSection
                                {
                                    SubHeading = "Teknikstack",
                                    Paragraphs = new()
                                    {
                                        new() 
                                        { 
                                            Text = "Jag designade och driftsatte en containerbaserad servermiljö på en VPS-hostad Ubuntu Server. " +
                                            "Infrastrukturens fokus ligger på isolering, säkerhet och enkel skalning av applikationer." 
                                        }
                                    },
                                    ListItems = {
                                        new() { Text = "Ubuntu Server (minimal installation)", IconName = "" },
                                        new() { Text = "Docker & Docker Compose", IconName = "" },
                                        new() { Text = "Nginx Reverse Proxy", IconName = "" },
                                        new() { Text = "PostgreSQL", IconName = "" },
                                        new() { Text = "Certbot (Let´s Encrypt)", IconName = "" },
                                        new() { Text = "VPS-hosting via Hostinger)", IconName = "" },
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                    SpaceBottom = false,                                    
                                },
                                new ImageWithPositionSection
                                {
                                    Heading = "Infrastrukturdesign",
                                    Paragraphs =
                                    {
                                        new() 
                                        { 
                                            Text = "Servern är uppbyggd enligt en lagerindelad arkitektur där Nginx körs direkt på " +
                                            "värdmaskinen och fungerar som central ingresspunkt för all inkommande trafik." 
                                        },
                                    },
                                    DesktopImage = { ImageUrl = "images/infra-desktop.png" },
                                    MobileImage = { ImageUrl = "images/infra-mobile.png" },
                                    ImagePosition = ImagePosition.Bottom,
                                    SpaceBottom = false,
                                },
                                new TextSection()
                                {
                                    Heading = "Nätverksarkitektur",
                                    Paragraphs = 
                                    {
                                        new() 
                                        { 
                                            Text = "Servermiljön är uppbyggd kring en central ingressmodell där Nginx fungerar som reverse proxy " +
                                            "för samtliga applikationer. Genom att hantera SSL, domänrouting och trafikstyrning på värdservern skapas en tydlig " +
                                            "separation mellan publika och interna resurser." 
                                        },

                                        new() 
                                        { 
                                            Text = "Applikationerna körs i isolerade Docker-nätverk och exponeras inte direkt mot internet. " +
                                            "Istället routas inkommande trafik till rätt container baserat på domän och konfiguration. Lösningen ger en " +
                                            "flexibel grund för att hosta flera tjänster på samma server samtidigt som säkerhet, underhållbarhet och skalbarhet bibehålls." 
                                        }
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                    SpaceBottom = false,
                                },
                                new TextSection()
                                {
                                    Heading = "Säkerhet",
                                    Paragraphs = {
                                        new() 
                                        { 
                                            Text = "Säkerhet har varit en central del av serverarkitekturen redan från början. " +
                                            "All extern trafik krypteras med SSL-certifikat från Let's Encrypt som automatiskt förnyas med hjälp av Certbot. " +
                                            "Nginx fungerar som den enda publika ingresspunkten och ansvarar för att dirigera trafiken vidare till rätt tjänst." 
                                        },

                                        new() 
                                        { 
                                            Text = "Databaser exponeras aldrig direkt mot internet utan är endast åtkomliga från de " +
                                            "containrar som behöver kommunicera med dem. Genom privata Docker-nätverk och separerade applikationsmiljöer " +
                                            "skapas ytterligare ett skyddslager som begränsar åtkomsten mellan olika system." 
                                        }
                                    },
                                   SpaceBottom = false,
                                },
                                new DockerSection()
                                {
                                    Heading = "Containerisering",
                                    LeftParagraph = 
                                    { 
                                        Text = "Säkerhet har varit en central del av serverarkitekturen redan från början. All extern trafik krypteras med " +
                                        "SSL-certifikat från Let's Encrypt som automatiskt förnyas med hjälp av Certbot. Nginx fungerar som den enda publika " +
                                        "ingresspunkten och ansvarar för att dirigera trafiken vidare till rätt tjänst. Databaser exponeras aldrig " +
                                            "direkt mot internet utan är endast åtkomliga från de containrar som behöver kommunicera med dem. Genom privata " +
                                            "Docker-nätverk och separerade applikationsmiljöer skapas ytterligare ett skyddslager som begränsar åtkomsten mellan olika system." 
                                    },

                                    RightParagraph = 
                                    { 
                                        Text = "För att skapa en stabil och lättadministrerad driftmiljö är plattformen uppbyggd kring Docker." +
                                        "Varje applikation körs i sin egen isolerade container tillsammans med en dedikerad PostgreSQL-databas. Genom att separera tjänsterna " +
                                        "från varandra minimeras risken att ett problem i en applikation påverkar övriga system på servern." 
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                    SpaceBottom = false,
                                },
                                new TextSection()
                                {
                                    Heading = "Skalbarhet",
                                    Paragraphs = 
                                    {
                                        new() 
                                        { Text = "Infrastrukturen är designad för att enkelt kunna växa när nya projekt eller tjänster " +
                                            "behöver läggas till. Genom Docker Compose definieras hela miljön som kod, vilket innebär att samma " +
                                            "konfiguration kan återskapas på nya servrar med minimala manuella insatser." 
                                        },

                                        new() 
                                        { 
                                            Text = "När en ny applikation ska driftsättas skapas en separat containeruppsättning med egna miljövariabler, " +
                                            "databasresurser och nätverksinställningar. Nginx kan därefter konfigureras för att routa trafik till den nya tjänsten " +
                                            "utan att påverka redan existerande applikationer." 
                                        },

                                        new()
                                        { 
                                            Text = "Detta arbetssätt ger konsekventa miljöer mellan utveckling, test och produktion samtidigt " +
                                            "som det förenklar både felsökning och framtida expansion." 
                                        },
                                    },
                                    SpaceBottom = false,
                                },
                                new TextSection()
                                {
                                    Heading = "Resultat",
                                    Paragraphs = {
                                        new() 
                                        { 
                                            Text = "Den färdiga plattformen ger en robust grund för att hosta flera applikationer på samma " +
                                            "server utan att kompromissa med säkerhet eller underhållbarhet. Genom containerisering, centraliserad trafikhantering " +
                                            "och automatiserad certifikathantering har infrastrukturen blivit både enkel att administrera och enkel att vidareutveckla." 
                                        },

                                        new() 
                                        { 
                                            Text = "Arkitekturen möjliggör snabb driftsättning av nya projekt samtidigt som befintliga tjänster kan uppdateras " +
                                            "oberoende av varandra. Resultatet är en flexibel och skalbar plattform som kan växa i takt med nya behov utan att kräva större " +
                                            "förändringar i den underliggande infrastrukturen." 
                                        },
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                },
                            }
                        }
                    };

                    _context.PageContents.Add(vpsHostingPageContent);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation(":::::: Vps Hosting PageContent Seeded ::::::");
                }
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

            if (receptakuten is not null && receptaktenFrontend is not null)
            {
                var content = await _context.PageContents.FirstOrDefaultAsync(x => x.PageId == receptaktenFrontend.Id);

                if (content is null)
                {
                    var receptakutenFrontendContent = new PageContentEntity
                    {
                        PageId = receptaktenFrontend.Id,
                        Content =
                        {
                            Sections =
                            {
                                new HeroSection
                                {
                                    Heading = "Receptakuten",
                                    TextColor = "sci-fi-glow",
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Frontenden är utvecklad i Vue.js 3 med Composition API och bygger på en komponentbaserad arkitektur där återanvändbarhet, " +
                                            "tydlig ansvarsfördelning och underhållbar kod har varit centrala designprinciper genom hela projektet."
                                        },
                                        new()
                                        {
                                            Text = "Applikationen består av ett stort antal specialiserade komponenter för bland annat recept, ingredienshantering, " +
                                            "gruppadministration, matscheman, profiler, formulär, dialogrutor och navigering. Genom att dela upp funktionaliteten i mindre " +
                                            "komponenter har samma komponenter kunnat återanvändas på flera ställen i applikationen, vilket minskar kodduplicering och förenklar " +
                                            "vidareutveckling."
                                        },
                                        new()
                                        {
                                            Text = "Affärslogiken hålls i stor utsträckning separerad från presentationslagret genom egna composables och serviceklasser. " +
                                            "Kommunikation mellan komponenter sker främst via props och emitters, medan globala händelser hanteras med en central EventBus. " +
                                            "Detta används exempelvis för att uppdatera olika delar av användargränssnittet när data förändras eller när notifieringar behöver visas " +
                                            "utan att komponenterna känner till varandra."
                                        },
                                        new()
                                        {
                                            Text = "Projektet är uppbyggt med en tydlig mappstruktur där komponenter, vyer, composables, API-tjänster och routing hålls åtskilda. " +
                                            "Detta gör projektet mer skalbart och enklare att underhålla " +
                                            "när nya funktioner läggs till."
                                        },
                                    },
                                    SpaceBottom = false,
                                    BordersCss = "border-top border-primary",
                                    BackgroundColorCss = "section-bg",
                                },
                                new TextSection
                                {
                                    Heading = "Routing och autentisering",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Navigeringen hanteras med Vue Router där olika layouts används beroende på användarens autentiseringsstatus. " +
                                            "Publika sidor, såsom inloggning, registrering och återställning av lösenord, använder en separat layout från den del " +
                                            "av applikationen som kräver inloggning. På så sätt hålls användarupplevelsen enkel innan användaren loggat in samtidigt " +
                                            "som den inloggade delen av applikationen kan erbjuda sidomenyer, navigering och användarspecifika funktioner."
                                        }
                                    },
                                    SpaceBottom = false,
                                },
                                new FlexSection
                                {
                                    ParagraphsLeft = [new() { Text = "Auth layout" }],
                                    ImagesLeft = [new() { ImageUrl = "images/auth-layout.png" }],
                                    ParagraphsRight = [new() { Text = "Account layout" }],
                                    ImagesRight = [new() { ImageUrl = "images/account-layout.png" }],
                                    PaddingsCss = "pb-3",
                                    SpaceBottom = false,
                                },
                                new ImageWithPositionSection
                                {
                                    Heading = "Kommunikation med backend",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "All kommunikation med backend sker via ett REST-API genom ett separat servicelager. " +
                                            "Genom att samla API-anrop på ett ställe blir komponenterna enklare och mer fokuserade på presentation."
                                        },
                                        new()
                                        {
                                            Text = "För att förbättra prestandan lagras viss information lokalt i klienten för att minska mängden " +
                                            "upprepade API-anrop. Detta ger kortare laddningstider och gör att användaren upplever applikationen som " +
                                            "snabbare och mer responsiv."
                                        }
                                    },
                                    ImagePosition = ImagePosition.Bottom,
                                    DesktopImage = { ImageUrl = "images/api-call-ex.png" },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-top border-primary",
                                    SpaceBottom = false,
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Flera funktioner bygger på att affärslogiken ligger i backend istället för i klienten. Ett exempel är " +
                                            "genereringen av matscheman där backend ansvarar för slumpalgoritmen. Algoritmen ser till att samma recept inte " +
                                            "väljs flera gånger under samma vecka och att variationen ökar i takt med att fler recept finns tillgängliga. " +
                                            "Genom att placera denna logik i backend säkerställs att alla klienter får samma resultat och att reglerna inte " +
                                            "kan kringgås."
                                        },
                                        new() { Text = "Exempel på logiken i backend:" },
                                    },
                                    PaddingsCss = "",
                                    BackgroundColorCss = "section-bg",
                                    SpaceBottom = false,
                                },
                                new ImageSection
                                {
                                    DesktopImages =
                                    {
                                        new() { ImageUrl = "images/part-off-dinner-schedule-creation.png" },
                                        new() { ImageUrl = "images/get-random-recipes.png" },
                                        new() { ImageUrl = "images/add-rotation-points.png" }
                                    },
                                    PaddingsCss = "pb-3",
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-bottom border-primary",
                                    SpaceBottom = false,
                                },
                                new ImageCenterSection
                                {
                                    Heading = "Realtidskommunikation",
                                    ParagraphsTop =
                                    {
                                        new()
                                        { Text = "Applikationen använder Server-Sent Events (SSE) för att skapa realtidsfunktionalitet. När användaren är inloggad " +
                                            "lyssnar klienten kontinuerligt efter händelser från servern utan att behöva göra återkommande polling-anrop."
                                        }
                                    },
                                    DesktopImages =
                                    {
                                        new() { ImageUrl = "images/sse-connect.png" },
                                        new() { ImageUrl = "images/sse-disconnect.png" }
                                    },
                                    ParagraphsBottom =
                                    {
                                        new()
                                        {
                                            Text = "Denna lösning används bland annat för gruppinbjudningar. När en användare blir inbjuden till en " +
                                            "grupp visas informationen direkt i gränssnittet utan att sidan behöver laddas om. " +
                                            "Detta bidrar till en mer responsiv och modern användarupplevelse."
                                        }
                                    },
                                    SpaceBottom = false,
                                },
                                new ListSection
                                {
                                    Heading = "Profilsida",
                                    Paragraphs = [new() { Text = "Varje användare har en profilsida där kontot kan administreras. Här kan användaren bland annat:" },],
                                    ListItems =
                                    {
                                        new() { Text = "ändra personlig information" },
                                        new() { Text = "byta lösenord" },
                                        new() { Text = "se hur många grupper man är medlem i" },
                                        new() { Text = "se hur många recept man har delat" },
                                        new() { Text = "anpassa applikationens tema" },
                                        new() { Text = "hantera övriga kontoinställningar" },
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = "pt-3",
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-top border-primary",
                                },
                                new TextSection
                                {
                                    Paragraphs = 
                                    { 
                                        new() 
                                        { 
                                            Text = "Genom att samla all kontohantering på ett ställe blir det enkelt för användaren " +
                                            "att administrera sitt konto." 
                                        }, 
                                    },
                                    PaddingsCss = "pb-3",
                                    SpaceBottom = false,
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-bottom border-primary",
                                },
                                new TextSection
                                {
                                    Heading = "Användarupplevelse",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Stor vikt har lagts vid att skapa ett snyggt och responsivt gränssnitt. Datumhantering " +
                                            "sker med komponenten VueDatePicker, vilket ger ett betydligt mer användarvänligt " +
                                            "sätt att välja datum vid exempelvis planering av matscheman."
                                        },
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = "pt-3",
                                },
                                new ImageSection
                                {
                                    DesktopImages = 
                                    [
                                        new() { ImageUrl = "images/datepicker.png" },
                                        new() { ImageUrl = "images/calendar.png" },
                                    ],
                                    SpaceBottom = false,
                                    PaddingsCss = "",
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Applikationen innehåller även automatisk konvertering av HEIC-bilder via Heic2any innan uppladdning. " +
                                            "Browser-image-compression används för att se till att bilden inte är förstor innan den skickas till backend, " +
                                            "men även backend kollar formatet, komprimerar och optimiserar bilden ytterligare så att det inte går att kringå i frontend." +
                                            "Detta gör att användare, framför allt från iPhone och andra Apple-enheter, " +
                                            "kan ladda upp receptbilder utan att själva behöva konvertera filformatet (för att det ska kunna visas i alla webbläsare)."
                                        },
                                        new() { Text = "För att göra applikationen mer personlig kan användaren även växla mellan olika teman." }
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = "",
                                },
                                new ImageSection
                                {
                                    DesktopImages =
                                    [
                                        new() { ImageUrl = "images/themes.png" },
                                    ],
                                    SpaceBottom = false,
                                    PaddingsCss = "",
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Gränssnittet använder dessutom laddningsindikatorer vid längre operationer samt tydliga " +
                                            "återkopplingar när en åtgärd lyckas eller misslyckas. Detta gör att användaren alltid får tydlig " +
                                            "feedback på vad som händer."
                                        },
                                        new() { Text = "Applikationen är även responsivt uppbyggd och fungerar på både dator, surfplatta och mobiltelefon." }
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = "",
                                },
                                new ImageSection
                                {
                                    DesktopImages =
                                    [
                                        new() { ImageUrl = "images/receptakuten-mobile-desktop.png" },
                                    ],
                                    SpaceBottom = false,
                                    PaddingsCss = "pb-3",
                                },
                                new ImageCenterSection
                                {
                                    Heading = "Behörigheter och säkerhet",
                                    ParagraphsTop =
                                    {
                                        new()
                                        {
                                            Text = "Behörighetskontroller utförs alltid i backend. Frontenden använder den information som returneras " +
                                            "för att visa eller dölja funktioner beroende på användarens rättigheter, men alla kritiska kontroller verifieras " +
                                            "på serversidan."
                                        },
                                        new()
                                        {
                                            Text = "Exempelvis kan endast gruppens ägare bjuda in nya medlemmar eller ta bort befintliga medlemmar. Även om " +
                                            "någon skulle försöka manipulera klienten kommer backend att neka otillåtna åtgärder."
                                        }
                                    },
                                    DesktopImages = { new() { ImageUrl = "images/groupowner.png" }, },
                                    ParagraphsBottom =
                                    {
                                        new()
                                        {
                                            Text = "Åtkomst till receptbilder kontrolleras också av backend. Bilder kan endast visas av receptets ägare eller " +
                                            "av användare som är medlem i en grupp där receptet har delats."
                                        }
                                    },
                                    SpaceBottom = false,
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary"
                                },
                                new TextSection
                                {
                                    Heading = "Delning av recept",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "När ett recept delas inom en grupp skapas en separat entitet i backend istället för att flera användare " +
                                            "arbetar mot samma databaspost. " +
                                            "Denna lösning gör det möjligt för användare att skapa egna kopior av delade recept och fortsätta utveckla " +
                                            "dem utan att originalreceptet påverkas. " +
                                            "På så sätt kan recept delas mellan användare samtidigt som varje användare behåller möjligheten " +
                                            "att göra egna ändringar."
                                        },
                                    },
                                    SpaceBottom = false,
                                },
                                new TextSection
                                {
                                    Heading = "Kodkvalitet och skalbarhet",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Projektet har utvecklats med fokus på långsiktig underhållbarhet. Genom den komponentbaserade " +
                                            "arkitekturen, återanvändbara composables, ett separat API-lager och en tydlig projektstruktur är det " +
                                            "enkelt att lägga till nya funktioner utan att påverka befintlig kod."
                                        },
                                        new()
                                        {
                                            Text = "Sidor laddas dynamiskt via Vue Router (lazy loading) för att minska den initiala laddningstiden, " +
                                            "och återanvändbara komponenter används genom hela applikationen för att skapa ett enhetligt gränssnitt och " +
                                            "minska mängden duplicerad kod."
                                        },
                                        new()
                                        {
                                            Text = "Denna arkitektur har gjort det möjligt att successivt bygga ut projektet med nya funktioner utan att " +
                                            "behöva göra större förändringar i den befintliga kodbasen."
                                        },
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary"
                                },
                            }
                        }
                    };

                    _context.PageContents.Add(receptakutenFrontendContent);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation(":::::: Receptakuten Frontend PageContent Seeded ::::::");
                }
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

            if (sciFiPort is not null)
            {
                var content = await _context.PageContents.FirstOrDefaultAsync(x => x.PageId == sciFiPort.Id);

                if (content is null)
                {
                    var sciFiPageContent = new PageContentEntity
                    {
                        PageId = sciFiPort.Id,
                        Content =
                        {
                            Sections =
                            {
                                new HeroSection
                                {
                                    Heading = "Portfolio",
                                    SubHeading = "Razor pages / Cms",
                                    TextColor = "sci-fi-glow",
                                },
                                new TextSection
                                {
                                    Heading = "Portfolio och CMS-plattform",
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Det här projektet började egentligen som något betydligt enklare. " +
                                            "Tanken från början var att bygga en traditionell webbplats med ASP.NET Core Razor Pages " +
                                            "där varje sida hade sin egen vy med innehåll direkt skrivet i koden."
                                        },
                                        new()
                                        {
                                            Text = "Efter hand började jag dock fundera på hur innehållet skulle hanteras när webbplatsen växte. " +
                                            "Att hårdkoda all text och allt innehåll direkt i Razor-vyerna fungerade, " +
                                            "men kändes ganska begränsande och inte särskilt flexibelt. Nästa steg blev därför att flytta " +
                                            "innehållet till tjänster och modeller för att separera innehåll från presentation."
                                        },
                                        new()
                                        {
                                            Text = "Ju längre projektet utvecklades desto tydligare blev det att innehållet egentligen borde lagras i " +
                                            "en databas. Samtidigt kändes det inte optimalt att skapa separata tabeller och kolumner för varje " +
                                            "tänkbar sektion som kunde förekomma på en sida. Om varje ny " +
                                            "innehållstyp krävde databasändringar, migrationer och nya modeller skulle systemet snabbt bli svårt att underhålla."
                                        },
                                        new()
                                        {
                                            Text = "Det var ungefär där tankarna började röra sig mot ett CMS-liknande upplägg. Från att ha varit en enkel " +
                                            "portfoliosida började projektet successivt utvecklas till en mer generell plattform där innehållet " +
                                            "styrs av data istället för hårdkodade vyer."
                                        },
                                        new()
                                        {
                                            Text = "Även om ett befintligt CMS hade kunnat lösa många av dessa problem är syftet med projektet inte bara att få fram en färdig webbplats. " +
                                            "En stor del av motivationen har varit att lära sig nya tekniker, utforska olika arkitekturmönster och bygga något från " +
                                            "grunden för att förstå hur systemen fungerar bakom kulisserna. Många delar av lösningen är därför mer " +
                                            "avancerade än vad som egentligen krävs för en portfolio, men just den typen av tekniska utmaningar är också det som gör projektet roligt att arbeta med."
                                        }
                                    },
                                    SpaceBottom = false,
                                },
                                new TextSection
                                {
                                    Heading = "Övergripande arkitektur",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Webbplatsen är byggd med ASP.NET Core Razor Pages och PostgreSQL som databas."
                                        },
                                        new()
                                        {
                                            Text = "Istället för att varje sida har en egen hårdkodad struktur består systemet till stor " +
                                            "del av sidor som byggs upp dynamiskt från innehållsblock som lagras i databasen. Samtidigt har det " +
                                            "varit viktigt att inte låsa hela webbplatsen till CMS-strukturen. Eftersom Razor Pages i grunden " +
                                            "är sidcentrerat finns det fortfarande möjlighet att bygga vissa sidor mer traditionellt när det passar bättre."
                                        },
                                        new()
                                        {
                                            Text = "Exempel på detta är startsidan, sidan om mig eller andra vyer där innehållet är mer unikt " +
                                            "och inte nödvändigtvis behöver representeras av återanvändbara innehållsblock. I sådana fall kan " +
                                            "sidan byggas upp direkt i vyn eller med hjälp av befintliga sektionsmodeller utan att använda den generella renderingskomponenten."
                                        },
                                        new()
                                        {
                                            Text = "Tanken är att CMS-funktionaliteten ska vara ett verktyg och inte ett krav. Om en " +
                                            "specifik sida behöver specialanpassad logik eller innehåll som bara kommer att användas på " +
                                            "ett enda ställe finns det inget som hindrar att den implementeras mer direkt. Detta ger en " +
                                            "balans mellan flexibiliteten i det dynamiska innehållssystemet och enkelheten i traditionella " +
                                            "Razor Pages när det är den mest lämpliga lösningen."
                                        },
                                        new()
                                        {
                                            Text = "Målet har hela tiden varit att skapa en flexibel struktur där nya sidor och sektioner " +
                                            "kan byggas på olika sätt beroende på behov, utan att den grundläggande arkitekturen behöver förändras."
                                        }
                                    },
                                    SpaceBottom = false,
                                },
                                new ListSection
                                {
                                    Heading = "Databasdesign",
                                    Paragraphs =
                                    [
                                        new() { Text = "Databasen bygger på ett antal centrala entiteter som beskriver webbplatsens innehåll." },
                                        new()
                                        {
                                            Text = "Varje sida representeras av en egen Page-entitet som innehåller grundläggande information om sidan, " +
                                            "exempelvis titel, slug och metadata. Till varje sida finns sedan ett eller flera PageContent-objekt kopplade " +
                                            "som beskriver det faktiska innehållet som ska visas."
                                        },
                                        new() { Text = "För innehåll som återanvänds på flera ställen används separata entiteter i databasen. Exempel på detta är:" },
                                    ],
                                    ListItems =
                                    {
                                        new() { Text = "Projektkort" },
                                        new() { Text = "Taggar" },
                                        new() { Text = "Övriga återanvändbara objekt som man kan tänkas vilja återanvända senare" }
                                    },
                                    BackgroundColorCss = "section-bg",
                                    PaddingsCss = "pt-3",
                                    BordersCss = "border-top border-primary",
                                    SpaceBottom = false,
                                },
                                new ImageWithPositionSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "På så sätt undviks duplicering av data samtidigt som samma information kan användas på flera sidor."
                                        },
                                        new()
                                        {
                                            Text = "Eftersom systemet bygger på ett blockbaserat innehållssystem där innehållssektioner lagras som JSON " +
                                            "istället för i separata databastabeller blir databasstrukturen relativt enkel och kompakt. För den nuvarande " +
                                            "omfattningen räcker denna modell väl, samtidigt som den ger stor flexibilitet när nya innehållstyper behöver introduceras. " +
                                            "I takt med att CMS:et vidareutvecklas kommer sannolikt fler entiteter och relationer att tillkomma, men den grundläggande " +
                                            "strukturen kan fortfarande hållas förhållandevis enkel. En sida kan exempelvis innehålla flera innehållssektioner samtidigt som " +
                                            "samma taggar, projektkort eller andra återanvändbara objekt kan användas på flera platser i systemet."
                                        }
                                    },
                                    DesktopImage = new(){ ImageUrl = "images/portfolio-db-desktop.png" },
                                    MobileImage = new() { ImageUrl = "images/portfolio-bd-mobile.png" },
                                    ImagePosition = ImagePosition.Bottom,
                                    PaddingsCss = "pb-3",
                                    BordersCss = "border-bottom border-primary",
                                    BackgroundColorCss = "section-bg",
                                    SpaceBottom = false,
                                },
                                new ImageCenterSection
                                {
                                    Heading = "Dynamiskt innehållssystem",
                                    ParagraphsTop =
                                    {
                                        new(){ Text = "Kärnan i systemet är det blockbaserade innehållssystemet." },
                                        new()
                                        {
                                            Text = "Istället för att lagra varje innehållstyp i egna databaskolumner lagras " +
                                            "innehållssektioner som JSON i databasen. När innehållet sparas serialiseras " +
                                            "objekten automatiskt och när de hämtas deserialiseras de tillbaka till sina respektive modeller."
                                        },

                                    },
                                    DesktopImages =
                                    {
                                        new() { ImageUrl = "images/json-content.png" },
                                        new() { ImageUrl = "images/serialize-deserialize.png" },
                                    },

                                    ParagraphsBottom =
                                    {
                                        new(){ Text = "Detta gör att nya innehållstyper kan introduceras utan att databasen behöver ändras." }
                                    },
                                    PaddingsCss = "pt-3",
                                    SpaceBottom = false,
                                },
                                new ListSection
                                {
                                    Paragraphs =
                                    [
                                        new() { Text = "Detta gör att nya innehållstyper kan introduceras utan att databasen behöver ändras." },
                                        new() { Text = "Exempel på innehållsblock som finns idag är:" },
                                    ],
                                    ListItems =
                                    {
                                        new() { Text = "TextSection" },
                                        new() { Text = "ImageSection" },
                                        new() { Text = "ImageWithPositionSection" },
                                        new() { Text = "ListSection" },
                                        new() { Text = "HeroSection" },
                                        new() { Text = "CardSection" }
                                    },
                                    PaddingsCss = "",
                                    SpaceBottom = false,
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Fördelen med denna lösning är att en sida kan byggas upp av valfria kombinationer av sektioner " +
                                            "samtidigt som databasen förblir relativt enkel."
                                        },
                                    },
                                    PaddingsCss = "pb-3",
                                    SpaceBottom = false,
                                },
                                new ImageWithPositionSection
                                {
                                    SubHeading = "Här är ett exempel på hur ett sektions block är uppbyggt",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "En modell byggs upp för sektionen med dess olika egenskaper och ärver fler från en basklass."
                                        },
                                    },
                                    DesktopImage = new(){ ImageUrl = "images/image-position-model.png" },
                                    ImagePosition = ImagePosition.Bottom,
                                    PaddingsCss = "pt-3 pb-3",
                                    BordersCss = "border-top border-primary",
                                    BackgroundColorCss = "section-bg",
                                    SpaceBottom = false,
                                },
                                new ImageWithPositionSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "ContentSection fungerar som basklass för samtliga innehållssektioner. " +
                                            "Klassen innehåller gemensamma egenskaper samtidigt som JsonDerivedType-attributen används för att " +
                                            "konfigurera polymorf serialisering och deserialisering. Detta gör att systemet automatiskt " +
                                            "kan avgöra vilken sektionstyp som ska instansieras när innehåll hämtas från databasen."
                                        }
                                    },
                                    DesktopImage = new(){ ImageUrl = "images/content-section.png" },
                                    ImagePosition = ImagePosition.Bottom,
                                    PaddingsCss = "",
                                    BackgroundColorCss = "section-bg",
                                    SpaceBottom = false,
                                },
                                new FlexSection
                                {
                                    ParagraphsLeft =
                                    [
                                        new()
                                        {
                                            Text = "I detta fallet är partialen uppdelad i olika delar för läsbarhetens skull. Här kontrolleras det vilken position som är vald för bilden."
                                        },
                                    ],
                                    ImagesLeft =
                                    [
                                        new() { ImageUrl = "images/image-position-partial.png" }
                                    ],
                                    ParagraphsRight =
                                    [
                                        new()
                                        {
                                            Text = "Sedan renderas bilden på rätt plats i vyn, i detta fallet så är det en topp/bottnen placering (kunde ha gjort detta enklare med bara css-klasser...men detta får vara just nu)."
                                        }
                                    ],
                                    ImagesRight =
                                    [
                                        new() { ImageUrl = "images/image-y-partial.png" }
                                    ],
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-bottom border-primary",
                                    SpaceBottom = false,
                                },
                                new ImageWithPositionSection
                                {
                                    Heading = "Rendering av sidor",
                                    Paragraphs =
                                    {
                                        new() { Text = "Alla projektsidor använder samma Razor Page för rendering." },
                                        new() { Text = "Istället för att skapa en separat sida för varje projekt används en gemensam renderingsmodell där rätt innehåll hämtas baserat på sidans slug." },
                                    },
                                    DesktopImage = { ImageUrl = "images/project-renderer.png" },
                                    ImagePosition = ImagePosition.Bottom,
                                    SpaceBottom = false,
                                    PaddingsCss = "pt-3"
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new() { Text = "Själva renderingen sker genom en specialiserad View " +
                                        "Component som fungerar som en central renderingsmotor för systemet. " +
                                        "Komponenten går igenom innehållssektionerna och avgör vilken partialvy som ska användas för respektive sektionstyp." },
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = ""
                                },
                                new ImageSection
                                {
                                    DesktopImages =
                                    {
                                        new() { ImageUrl = "images/view-component.png" },
                                        new() { ImageUrl = "images/view-component-view.png" }
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = ""
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "För att göra systemet mer robust används även ICompositeViewEngine för att kontrollera att " +
                                            "en partialvy faktiskt existerar innan den renderas. På så sätt upptäcks eventuella fel tidigt samtidigt som det " +
                                            "blir enklare att utveckla nya innehållsblock."
                                        },
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = ""
                                },
                                new ImageSection
                                {
                                    DesktopImages =
                                    {
                                        new() { ImageUrl = "images/renderer-service.png" },
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = ""
                                },
                                new ListSection
                                {
                                    Paragraphs =
                                    [
                                        new()
                                        {
                                            Text = "Nu blir det enkelt att skapa nya " +
                                            "sektionstyper utan att behöva ändra på renderingslogiken, det enda man behöver göra är att skapa:"
                                        },
                                    ],
                                    ListItems =
                                    {
                                        new() { Text = "En modell" },
                                        new() { Text = "En partialvy" },
                                    },
                                    PaddingsCss = "pb-3",
                                    SpaceBottom = false,
                                },
                                new TextSection
                                {
                                    Heading = "Servicelager",
                                    Paragraphs =
                                    {
                                        new() { Text = "För närvarande används främst en central PageService som ansvarar för att hämta sidor, innehåll och relaterade objekt." },
                                        new()
                                        {
                                            Text = "Under utvecklingen har detta varit en enkel lösning som gjort det möjligt att snabbt bygga vidare på funktionaliteten. " +
                                            "Tanken är dock att servicelagret framöver ska delas upp i mer specialiserade tjänster med tydligare ansvarsområden men just nu " +
                                            "när systemet inte är så stort så fungerar det utmärkt."
                                        },
                                        new()
                                        {
                                            Text = "Samtidigt finns planer på att införa en gemensam basklass för repositories för att minska duplicerad kod och skapa " +
                                            "en mer enhetlig dataåtkomst."
                                        },
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                    SpaceBottom = false,
                                },
                                new ListSection
                                {
                                    Heading = "Temasystem",
                                    Paragraphs =
                                    [
                                        new() { Text = "Webbplatsen innehåller ett temasystem där användaren kan välja mellan flera olika visuella teman." },
                                        new() { Text = "Bland de teman som finns idag återfinns bland annat:" },
                                    ],
                                    ListItems =
                                    {
                                        new() { Text = "Sci-Fi" },
                                        new() { Text = "Cyberpunk" },
                                        new() { Text = "Matrix" },
                                        new() { Text = "Deep Space" },
                                        new() { Text = "Plasma Core" },
                                        new() { Text = "Alien Tech" },
                                        new() { Text = "Starfleet" },
                                    },
                                    PaddingsCss = "pt-3",
                                    SpaceBottom = false,
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Temana bygger på CSS-variabler och ett data-theme-attribut på rot-elementet. " +
                                            "Genom att byta tema ändras färger, gradienter, skuggor, bakgrunder och visuella effekter " +
                                            "utan att de enskilda komponenterna behöver känna till vilket tema som används.."
                                        },
                                        new() { Text = "Det gör det enkelt att introducera nya teman samtidigt som gränssnittet förblir konsekvent." },
                                    },

                                    PaddingsCss = "pb-3",
                                    SpaceBottom = false,
                                },
                                new ListSection
                                {
                                    Heading = "Responsiv design",
                                    Paragraphs =
                                    [
                                        new() { Text = "Hela webbplatsen är byggd med responsiv design som utgångspunkt." },
                                        new() { Text = "Layout och innehåll anpassar sig automatiskt efter skärmstorleken vilket gör att webbplatsen fungerar på:" },
                                    ],
                                    ListItems =
                                    {
                                        new() { Text = "Dator" },
                                        new() { Text = "Surfplatta" },
                                        new() { Text = "Mobiltelefon" },
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = "pt-3",
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-top border-primary"
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Målet har varit att samma innehåll ska fungera oavsett enhet utan att separata mobilversioner behöver utvecklas."
                                        },
                                    },
                                    PaddingsCss = "pb-3",
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-bottom border-primary",
                                    SpaceBottom = false,
                                },
                                new ListSection
                                {
                                    Heading = "Prestanda och caching",
                                    Paragraphs =
                                    [
                                        new() { Text = "Eftersom att innehållet är identiskt för alla besökare lämpar sig systemet väl för caching." },
                                        new() { Text = "Output cache används för innehåll som sällan förändras, exempelvis: " },
                                    ],
                                    ListItems =
                                    {
                                        new() { Text = "Sidor" },
                                        new() { Text = "Innehållssektioner" },
                                        new() { Text = "Projektkort" },
                                        new() { Text = "Taggar" },
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = "pt-3",
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new() { Text = "Detta minskar antalet databasförfrågningar och förbättrar laddningstiderna för besökaren." },
                                    },
                                    PaddingsCss = "pb-3",
                                    SpaceBottom = false,
                                },
                                new TextSection
                                {
                                    Heading = "Framtida utveckling",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Även om systemet idag används för den egna portfolion har arkitekturen utvecklats " +
                                            "med betydligt större möjligheter i åtanke."
                                        },
                                         new()
                                        {
                                            Text = "Ett naturligt nästa steg är att bygga en separat administrationsapplikation där " +
                                            "innehåll kan hanteras genom ett grafiskt gränssnitt istället för att seedas in programmatiskt."
                                        },
                                          new()
                                        {
                                            Text = "På längre sikt finns även tankar kring att vidareutveckla lösningen till ett mer " +
                                            "generellt CMS som kan användas i andra projekt. Delar av funktionaliteten skulle exempelvis kunna " +
                                            "paketeras som ett NuGet-paket för att göra systemet återanvändbart även utanför den här webbplatsen."
                                        },
                                           new()
                                        {
                                            Text = "Oavsett hur långt projektet utvecklas har det redan fyllt sitt viktigaste syfte: " +
                                            "att fungera som en plattform för att experimentera, lära sig nya tekniker och bygga erfarenhet " +
                                            "kring arkitektur, datamodellering och innehållshantering i större webbapplikationer."
                                        },
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                },
                            }
                        }
                    };

                    _context.PageContents.Add(sciFiPageContent);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation(":::::: sci-fi PageContent Seeded ::::::");
                }
            }

            #endregion
        }
    }
}



