using System.Drawing;
using NZWalks.API.Models.Domain;
using Image = NZWalks.API.Models.Domain.Image;

namespace NZWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image imageDomainModel);
    }
}
