using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ThesisApplication.Controllers
{
    public class HomeController : Controller
    {
        // METHOD: For getting logged in username
        private string getLoggedInUser()
        {
            return this.User.Identity.Name;
        }

        public IActionResult Index()
        {
            // because View() did not get any parameters which page to display
            // it visualizes _Layout.cshtml
            return View();
        }

        public IActionResult FileUpload()
        {
            // If user is not logged in, do not let him proceed
            if (getLoggedInUser() == null){return RedirectToAction("Login","Account");}

            return RedirectToAction("Index", "userCases");
        }

        public IActionResult MeshingStage()
        {
            // If user is not logged in, do not let him proceed
            if (getLoggedInUser() == null) { return RedirectToAction("Login", "Account"); }

            return RedirectToAction("MeshingIndex", "userCases");
        }

        public IActionResult SimulationStage()
        {
            // If user is not logged in, do not let him proceed
            if (getLoggedInUser() == null) { return RedirectToAction("Login", "Account"); }

            return RedirectToAction("SimulationIndex", "userCases");
        }

        public IActionResult About()
        {
            // If user is not logged in, do not let him proceed
            if (getLoggedInUser() == null) { return RedirectToAction("Login", "Account"); }

            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            // If user is not logged in, do not let him proceed
            if (getLoggedInUser() == null) { return RedirectToAction("Login", "Account"); }

            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            // If user is not logged in, do not let him proceed
            if (getLoggedInUser() == null) { return RedirectToAction("Login", "Account"); }

            return View();
        }
    }
}
