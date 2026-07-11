using SciFiPortfolio.Models;

namespace SciFiPortfolio.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<List<ProjectCard>> GetProjectCards();
    }
}
