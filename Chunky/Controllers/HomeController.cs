using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Chunky.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
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

        public IActionResult SubmitChunks()
        {
            try
            {
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
