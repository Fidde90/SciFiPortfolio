using SciFiPortfolio.Models;
using System.ComponentModel.DataAnnotations;

namespace SciFiPortfolio.Entities
{
    public class ProjectCardEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MinLength(1)]
        public string Title { get; set; } = "";

        public string UniqueName { get; set; } = null!;

        public string ImageUrl { get; set; } = "images/portimg.png";

        public string ImageAltText { get; set; } = "";

        public HyperLink Hyperlink { get; set; } = new();

        public AppLink AppLink { get; set; } = new();


        public ICollection<TagEntity> Tags { get; set; } = [];
    }
}
