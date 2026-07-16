using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Models.ContentSections;
using SciFiPortfolio.ViewModels;


namespace SciFiPortfolio.Pages
{
    public class testModel : PageModel
    {

        public TestViewModel Vm { get; set; } = new();

        public void OnGet()
        {
            var sections = new List<ContentSection>()
            {
                new HeroSection
                {
                    Heading = "Fredrik Bengtsson Fullstack .Net Utvecklare",
                    SpaceBottm = true
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
                        new(){ ImageUrl="Images/nginx.svg", AltText= "nginX icon"}
                    },
                },
                new TextSection
                {
                    Paragraphs =
                    {
                         new(){ Text = "Hej! Jag är en .NET Fullstack-utvecklare med ett särskilt intresse för backendutveckling. Jag tycker om att bygga API:er, designa databaser och skapa robusta system som är skalbara, säkra och enkla att underhålla." },
                            new(){ Text = "Även om backend är där jag känner mig mest hemma har jag också erfarenhet av frontendutveckling med Vue.js, JavaScript, HTML och CSS. Det gör att jag kan ta ansvar för hela utvecklingsprocessen – från databas och API till användargränssnitt och användarupplevelse." },
                            new(){ Text = "Jag arbetar regelbundet med Docker för containerisering, Nginx för webbserver- och proxyhantering samt Ubuntu Server för drift och deployment. Kombinationen av utveckling och infrastruktur ger mig en helhetsförståelse för hur moderna webbapplikationer byggs, distribueras och underhålls i produktion." },
                            new(){ Text = "Jag drivs av att lösa problem, lära mig nya tekniker och utveckla lösningar som skapar verkligt värde. Oavsett om det handlar om att optimera prestanda i ett API, bygga nya funktioner eller sätta upp en stabil produktionsmiljö strävar jag alltid efter att leverera kod av hög kvalitet." },
                            new(){ Text = "När jag inte utvecklar utforskar jag gärna nya tekniker och verktyg för att fortsätta utvecklas som utvecklare och hålla mig uppdaterad inom branschen." }
                    },
                    BackgroundColor = true
                },
                new TextSection
                {
                    Paragraphs =
                    {
                         new(){ Text = "Hej! Jag är en .NET Fullstack-utvecklare med ett särskilt intresse för backendutveckling. Jag tycker om att bygga API:er, designa databaser och skapa robusta system som är skalbara, säkra och enkla att underhålla." },
                            new(){ Text = "Även om backend är där jag känner mig mest hemma har jag också erfarenhet av frontendutveckling med Vue.js, JavaScript, HTML och CSS. Det gör att jag kan ta ansvar för hela utvecklingsprocessen – från databas och API till användargränssnitt och användarupplevelse." },
                            new(){ Text = "Jag arbetar regelbundet med Docker för containerisering, Nginx för webbserver- och proxyhantering samt Ubuntu Server för drift och deployment. Kombinationen av utveckling och infrastruktur ger mig en helhetsförståelse för hur moderna webbapplikationer byggs, distribueras och underhålls i produktion." },
                            new(){ Text = "Jag drivs av att lösa problem, lära mig nya tekniker och utveckla lösningar som skapar verkligt värde. Oavsett om det handlar om att optimera prestanda i ett API, bygga nya funktioner eller sätta upp en stabil produktionsmiljö strävar jag alltid efter att leverera kod av hög kvalitet." },
                            new(){ Text = "När jag inte utvecklar utforskar jag gärna nya tekniker och verktyg för att fortsätta utvecklas som utvecklare och hålla mig uppdaterad inom branschen." }
                    },
                    BackgroundColor = true
                }
            };

            Vm.Sections.AddRange(sections);

        }
    }
}
