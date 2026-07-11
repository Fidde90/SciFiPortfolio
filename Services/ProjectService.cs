using SciFiPortfolio.Interfaces.Repositories;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models;

namespace SciFiPortfolio.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepo)
        {
            _projectRepository = projectRepo;
        }

        public async Task<List<ProjectCard>> GetProjectCards()
        {
            return await _projectRepository.GetProjectCards();
        }
    }
}
