using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages
{
    public class IndexModel : PageModel
    {
        public IndexViewModel Vm { get; set; } = new();

        private readonly IProjectService _projectService;
        private readonly IImageService _imageService;
        private readonly IContentSectionService _contentSectionService;


        public IndexModel(IProjectService projectService, IImageService imageService, IContentSectionService contentSectionService)
        {
            _contentSectionService = contentSectionService;
            _imageService = imageService;
            _projectService = projectService;
        }

        public async Task OnGet()
        {
            ViewData["Title"] = "Fredrik Bengtsson | .NET Developer Portfolio";

            Vm.ProjectCards = await _projectService.GetProjectCards();
            Vm.ContentSections = await _contentSectionService.GetContentSections("index");
            Vm.TechIconsSlider = await _imageService.GetTechIcons();


          
        }
    }
}
