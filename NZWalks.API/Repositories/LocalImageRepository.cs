using NZWalks.API.Data;
using System.Drawing;
using Image = NZWalks.API.Models.Domain.Image;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly NZWalksDBContext nZWalksDBContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            NZWalksDBContext nZWalksDBContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.HttpContextAccessor = httpContextAccessor;
            this.nZWalksDBContext = nZWalksDBContext;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{image.Filename}{image.FileExtension}");

            //Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //https://localhost:1234/Images/image.jpg
            var urlFilePath = $"{HttpContextAccessor.HttpContext.Request.Scheme}://{HttpContextAccessor.HttpContext.Request.Host}{HttpContextAccessor.HttpContext.Request.PathBase}/Images/{image.Filename}{image.FileExtension}";
            image.FilePath=urlFilePath;

            //Add the images to Image table
            await nZWalksDBContext.Images.AddAsync(image);
            await nZWalksDBContext.SaveChangesAsync();

            return image;
        }
    }
}
