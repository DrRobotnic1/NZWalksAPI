using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository
{
    public interface IImageRespository
    {
        Task<Image> Upload(Image image);
    }
}
