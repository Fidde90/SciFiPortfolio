using SciFiPortfolio.Models;

namespace SciFiPortfolio.Interfaces.Services
{
    public interface IProjectService
    {
        Task<List<ProjectCard>> GetProjectCards();
    }
}
