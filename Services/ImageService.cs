using SciFiPortfolio.Interfaces.Repositories;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Models;

namespace SciFiPortfolio.Services
{
    public class ImageService : IImageService
    {
        public readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepo)
        {
            _imageRepository = imageRepo;
        }

        public async Task<List<Image>> GetTechIcons()
        {
            return await _imageRepository.GetTechImages();
        }
    }
}
