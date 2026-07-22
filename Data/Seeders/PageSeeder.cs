using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Entities;
using SciFiPortfolio.Enums;
using SciFiPortfolio.Interfaces;
using SciFiPortfolio.Models.ContentSections;
using static System.Collections.Specialized.BitVector32;

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
            if (!await _context.Pages.AnyAsync(page => page.Slug == "/"))
            {
                var Index = new PageEntity
                {
                    Title = "Home",
                    Slug = "/",
                    Published = true,
                    PublishedDate = DateTime.UtcNow
                };

                _context.Pages.Add(Index);
                await _context.SaveChangesAsync();

                _logger.LogInformation(":::::: Index Page Seeded ::::::");
            }

            if (!await _context.Pages.AnyAsync(page => page.Slug == "vps-hosting"))
            {
                var server = new PageEntity
                {
                    Title = "Serverarkitektur",
                    Slug = "vps-hosting",
                    Published = true,
                    PublishedDate = DateTime.UtcNow
                };

                _context.Pages.Add(server);
                await _context.SaveChangesAsync();

                _logger.LogInformation(":::::: Vps Page Seeded ::::::");
            }

            if (!await _context.Pages.AnyAsync(page => page.Slug == "receptakuten"))
            {
                var receptakutenPage = new PageEntity
                {
                    Title = "Receptakuten",
                    Slug = "receptakuten",
                    Published = true,
                    PublishedDate = DateTime.UtcNow
                };

                _context.Pages.Add(receptakutenPage);
                await _context.SaveChangesAsync();

                _logger.LogInformation(":::::: Receptakuten Page Seeded ::::::");
            }

            if (!await _context.Pages.AnyAsync(page => page.Slug == "sci-fi-portfolio"))
            {
                var sciFi = new PageEntity
                {
                    Title = "Sci-fi Portfolio",
                    Slug = "sci-fi-portfolio",
                    Published = true,
                    PublishedDate = DateTime.UtcNow
                };

                _context.Pages.Add(sciFi);
                await _context.SaveChangesAsync();

                _logger.LogInformation(":::::: Sci-fi Page Seeded ::::::");
            }

            var pagesLookUp = await _context.Pages
                .ToDictionaryAsync(x => x.Slug);

            var tags = new List<TagEntity>()
            {
                new(){ Name= ".net-api", Text= ".Net API" },
                new(){ Name= "vue-js", Text = "Vue.js" },
                new(){ Name= "postgresql", Text = "PostgreSql" },
                new(){ Name= "identity", Text = "Identity" },
                new(){ Name= "nginx", Text = "NginX" },
                new(){ Name= "ubuntu-server", Text = "Ubuntu Server" },
                new(){ Name= "docker", Text = "Docker" },
                new(){ Name= "hostinger", Text = "Hostinger" },
                new(){ Name= "razor-pages", Text = "Razor pages" },
                new(){ Name= "vanilla-javascript", Text = "Vanilla javascript" },
                new(){ Name= "ssr", Text = "SSR" },
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
                    ImageUrl = "Images/receptakuten.png",
                    ImageAltText = "receptakuten image",
                    Hyperlink = { LinkText= "Testa den här", LinkUrl="https://receptakuten.net", IconName = "fa-solid fa-arrow-right" },
                    AppLink = { LinkText= "Läs mer om appen", LinkUrl = "/Projects/Receptakuten", PageSlug = "receptakuten", PageId = pagesLookUp["receptakuten"].Id },
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
                    ImageUrl = "Images/server.png",
                    ImageAltText = "image of a server",
                    AppLink = { LinkText="Läs mer", LinkUrl = "Projects/Hosting", PageSlug ="vps-hosting", PageId = pagesLookUp["vps-hosting"].Id },
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
                    ImageUrl = "Images/portimg.jpeg",
                    ImageAltText = "a sci-fi image",
                    AppLink = { LinkText="Läs mer", LinkUrl ="/", PageSlug="sci-fi-portfolio", PageId = pagesLookUp["sci-fi-portfolio"].Id },
                    Tags = {
                        tagLookup["razor-pages"],
                        tagLookup["vanilla-javascript"],
                        tagLookup["ssr"],
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

            var index = pagesLookUp["/"];

            if (index is not null && await _context.Pages.AnyAsync(page => page.Slug == index.Slug))
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
                                        new(){ ImageUrl="Images/vue.svg", AltText= "vue icon"},
                                        new(){ ImageUrl="Images/javascript.svg", AltText= "javascript icon"},
                                        new(){ ImageUrl="Images/net.svg", AltText= ".net icon"},
                                        new(){ ImageUrl="Images/csharp.svg", AltText= "c# icon"},
                                        new(){ ImageUrl="Images/azure.svg", AltText= "azure icon"},
                                        new(){ ImageUrl="Images/api.svg", AltText= "api icon"},
                                        new(){ ImageUrl="Images/docker.svg", AltText= "docker icon"},
                                        new(){ ImageUrl="Images/nginx.svg", AltText= "nginX icon"},
                                        new(){ ImageUrl="Images/postgres-ub.png", AltText= "postgresql icon"},
                                    },
                                },
                                new TextSection()
                                {
                                    Paragraphs =
                                    {
                                        new(){ Text = "Hej! Jag är en .NET Fullstack-utvecklare med ett särskilt intresse för backendutveckling. Jag tycker om att bygga API:er, designa databaser och skapa robusta system som är skalbara, säkra och enkla att underhålla." },
                                        new(){ Text = "Även om backend är där jag känner mig mest hemma har jag också erfarenhet av frontendutveckling med Vue.js, JavaScript, HTML och CSS. Det gör att jag kan ta ansvar för hela utvecklingsprocessen – från databas och API till användargränssnitt och användarupplevelse." },
                                        new(){ Text = "Jag arbetar regelbundet med Docker för containerisering, Nginx för webbserver- och proxyhantering samt Ubuntu Server för drift och deployment. Kombinationen av utveckling och infrastruktur ger mig en helhetsförståelse för hur moderna webbapplikationer byggs, distribueras och underhålls i produktion." },
                                        new(){ Text = "Jag drivs av att lösa problem, lära mig nya tekniker och utveckla lösningar som skapar verkligt värde. Oavsett om det handlar om att optimera prestanda i ett API, bygga nya funktioner eller sätta upp en stabil produktionsmiljö strävar jag alltid efter att leverera kod av hög kvalitet." },
                                        new(){ Text = "När jag inte utvecklar utforskar jag gärna nya tekniker och verktyg för att fortsätta utvecklas som utvecklare och hålla mig uppdaterad inom branschen." }
                                    },
                                    BackgroundColor = "section-bg",
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

            var vpsHosting = pagesLookUp["vps-hosting"];

            if (vpsHosting is not null && await _context.Pages.AnyAsync(page => page.Slug == vpsHosting.Slug))
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
                                    Image = new(){ ImageUrl = "Images/server.png"},
                                    SpaceBottom = false
                                },

                                new ListSection
                                {
                                    SubHeading ="Teknikstack",
                                    Paragraphs = new()
                                    {
                                            new(){ Text = "Jag designade och driftsatte en containerbaserad servermiljö på en VPS-hostad Ubuntu Server. Infrastrukturens fokus ligger på isolering, säkerhet och enkel skalning av applikationer." }
                                    },
                                    ListItems = {
                                        new(){Text="Ubuntu Server (minimal installation)", IconName = ""  },
                                        new(){Text="Docker & Docker Compose", IconName = ""},
                                        new(){Text="Nginx Reverse Proxy", IconName = ""},
                                        new(){Text="PostgreSQL", IconName = ""},
                                        new(){Text="Certbot (Let´s Encrypt)", IconName = ""},
                                        new(){Text="VPS-hosting via Hostinger)", IconName = ""},
                                    },
                                    BackgroundColor = "section-bg",
                                },
                                new ImageWithPositionSection
                                {
                                    Heading = "Infrastrukturdesign",
                                    Paragraphs =
                                    {
                                        new(){ Text = "Servern är uppbyggd enligt en lagerindelad arkitektur där Nginx körs direkt på " +
                                        "värdmaskinen och fungerar som central ingresspunkt för all inkommande trafik." },
                                    },
                                    DesktopImage = { ImageUrl = "images/infra-desktop.png"},
                                    MobileImage = { ImageUrl = "images/infra-mobile.png" },
                                    ImagePosition = ImagePosition.Bottom
                                },
                                new TextSection()
                                {
                                    Heading = "Nätverksarkitektur",
                                    Paragraphs = {
                                        new(){ Text = "Servermiljön är uppbyggd kring en central ingressmodell där Nginx fungerar som reverse proxy " +
                                        "för samtliga applikationer. Genom att hantera SSL, domänrouting och trafikstyrning på värdservern skapas en tydlig " +
                                        "separation mellan publika och interna resurser." },

                                        new(){ Text = "Applikationerna körs i isolerade Docker-nätverk och exponeras inte direkt mot internet. " +
                                        "Istället routas inkommande trafik till rätt container baserat på domän och konfiguration. Lösningen ger en " +
                                        "flexibel grund för att hosta flera tjänster på samma server samtidigt som säkerhet, underhållbarhet och skalbarhet bibehålls." }
                                    },
                                    BackgroundColor = "section-bg",
                                },
                                new TextSection()
                                {
                                    Heading = "Säkerhet",
                                    Paragraphs = {
                                        new(){ Text = "Säkerhet har varit en central del av serverarkitekturen redan från början. " +
                                        "All extern trafik krypteras med SSL-certifikat från Let's Encrypt som automatiskt förnyas med hjälp av Certbot. " +
                                        "Nginx fungerar som den enda publika ingresspunkten och ansvarar för att dirigera trafiken vidare till rätt tjänst." },

                                        new(){ Text = "Databaser exponeras aldrig direkt mot internet utan är endast åtkomliga från de " +
                                        "containrar som behöver kommunicera med dem. Genom privata Docker-nätverk och separerade applikationsmiljöer " +
                                        "skapas ytterligare ett skyddslager som begränsar åtkomsten mellan olika system." }
                                    },
                                },
                                new DockerSection()
                                {
                                    Heading = "Containerisering",
                                    LeftParagraph = { Text= "Säkerhet har varit en central del av serverarkitekturen redan från början. All extern trafik krypteras med " +
                                    "SSL-certifikat från Let's Encrypt som automatiskt förnyas med hjälp av Certbot. Nginx fungerar som den enda publika " +
                                    "ingresspunkten och ansvarar för att dirigera trafiken vidare till rätt tjänst. Databaser exponeras aldrig " +
                                        "direkt mot internet utan är endast åtkomliga från de containrar som behöver kommunicera med dem. Genom privata " +
                                        "Docker-nätverk och separerade applikationsmiljöer skapas ytterligare ett skyddslager som begränsar åtkomsten mellan olika system."},

                                    RightParagraph = { Text= "För att skapa en stabil och lättadministrerad driftmiljö är plattformen uppbyggd kring Docker." +
                                    "Varje applikation körs i sin egen isolerade container tillsammans med en dedikerad PostgreSQL-databas. Genom att separera tjänsterna " +
                                    "från varandra minimeras risken att ett problem i en applikation påverkar övriga system på servern."},

                                    SpaceBottom = true,
                                    BackgroundColor = "section-bg",
                                },
                                new TextSection()
                                {
                                    Heading = "Skalbarhet",
                                    Paragraphs = {
                                        new(){ Text = "Infrastrukturen är designad för att enkelt kunna växa när nya projekt eller tjänster " +
                                        "behöver läggas till. Genom Docker Compose definieras hela miljön som kod, vilket innebär att samma " +
                                        "konfiguration kan återskapas på nya servrar med minimala manuella insatser." },

                                        new(){ Text = "När en ny applikation ska driftsättas skapas en separat containeruppsättning med egna miljövariabler, " +
                                        "databasresurser och nätverksinställningar. Nginx kan därefter konfigureras för att routa trafik till den nya tjänsten " +
                                        "utan att påverka redan existerande applikationer." },

                                        new(){ Text = "Detta arbetssätt ger konsekventa miljöer mellan utveckling, test och produktion samtidigt " +
                                        "som det förenklar både felsökning och framtida expansion." },
                                    },
                                },
                                new TextSection()
                                {
                                    Heading = "Resultat",
                                    Paragraphs = {
                                        new(){ Text = "Den färdiga plattformen ger en robust grund för att hosta flera applikationer på samma " +
                                        "server utan att kompromissa med säkerhet eller underhållbarhet. Genom containerisering, centraliserad trafikhantering " +
                                        "och automatiserad certifikathantering har infrastrukturen blivit både enkel att administrera och enkel att vidareutveckla." },

                                        new(){ Text = "Arkitekturen möjliggör snabb driftsättning av nya projekt samtidigt som befintliga tjänster kan uppdateras " +
                                        "oberoende av varandra. Resultatet är en flexibel och skalbar plattform som kan växa i takt med nya behov utan att kräva större " +
                                        "förändringar i den underliggande infrastrukturen." },
                                    },
                                    BackgroundColor = "section-bg",
                                    SpaceBottom = false,
                                },
                            }
                        }
                    };

                    _context.PageContents.Add(vpsHostingPageContent);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation(":::::: Vps Hosting PageContent Seeded ::::::");
                }
            };

            var receptakuten = pagesLookUp["receptakuten"];

            if (receptakuten is not null && await _context.Pages.AnyAsync(page => page.Slug == receptakuten.Slug))
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
                                    new ImageWithPositionSection
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
                                        DesktopImage = { ImageUrl = "images/recept-bil-ub.png"},
                                        ImagePosition = ImagePosition.Top,
                                        BackgroundColor = "",

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
                                        BackgroundColor = "section-bg",

                                    },
                                    new ImageWithPositionSection
                                    {
                                        Heading = "Backend",
                                        Paragraphs =
                                        {
                                            new()
                                            {
                                                Text = "Backenden är utvecklad enligt principerna för Clean Architecture i tankarna men följer inte fullt ut men försöker hålla ansvar och beroenden tydliga och " +
                                                "separerade mellan olika lager (Bytte namn från Meal Menu till Receptakuten när projektet närmade sig sitt slut)."
                                            },
                                        },
                                        DesktopImage = { ImageUrl = "images/receptakuten-layers.png"},
                                        ImagePosition = ImagePosition.Bottom,
                                        BackgroundColor = "",
                                        SpaceBottom = false,
                                        Paddings = false,

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
                                        BackgroundColor = "",
                                        SpaceBottom = false,
                                        Paddings = false,
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
                                        BackgroundColor = "",
                                        SpaceBottom = false,
                                        Paddings = true,

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
                                            new() { Text = "CSRF-skydd (XXS ?)" },
                                            new() { Text = "Rate Limiting" },
                                            new() { Text = "Inputvalidering" },
                                            new() { Text = "Roll- och behörighetskontroller" },
                                        },
                                        SpaceBottom = false,
                                        Paddings = false,

                                    },
                                    new TextSection
                                    {
                                        Paragraphs =
                                        {
                                            new()
                                            {
                                                Text= "Utöver klientbaserade begränsningar verifieras samtliga rättigheter även på serversidan för att " +
                                                "säkerställa att obehöriga användare inte kan kringgå systemets regler genom manipulerade anrop."
                                            },
                                            new()
                                            {
                                                Text= "För att aktivera nya konton krävs e-postverifiering och samma mekanism används vid glömt lösenord."
                                            }
                                        },
                                        Paddings = false,
                                        SpaceBottom = false,

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
                                        BackgroundColor = "",
                                        SpaceBottom = false,
                                        Paddings = true,

                                    },
                                    new ImageCenterSection
                                    {
                                        SubHeading = "Bildhantering",
                                        ParagraphsTop =
                                        {
                                            new()
                                            {
                                                Text="Användaruppladdade bilder bearbetas på serversidan med hjälp av ImageMagick via Magick.NET. Där kontroll av accepterade bildtypr kontrolleras, storlek osv"
                                            },

                                        },
                                        DesktopImages = {new() { ImageUrl= "images/allowed-images.png" } },
                                        ParagraphsBottom =
                                        {
                                            new()
                                            {
                                                Text="Vid uppladdning genomförs konvertering och storlek/kvallitets optimering för att minska lagringsutrymme och förbättra laddningstider. " +
                                                "Bilderna lagras därefter på servern och kopplas till respektive recept. Nedan kan man se att Clean Code mönster följs med flera små metoder " +
                                                "används för att ge klarare förståelse när man läser koden, detta upprepas över hela projektet."
                                            },
                                        },
                                        BackgroundColor = "",
                                        SpaceBottom = false,
                                        Paddings = false,
                                    },
                                    new ImageSection
                                    {
                                        DesktopImages =
                                        {
                                            new() { ImageUrl = "images/save-image.png"}
                                        },
                                        BackgroundColor = "",
                                        Paddings = false
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
                                        Paddings = false,
                                    },
                                    new TextSection
                                    {
                                        Paragraphs =
                                        {
                                            new() { Text = "Dessa jobb configureras med hjälp av nycklar och triggers där man sedan anger när de ska köras, Här kan ni se ett exempel på ett av dess jobb: " },
                                        },
                                        Paddings = false,
                                        SpaceBottom = false,
                                    },
                                    new FlexSection
                                    {
                                        ImagesLeft = [ new () { ImageUrl = "images/quartz-conf-2.png" } ],
                                        ImagesRight = [ new () { ImageUrl = "images/quartz-job-ex-3.png" } ],
                                        SpaceBottom = false,
                                        Paddings = false
                                    },
                                    new TextSection
                                    {
                                        Paragraphs =
                                        {
                                            new()
                                            {
                                                Text= "Genom att flytta denna typ av arbete till separata processer kan API:t fokusera" +
                                                " på användarrelaterade förfrågningar samtidigt som återkommande underhåll sker automatiskt."
                                            },
                                        },
                                        Paddings = false,
                                        SpaceBottom = false,
                                    },
                                    new TextSection
                                    {
                                        SubHeading = "Loggning och felhantering",
                                        Paragraphs =
                                        {
                                            new()
                                            {
                                                Text= "För övervakning och felsökning används Serilog med stöd för både konsolloggning och filbaserad loggning."
                                            },
                                            new()
                                            {
                                                Text= " Systemet använder en central Global Exception Handler som fångar upp oväntade fel och returnerar konsekventa felmeddelanden till klienten. " +
                                                "Detta minskar mängden duplicerad felhantering i applikationen och förenklar felsökning."
                                            },
                                            new()
                                            {
                                                Text= "I särskilda situationer där återhämtning är möjlig används riktade try/catch-block för att hantera specifika undantag," +
                                                "exempelvis vid filhantering och borttagning av resurser från servern."
                                            },
                                        },
                                        Paddings = true,
                                        SpaceBottom = true,
                                    },
                                    new TextSection
                                    {
                                        Heading = "Frontend",
                                        Paragraphs =
                                        {
                                            new()
                                            {
                                                Text="Frontenden är utvecklad i Vue.js 3 med Composition API och bygger på en komponentbaserad arkitektur där " +
                                                "återanvändbarhet och tydlig ansvarsfördelning har varit centrala designprinciper."
                                            },
                                            new()
                                            {
                                                Text=" Applikationen består av ett stort antal specialiserade komponenter för recept, ingredienshantering, " +
                                                "gruppadministration, matscheman, formulär och dialogrutor. Kommunikation mellan komponenter sker genom props, " +
                                                "emitters och gemensamma eventflöden, vilket möjliggör en tydlig separation mellan presentation och affärslogik."
                                            },new()
                                            {
                                                Text="Navigeringen hanteras med Vue Router där olika layouts används beroende på användarens autentiseringsstatus. " +
                                                "Publika sidor såsom inloggning och registrering använder en separat layout från de delar av applikationen som kräver inloggning."
                                            },new()
                                            {
                                                Text="För att förbättra användarupplevelsen lagras utvald data lokalt i klienten för att minska mängden upprepade API-anrop. " +
                                                "Detta bidrar till snabbare laddningstider och en mer responsiv användarupplevelse."
                                            },
                                            new()
                                            {
                                                Text="Klienten innehåller även stöd för automatisk konvertering av bildformat såsom HEIC innan uppladdning, " +
                                                "vilket säkerställer kompatibilitet mellan olika enheter och webbläsare."
                                            }
                                        },
                                        BackgroundColor = "section-bg"
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



