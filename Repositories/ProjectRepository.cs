using SciFiPortfolio.Interfaces.Repositories;
using SciFiPortfolio.Models;

namespace SciFiPortfolio.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public async Task<List<ProjectCard>> GetProjectCards()
        {
            List<ProjectCard> cards = new()
            {
                new()
                {
                    Title = "SPA Applikation",
                    Image = { ImageUrl = "Images/receptakuten.png", AltText = "receptakuten image"},
                    Link = { LinkText= "Testa den här", LinkUrl="https://receptakuten.net", Icon = new(){ IconText = "fa-solid fa-arrow-right" } },
                    LinkButton = { LinkText="Läs mer om appen", LinkUrl="/Projects/ReceptakutenPage" },
                    Tags = {
                        new(){ TagText= ".Net API" },
                        new(){ TagText = "Vue.js" },
                        new(){ TagText = "PostgreSql" },
                        new(){ TagText = "Identity" }
                    }
                },
                new()
                {
                    Title = "Vps hosting",
                    Image = { ImageUrl = "Images/server.png", AltText = "image of a server"},
                    LinkButton = { LinkText="Läs mer", LinkUrl="Projects/ServerPage" },
                    Tags = {
                        new(){ TagText= "NginX" },
                        new(){ TagText = "Ubuntu Server" },
                        new(){ TagText = "Docker" },
                        new(){ TagText = "Hostinger" }
                    }
                },
                new()
                {
                    Title = "Sci-fi portfolio",
                    Image = { ImageUrl = "Images/portimg.jpeg", AltText = "a sci-fi image"},
                    LinkButton = { LinkText="Läs mer", LinkUrl="/" },
                    Tags = {
                        new(){ TagText= "Razor pages" },
                        new(){ TagText = "Vanilla javascript" },
                        new(){ TagText = "SSR" },
                    }
                }
            };

            return cards;
        }
    }
}
