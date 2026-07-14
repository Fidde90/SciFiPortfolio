using SciFiPortfolio.Interfaces.Repositories;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Services
{
    public class ContentSectionService : IContentSectionService
    {
        public readonly IContentSectionRepository _contentSectionRepository;

        public ContentSectionService(IContentSectionRepository contentSectionRepo)
        {
            _contentSectionRepository = contentSectionRepo;
        }

        public async Task<List<ContentSection>> GetContentSections(string page)
        {
            return await _contentSectionRepository.GetAllSections(page);
        }
    }
}
