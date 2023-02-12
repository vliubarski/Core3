using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [EnableCors("AllowAnyOriginPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [Route("UploadFile")]
        [HttpPost()]
        public async Task<ActionResult> UploadFile([FromForm] FileUploadModel fileDetails)
        {
            var result = await _fileService.UploadFile(fileDetails);
            return result is not null
                ? Ok(result)
                : UnprocessableEntity(new { Message = $"Can not add {fileDetails.FormFile.FileName} to data base; possibly it already exists" });
        }

        [Route("FileDetails")]
        [HttpGet()]
        public async Task<ActionResult> FileDetails(string fileName)
        {
            var result = await _fileService.FileDetails(fileName);
            return result is not null
                ? Ok(result)
                : NotFound();
        }

        [Route("FilesDetails")]
        [HttpGet()]
        public async Task<ActionResult> FilesDetails()
        {
            var res = await _fileService.FilesDetails();
            return Ok(res);
        }
    }
}