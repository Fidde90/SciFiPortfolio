using SciFiPortfolio.Interfaces.Repositories;
using SciFiPortfolio.Models;

namespace SciFiPortfolio.Repositories
{
    public class ImageRepository : IImageRepository
    {
        public async Task<List<Image>> GetTechImages()
        {
            List<Image> images = new()
            {
                new(){ ImageUrl="Images/vue.svg", AltText= "vue icon"},
                new(){ ImageUrl="Images/javascript.svg", AltText= "javascript icon"},
                new(){ ImageUrl="Images/net.svg", AltText= ".net icon"},
                new(){ ImageUrl="Images/csharp.svg", AltText= "c# icon"},
                new(){ ImageUrl="Images/azure.svg", AltText= "azure icon"},
                new(){ ImageUrl="Images/api.svg", AltText= "api icon"},
                new(){ ImageUrl="Images/docker.svg", AltText= "docker icon"},
                new(){ ImageUrl="Images/nginx.svg", AltText= "nginX icon"}
            };

            return images;
        }
    }
}
