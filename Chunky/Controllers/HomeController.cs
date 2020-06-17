using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;

namespace Chunky.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            ClearUploadDirectory();
            return View();
        }

        private void ClearUploadDirectory()
        {
            var dir = $@"upload";
            if (Directory.Exists(dir))
            {
                Directory.Delete(dir, true);
            }
        }

        [HttpPost]
        public IActionResult UploadChunks()
        {
            try
            {
                var dzuuid = HttpContext.Request.Form["dzuuid"];
                var fileName = HttpContext.Request.Form.Files[0].FileName;
                var dir = $@"upload/{dzuuid}";
                var path = System.IO.Path.Combine(dir, fileName);
                Directory.CreateDirectory(dir);

                using (var stream = HttpContext.Request.Form.Files[0].OpenReadStream())
                {
                    stream.Position = 0;
                    using (var fileStream = System.IO.File.OpenWrite(path))
                    {
                        stream.CopyTo(fileStream);
                    }
                }


                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        public IActionResult SubmitChunks(string fileName, string uuid)
        {
            try
            {
                // do whatever with the file. 
                // send to blob storage and crap.
                // you should clear here not on reload of index.
                return Ok(new {success = true, message = $"{fileName} finished!"});
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
