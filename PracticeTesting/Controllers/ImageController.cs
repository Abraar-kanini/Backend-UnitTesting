using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticeTesting.Data;
using PracticeTesting.DTO;
using PracticeTesting.Models;
using System.Numerics;

namespace PracticeTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly EmployeeDbContext employeeDbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ImageController(EmployeeDbContext employeeDbContext,IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor)
        {
            this.employeeDbContext = employeeDbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> ImageUpload(ImageUploadDto imageUploadDto)
        {
            ValidateFileUpload(imageUploadDto);
            if (!ModelState.IsValid) {

                return BadRequest(ModelState);
            }

            var image = new Image()
            {
                File=imageUploadDto.File,
                FileExtention=Path.GetExtension(imageUploadDto.File.FileName),
                FileSizeInBytes=imageUploadDto.File.Length,
                fileName=imageUploadDto.fileName
            };

            var localimagepath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.fileName}{image.FileExtention}");
            using var stream = new FileStream(localimagepath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            var urlfilepath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.fileName}{image.FileExtention}";
            image.FilePath = urlfilepath;

            await employeeDbContext.images.AddAsync(image);
            await employeeDbContext.SaveChangesAsync();
            return Ok();
        }
        private void ValidateFileUpload(ImageUploadDto imageUploadDto) {

            var extention = new string[] { ".jpg", ".jpeg", ".png" };
            if (!extention.Contains(Path.GetExtension(imageUploadDto.File.FileName)))
            {
                ModelState.AddModelError("file", "unsupported file extension");
            }

            if(imageUploadDto.File.Length> 10485760)
            {
                ModelState.AddModelError("file", "the file size is more than 10 mb");
            }
        }

    }
}
