using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRespository imageRespository;
        public ImagesController(IImageRespository imageRespository)
        {
            this.imageRespository = imageRespository; 
        }
        [HttpPost]
        [Route("images")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid) {
                //convert imdto to model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };
                await imageRespository.Upload(imageDomainModel);
                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);

        }
        private void ValidateFileUpload(ImageUploadRequestDto request) {
            var allowed = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowed.Contains(Path.GetExtension(request.File.FileName))) {
                ModelState.TryAddModelError("file", "Unsupported file extension");
            }
            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Fize size too big");
            }
        }
    }
}
