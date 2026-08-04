using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Enums;
using SciFiPortfolio.Models.ContentSections;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages
{
    public class TestModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        public TestViewModel Vm { get; set; } = new();

        public TestModel(IWebHostEnvironment env) => _env = env;

        public void OnGet()
        {

            if (!_env.IsDevelopment())
                return;

            var sections = new List<ContentSection>()
            {
                //new HeroSection
                //{
                //    Heading = "Portfolio",
                //    SubHeading = "Razor pages / Cms",
                //    TextColor = "sci-fi-glow",
                //},
                //new TextSection
                //{
                //    Heading = "Portfolio och CMS-plattform",
                //    BackgroundColorCss = "section-bg",
                //    BordersCss = "border-all border-primary",
                //    Paragraphs =
                //    {
                //        new()
                //        {
                //            Text = "Det här projektet började egentligen som något betydligt enklare. " +
                //            "Tanken från början var att bygga en traditionell webbplats med ASP.NET Core Razor Pages " +
                //            "där varje sida hade sin egen vy med innehåll direkt skrivet i koden."
                //        },
                //        new()
                //        {
                //            Text = "Efter hand började jag dock fundera på hur innehållet skulle hanteras när webbplatsen växte. " +
                //            "Att hårdkoda all text och allt innehåll direkt i Razor-vyerna fungerade, " +
                //            "men kändes ganska begränsande och inte särskilt flexibelt. Nästa steg blev därför att flytta " +
                //            "innehållet till tjänster och modeller för att separera innehåll från presentation."
                //        },
                //        new()
                //        {
                //            Text = "Ju längre projektet utvecklades desto tydligare blev det att innehållet egentligen borde lagras i " +
                //            "en databas. Samtidigt kändes det inte optimalt att skapa separata tabeller och kolumner för varje " +
                //            "tänkbar sektion som kunde förekomma på en sida. Om varje ny " +
                //            "innehållstyp krävde databasändringar, migrationer och nya modeller skulle systemet snabbt bli svårt att underhålla."
                //        },
                //        new()
                //        {
                //            Text = "Det var ungefär där tankarna började röra sig mot ett CMS-liknande upplägg. Från att ha varit en enkel " +
                //            "portfoliosida började projektet successivt utvecklas till en mer generell plattform där innehållet " +
                //            "styrs av data istället för hårdkodade vyer."
                //        },
                //        new()
                //        {
                //            Text = "Även om ett befintligt CMS hade kunnat lösa många av dessa problem är syftet med projektet inte bara att få fram en färdig webbplats. " +
                //            "En stor del av motivationen har varit att lära sig nya tekniker, utforska olika arkitekturmönster och bygga något från " +
                //            "grunden för att förstå hur systemen fungerar bakom kulisserna. Många delar av lösningen är därför mer " +
                //            "avancerade än vad som egentligen krävs för en portfolio, men just den typen av tekniska utmaningar är också det som gör projektet roligt att arbeta med."
                //        }
                //    }
                //},
                //new TextSection
                //{
                //    Heading = "Övergripande arkitektur",
                //    Paragraphs =
                //    {
                //        new()
                //        {
                //            Text = "Webbplatsen är byggd med ASP.NET Core Razor Pages och PostgreSQL som databas."
                //        },
                //        new()
                //        {
                //            Text = "Istället för att varje sida har en egen hårdkodad struktur består systemet till stor " +
                //            "del av sidor som byggs upp dynamiskt från innehållsblock som lagras i databasen. Samtidigt har det " +
                //            "varit viktigt att inte låsa hela webbplatsen till CMS-strukturen. Eftersom Razor Pages i grunden " +
                //            "är sidcentrerat finns det fortfarande möjlighet att bygga vissa sidor mer traditionellt när det passar bättre."
                //        },
                //        new()
                //        {
                //            Text = "Exempel på detta är startsidan, sidan om mig eller andra vyer där innehållet är mer unikt " +
                //            "och inte nödvändigtvis behöver representeras av återanvändbara innehållsblock. I sådana fall kan " +
                //            "sidan byggas upp direkt i vyn eller med hjälp av befintliga sektionsmodeller utan att använda den generella renderingskomponenten."
                //        },
                //        new()
                //        {
                //            Text = "Tanken är att CMS-funktionaliteten ska vara ett verktyg och inte ett krav. Om en " +
                //            "specifik sida behöver specialanpassad logik eller innehåll som bara kommer att användas på " +
                //            "ett enda ställe finns det inget som hindrar att den implementeras mer direkt. Detta ger en " +
                //            "balans mellan flexibiliteten i det dynamiska innehållssystemet och enkelheten i traditionella " +
                //            "Razor Pages när det är den mest lämpliga lösningen."
                //        },
                //        new()
                //        {
                //            Text = "Målet har hela tiden varit att skapa en flexibel struktur där nya sidor och sektioner " +
                //            "kan byggas på olika sätt beroende på behov, utan att den grundläggande arkitekturen behöver förändras."
                //        }
                //    }
                //},
                //new ListSection
                //{
                //    Heading = "Databasdesign",
                //    Paragraphs =
                //    [
                //        new() { Text = "Databasen bygger på ett antal centrala entiteter som beskriver webbplatsens innehåll." },
                //        new()
                //        {
                //            Text = "Varje sida representeras av en egen Page-entitet som innehåller grundläggande information om sidan, " +
                //            "exempelvis titel, slug och metadata. Till varje sida finns sedan ett eller flera PageContent-objekt kopplade " +
                //            "som beskriver det faktiska innehållet som ska visas."
                //        },
                //        new() { Text = "För innehåll som återanvänds på flera ställen används separata entiteter i databasen. Exempel på detta är:" },
                //    ],
                //    ListItems =
                //    {
                //        new() { Text = "Projektkort" },
                //        new() { Text = "Taggar" },
                //        new() { Text = "Övriga återanvändbara objekt som man kan tänkas vilja återanvända senare" }
                //    },
                //    BackgroundColorCss = "section-bg",
                //    PaddingsCss = "pt-3",
                //    BordersCss = "border-top border-primary",
                //    SpaceBottom = false,
                //},
                //new ImageWithPositionSection
                //{
                //    Paragraphs =
                //    {
                //        new()
                //        {
                //            Text = "På så sätt undviks duplicering av data samtidigt som samma information kan användas på flera sidor."
                //        },
                //        new()
                //        {
                //            Text = "Eftersom systemet bygger på ett blockbaserat innehållssystem där innehållssektioner lagras som JSON " +
                //            "istället för i separata databastabeller blir databasstrukturen relativt enkel och kompakt. För den nuvarande " +
                //            "omfattningen räcker denna modell väl, samtidigt som den ger stor flexibilitet när nya innehållstyper behöver introduceras. " +
                //            "I takt med att CMS:et vidareutvecklas kommer sannolikt fler entiteter och relationer att tillkomma, men den grundläggande " +
                //            "strukturen kan fortfarande hållas förhållandevis enkel. En sida kan exempelvis innehålla flera innehållssektioner samtidigt som " +
                //            "samma taggar, projektkort eller andra återanvändbara objekt kan användas på flera platser i systemet."
                //        }
                //    },
                //    DesktopImage = new(){ ImageUrl = "images/portfolio-db-desktop.png" },
                //    MobileImage = new() { ImageUrl = "images/portfolio-bd-mobile.png" },
                //    ImagePosition = ImagePosition.Bottom,
                //    PaddingsCss = "pb-3",
                //    BordersCss = "border-bottom border-primary",
                //    BackgroundColorCss = "section-bg"
                //},
                //new ImageCenterSection
                //{
                //    Heading = "Dynamiskt innehållssystem",
                //    ParagraphsTop =
                //    {
                //        new(){ Text = "Kärnan i systemet är det blockbaserade innehållssystemet." },
                //        new()
                //        { 
                //            Text = "Istället för att lagra varje innehållstyp i egna databaskolumner lagras " +
                //            "innehållssektioner som JSON i databasen. När innehållet sparas serialiseras " +
                //            "objekten automatiskt och när de hämtas deserialiseras de tillbaka till sina respektive modeller." 
                //        },

                //    },
                //    DesktopImages =
                //    {
                //        new() { ImageUrl = "images/json-content.png" },
                //        new() { ImageUrl = "images/serialize-deserialize.png" },
                //    },
               
                //    ParagraphsBottom =
                //    {
                //        new(){ Text = "Detta gör att nya innehållstyper kan introduceras utan att databasen behöver ändras." }
                //    },
                //    PaddingsCss = "pt-3",
                //    SpaceBottom = false,
                //},
                //new ListSection
                //{
                //    Paragraphs =
                //    [
                //        new() { Text = "Detta gör att nya innehållstyper kan introduceras utan att databasen behöver ändras." },
                //        new() { Text = "Exempel på innehållsblock som finns idag är:" },
                //    ],
                //    ListItems =
                //    {
                //        new() { Text = "TextSection" },
                //        new() { Text = "ImageSection" },
                //        new() { Text = "ImageWithPositionSection" },
                //        new() { Text = "ListSection" },
                //        new() { Text = "HeroSection" },
                //        new() { Text = "CardSection" }
                //    },
                //    PaddingsCss = "",
                //    SpaceBottom = false,
                //},
                //new TextSection
                //{
                //    Paragraphs =
                //    {
                //        new()
                //        {
                //            Text = "Fördelen med denna lösning är att en sida kan byggas upp av valfria kombinationer av sektioner " +
                //            "samtidigt som databasen förblir relativt enkel."
                //        },
                //    },
                //    PaddingsCss = "pb-3"
                //},
                //new ImageWithPositionSection
                //{
                //    SubHeading = "Här är ett exempel på hur ett sektions block är uppbyggt",
                //    Paragraphs =
                //    {
                //        new()
                //        {
                //            Text = "En modell byggs upp för sektionen med dess olika egenskaper och ärver fler från en basklass."
                //        },
                //    },
                //    DesktopImage = new(){ ImageUrl = "images/image-position-model.png" },
                //    ImagePosition = ImagePosition.Bottom,
                //    PaddingsCss = "pt-3 pb-3",
                //    BordersCss = "border-top border-primary",
                //    BackgroundColorCss = "section-bg",
                //    SpaceBottom = false,
                    
                //},
                //new ImageWithPositionSection
                //{
                //    Paragraphs =
                //    {
                //        new()
                //        {
                //            Text = "ContentSection fungerar som basklass för samtliga innehållssektioner. " +
                //            "Klassen innehåller gemensamma egenskaper samtidigt som JsonDerivedType-attributen används för att " +
                //            "konfigurera polymorf serialisering och deserialisering. Detta gör att systemet automatiskt " +
                //            "kan avgöra vilken sektionstyp som ska instansieras när innehåll hämtas från databasen."
                //        }
                //    },
                //    DesktopImage = new(){ ImageUrl = "images/content-section.png" },
                //    ImagePosition = ImagePosition.Bottom,
                //    PaddingsCss = "",
                //    BackgroundColorCss = "section-bg",
                //    SpaceBottom = false,

                //},
                //new FlexSection
                //{
                //    ParagraphsLeft =
                //    [
                //        new()
                //        {
                //            Text = "I detta fallet är partialen uppdelad i olika delar för läsbarhetens skull. Här kontrolleras det vilken position som är vald för bilden."
                //        },
                //    ],
                //    ImagesLeft =
                //    [
                //        new() { ImageUrl = "images/image-position-partial.png" }
                //    ],
                //    ParagraphsRight =
                //    [
                //        new()
                //        {
                //            Text = "Sedan renderas bilden på rätt plats i vyn, i detta fallet så är det en topp/bottnen placering (kunde ha gjort detta enklare med bara css-klasser...men detta får vara just nu)."
                //        }
                //    ],          
                //    ImagesRight =
                //    [
                //        new() { ImageUrl = "images/image-y-partial.png" }
                //    ],
                //    BackgroundColorCss = "section-bg",
                //    BordersCss = "border-bottom border-primary"
                //},
                //new ImageWithPositionSection
                //{
                //    Heading = "Rendering av sidor",
                //    Paragraphs =
                //    {
                //        new() { Text = "Alla projektsidor använder samma Razor Page för rendering." },
                //        new() { Text = "Istället för att skapa en separat sida för varje projekt används en gemensam renderingsmodell där rätt innehåll hämtas baserat på sidans slug." },
                //    },
                //    DesktopImage = { ImageUrl = "images/project-renderer.png" },
                //    ImagePosition = ImagePosition.Bottom,
                //    SpaceBottom = false,
                //    PaddingsCss = "pt-3"
                //},
                //new TextSection
                //{
                //    Paragraphs =
                //    {
                //        new() { Text = "Själva renderingen sker genom en specialiserad View " +
                //        "Component som fungerar som en central renderingsmotor för systemet. " +
                //        "Komponenten går igenom innehållssektionerna och avgör vilken partialvy som ska användas för respektive sektionstyp." },   
                //    },
                //    SpaceBottom = false,
                //    PaddingsCss = ""
                //},
                //new ImageSection
                //{
                //    DesktopImages =
                //    {
                //        new() { ImageUrl = "images/view-component.png" },
                //        new() { ImageUrl = "images/view-component-view.png" }
                //    },
                //    SpaceBottom = false,
                //    PaddingsCss = ""
                //},
                //new TextSection
                //{
                //    Paragraphs =
                //    {
                //        new() 
                //        { 
                //            Text = "För att göra systemet mer robust används även ICompositeViewEngine för att kontrollera att " +
                //            "en partialvy faktiskt existerar innan den renderas. På så sätt upptäcks eventuella fel tidigt samtidigt som det " +
                //            "blir enklare att utveckla nya innehållsblock." 
                //        },
                //    },
                //    SpaceBottom = false,
                //    PaddingsCss = ""
                //},
                //new ImageSection
                //{
                //    DesktopImages =
                //    {
                //        new() { ImageUrl = "images/renderer-service.png" },  
                //    },
                //    SpaceBottom = false,
                //    PaddingsCss = ""
                //},
                //new ListSection
                //{
                //    Paragraphs =
                //    [
                //        new() 
                //        { 
                //            Text = "Nu blir det enkelt att skapa nya " +
                //            "sektionstyper utan att behöva ändra på renderingslogiken, det enda man behöver göra är att skapa:" 
                //        },
                //    ],
                //    ListItems =
                //    {
                //        new() { Text = "En modell" },
                //        new() { Text = "En partialvy" },
                //    },
                //    PaddingsCss = "pb-3",
                //},
                //new TextSection
                //{
                //    Heading = "Servicelager",
                //    Paragraphs =
                //    {
                //        new() { Text = "För närvarande används främst en central PageService som ansvarar för att hämta sidor, innehåll och relaterade objekt." },
                //        new()
                //        {
                //            Text = "Under utvecklingen har detta varit en enkel lösning som gjort det möjligt att snabbt bygga vidare på funktionaliteten. " +
                //            "Tanken är dock att servicelagret framöver ska delas upp i mer specialiserade tjänster med tydligare ansvarsområden men just nu " +
                //            "när systemet inte är så stort så fungerar det utmärkt."
                //        },
                //        new()
                //        {
                //            Text = "Samtidigt finns planer på att införa en gemensam basklass för repositories för att minska duplicerad kod och skapa " +
                //            "en mer enhetlig dataåtkomst."
                //        },
                //    },
                //    BackgroundColorCss = "section-bg",
                //    BordersCss = "border-all border-primary"
                //},
                //new ListSection
                //{
                //    Heading = "Temasystem",
                //    Paragraphs =
                //    [
                //        new() { Text = "Webbplatsen innehåller ett temasystem där användaren kan välja mellan flera olika visuella teman." },
                //        new() { Text = "Bland de teman som finns idag återfinns bland annat:" },
                //    ],
                //    ListItems =
                //    {
                //        new() { Text = "Sci-Fi" },
                //        new() { Text = "Cyberpunk" },
                //        new() { Text = "Matrix" },
                //        new() { Text = "Deep Space" },
                //        new() { Text = "Plasma Core" },
                //        new() { Text = "Alien Tech" },
                //        new() { Text = "Starfleet" },
                //    },
                //    PaddingsCss = "pt-3",
                //    SpaceBottom = false,
                //},
                //new TextSection
                //{
                //    Paragraphs =
                //    {
                //        new()
                //        {
                //            Text = "Temana bygger på CSS-variabler och ett data-theme-attribut på rot-elementet. " +
                //            "Genom att byta tema ändras färger, gradienter, skuggor, bakgrunder och visuella effekter " +
                //            "utan att de enskilda komponenterna behöver känna till vilket tema som används.."
                //        },
                //        new() { Text = "Det gör det enkelt att introducera nya teman samtidigt som gränssnittet förblir konsekvent." },
                //    },
           
                //    PaddingsCss = "pb-3"
                //},   
                //new ListSection
                //{
                //    Heading = "Responsiv design",
                //    Paragraphs =
                //    [
                //        new() { Text = "Hela webbplatsen är byggd med responsiv design som utgångspunkt." },
                //        new() { Text = "Layout och innehåll anpassar sig automatiskt efter skärmstorleken vilket gör att webbplatsen fungerar på:" },
                //    ],
                //    ListItems =
                //    {
                //        new() { Text = "Dator" },
                //        new() { Text = "Surfplatta" },
                //        new() { Text = "Mobiltelefon" },
                //    },
                //    SpaceBottom = false,
                //    PaddingsCss = "pt-3",
                //    BackgroundColorCss = "section-bg",
                //    BordersCss = "border-top border-primary"
                //},
                //new TextSection
                //{
                //    Paragraphs =
                //    {
                //        new()
                //        {
                //            Text = "Målet har varit att samma innehåll ska fungera oavsett enhet utan att separata mobilversioner behöver utvecklas."
                //        },
                //    },
                //    PaddingsCss = "pb-3",
                //    BackgroundColorCss = "section-bg",
                //    BordersCss = "border-bottom border-primary"
                //},
                //new ListSection
                //{
                //    Heading = "Prestanda och caching",
                //    Paragraphs =
                //    [
                //        new() { Text = "Eftersom att innehållet är identiskt för alla besökare lämpar sig systemet väl för caching." },
                //        new() { Text = "In-memory caching används för innehåll som sällan förändras, exempelvis: " },
                //    ],
                //    ListItems =
                //    {
                //        new() { Text = "Sidor" },
                //        new() { Text = "Innehållssektioner" },
                //        new() { Text = "Projektkort" },
                //        new() { Text = "Taggar" },
                //    },
                //    SpaceBottom = false,
                //    PaddingsCss = "pt-3",
                //},
                //new TextSection
                //{
                //    Paragraphs =
                //    {
                //        new() { Text = "Detta minskar antalet databasförfrågningar och förbättrar laddningstiderna för besökaren." },
                //    },
                //    PaddingsCss = "pb-3",
                //},
                //new TextSection
                //{
                //    Heading = "Framtida utveckling",
                //    Paragraphs =
                //    {
                //        new() 
                //        { 
                //            Text = "Även om systemet idag används för den egna portfolion har arkitekturen utvecklats " +
                //            "med betydligt större möjligheter i åtanke." 
                //        },
                //         new()
                //        {
                //            Text = "Ett naturligt nästa steg är att bygga en separat administrationsapplikation där " +
                //            "innehåll kan hanteras genom ett grafiskt gränssnitt istället för att seedas in programmatiskt."
                //        },
                //          new()
                //        {
                //            Text = "På längre sikt finns även tankar kring att vidareutveckla lösningen till ett mer " +
                //            "generellt CMS som kan användas i andra projekt. Delar av funktionaliteten skulle exempelvis kunna " +
                //            "paketeras som ett NuGet-paket för att göra systemet återanvändbart även utanför den här webbplatsen."
                //        },
                //           new()
                //        {
                //            Text = "Oavsett hur långt projektet utvecklas har det redan fyllt sitt viktigaste syfte: " +
                //            "att fungera som en plattform för att experimentera, lära sig nya tekniker och bygga erfarenhet " +
                //            "kring arkitektur, datamodellering och innehållshantering i större webbapplikationer."
                //        },
                //    },
                //    BackgroundColorCss = "section-bg",
                //    BordersCss = "border-all border-primary",
                //},
            };

            Vm.Sections.AddRange(sections);
        }
    }
}
