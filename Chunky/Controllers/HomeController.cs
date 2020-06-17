using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Chunky.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UploadChunks()
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
