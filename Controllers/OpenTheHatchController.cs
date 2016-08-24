using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ThesisApplication.Controllers
{
    public class OpenTheHatchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FileUpload()
        {
            //return Redirect("~/Views/fileUploads/Create.cshtml");
            return RedirectToAction("Index","fileUploads");
            //return View("~/Views/fileUploads/Create.cshtml");
        }
    }
}