using GymAndYou.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace GymAndYou.Controllers
{
    [Route("/api/files")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _service;

        public FileController(IFileService fileService)
        {
            _service = fileService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string fileName)
        {
            var fileResoult = _service.GetFile(fileName);
            return File(fileResoult.fileContents,fileResoult.contentType,fileResoult.fileName);
        }

        [HttpPost]
        public IActionResult UploadFile([FromForm] IFormFile file)
        {
            var fileName = _service.UploadFile(file);
            return Created(fileName,null);
        }
    }
}
