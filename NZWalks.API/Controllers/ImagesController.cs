using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Drawing;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);
            if (ModelState.IsValid)
            {
                //convert DTO to Domain Model
                var imageDomainModel = new Models.Domain.Image
                {
                    File = imageUploadRequestDto.File,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileSizeInBytes = imageUploadRequestDto.File.Length,
                    Filename = imageUploadRequestDto.FileName,
                    FileDescription = imageUploadRequestDto.FileDescription
                };


                //user repository to upload image
                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedextensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedextensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File Size more than 10MB, Please upload a smaler file size");
            }
        }
    }
}
