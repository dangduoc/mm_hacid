using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace BaseProjectWebRazor.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly ILocalUploadService handler;
        public FileUploadController(ILocalUploadService _handler)
        {
            handler = _handler;
        }
        [HttpGet]
        public IActionResult Get(string folder)
        {
            var response = handler.GetImagesFromFolder(folder);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> UploadImages(string folder)
        {
            if (Request.Form.Files.Count > 0)
                return Ok(await handler.UploadImages(Request.Form.Files.ToList(), folder,null,null));
            return BadRequest();
        }


        [HttpPost("editor")]
        public async Task<IActionResult> UploadEditor(string folder)
        {
            var fileToUpload = Request.Form.Files[0];

            return Ok(await handler.UploadForCkEditor(fileToUpload, folder, HttpContext.Request.Host.Value.ToString()));
        }

        [HttpDelete]
        public IActionResult Delete(string filePath)
        {
            handler.DeleteFile(filePath);
            return Ok();
        }
    }
}
