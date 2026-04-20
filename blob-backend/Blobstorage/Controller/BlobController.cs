using Blobstorage.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blob.Controllers
{
    /*
     ============================================================
     API Controller for accessing files stored in Blob Storage

     Endpoints:
     - GET /api/handbooks
         Returns a list of available files

     - GET /api/handbooks/{fileName}
         Downloads a specific file
     ============================================================
    */
    [ApiController]
    [Route("api/[controller]")]
    public class HandbooksController : ControllerBase
    {
        private readonly IBlobservice _blobService;

        public HandbooksController(IBlobservice blobService)
        {
            _blobService = blobService;
        }

        // Returns metadata for all files (uses DTO)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var files = await _blobService.GetAllFilesAsync();
            return Ok(files);
        }

        // Downloads a file from Blob Storage by file name
        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetFile(string fileName)
        {
            var file = await _blobService.GetFileAsync(fileName);

            return File(file.Content, file.ContentType, file.FileName);
        }
    }
}