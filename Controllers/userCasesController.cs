using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThesisApplication.Data;
using ThesisApplication.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ThesisApplication.Controllers
{
    public class userCasesController : Controller
    {
        private double maxFileSize = 10 * 1024 * 1024;
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        // Environmental variables for databse and local files
        public userCasesController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }


        // MY METHODS

        
        //string path01 = PlatformServices.Default.Application.ApplicationBasePath;

        // METHOD: For veryfing extension
        public Boolean verfiyExtension(string extension)
        {
            string[] allowedExtensions = { ".stl", ".STL" };
            return allowedExtensions.Contains(extension);
        }

        // METHOD: For getting logged in username
        private string getLoggedInUser()
        {
            return this.User.Identity.Name;
        }


        // POST: Just for uploading files
        [HttpPost]
        public async Task<IActionResult> UploadButton(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            //return View();
            return RedirectToAction("Index", "userCases");
        }


        // POST: For uploading files and inserting querries into database
        [HttpPost]
        public async Task<IActionResult> CreateUploadButton(int id, [Bind("ID,caseName,inputFilename,mesh_expRatio,mesh_finLayerThickness,mesh_minThickness,mesh_numLayers,mesh_refRegDist1,mesh_refRegDist2,mesh_refRegDist3,mesh_refRegLvl1,mesh_refRegLvl2,mesh_refRegLvl3,mesh_refSurfLvlMax,mesh_refSurfLvlMin,meshedDate,status,unitModel,uploadDate,userName,"+
            "visualization_operatingPressure,visualization_operatingtemperature,visualization_flowSpeed,visualization_flowDirectionX,visualization_flowDirectionY,visualization_flowDirectionZ,"+
            "visualization_numProbes,visualization_probe1x,visualization_probe1y,visualization_probe1z,visualization_probe2x,visualization_probe2y,visualization_probe2z,visualization_probe3x,visualization_probe3y,visualization_probe3z,visualization_numCuts,"+
            "visualization_point1x,visualization_point1y,visualization_point1z,visualization_normal1x,visualization_normal1y,visualization_normal1z,"+
            "visualization_point2x,visualization_point2y,visualization_point2z,visualization_normal2x,visualization_normal2y,visualization_normal2z,"+
            "visualization_point3x,visualization_point3y,visualization_point3z,visualization_normal3x,visualization_normal3y,visualization_normal3z")] userCase userCase, ICollection<IFormFile> uploadedFile)
        {
            //Checking for any errors
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (uploadedFile.Count == 0)
                {
                    // Save errore message to "ViewData", dictionary whico offers one way to pass data from controller to View.
                    ViewData["file_errormessage"] = "Please select file!";
                    return View("Create"); // WORKS:
                    //return RedirectToAction("Create", "s"); //looses VIEWDATA
                }
                else if (uploadedFile.First().Length > maxFileSize)
                {
                    // Save errore message to "ViewData", dictionary whico offers one way to pass data from controller to View.
                    ViewData["file_errormessage"] = "File exceeds maximus size allowance of " + maxFileSize + "!";
                    return View("Create");
                }

                foreach (var file in uploadedFile)
                {
                    // Check again if file is approriate size
                    if (file.Length > 0)
                    {
                        // Get name of logged-in user
                        var loggedInUser = getLoggedInUser();
                        
                        // Combine path to specfic file
                        var uploads = Path.Combine(_environment.WebRootPath, "uploads");

                        // File name without extension
                        var pureFileName = Path.GetFileNameWithoutExtension(Path.Combine(uploads, file.FileName));

                        // Check file extension
                        var fileExtension = Path.GetExtension(Path.Combine(uploads, file.FileName));
                        if (verfiyExtension(fileExtension))
                        {
                            // Assemble path to where uploaded file will be saved
                            var savePath = uploads + "\\" + loggedInUser + "\\" + userCase.caseName;

                            // Assemble path to where mesh and visualization directory are to be created
                            var meshVisuPath = uploads + "\\" + loggedInUser + "\\" + userCase.caseName + "\\" + pureFileName; ;

                            // Check if file already exists in user's database
                            foreach (var existingUpload in _context.userCase)
                            {
                                //foreach (var sameUser in existingUpload.userName)
                                //{
                                    if (existingUpload.caseName == userCase.caseName && existingUpload.userName == loggedInUser)
                                    {
                                        // Save errore message to "ViewData", dictionary whico offers one way to pass data from controller to View.
                                        ViewData["case_errormessage"] = "Case name you have entered already exists on list of your experiments!";
                                        return View("Create");
                                    }
                                //}
                            }

                            // Obtain directory info of given path
                            DirectoryInfo dirInfo = new DirectoryInfo(savePath);

                            // Check if directory exists
                            if (!dirInfo.Exists)
                            {
                                dirInfo = Directory.CreateDirectory(savePath);
                                
                                var mesh_path = meshVisuPath + "_mesh";
                                var visualisation_path = meshVisuPath + "_visualisation";
                                Directory.CreateDirectory(mesh_path);
                                Directory.CreateDirectory(visualisation_path);
                                
                                // Create stream with specified save-path and mode at which we operate with file
                                using (var fileStream = new FileStream(Path.Combine(savePath, pureFileName + ".stl"), FileMode.Create))
                                {
                                    userCase.uploadDate = DateTime.Now;
                                    userCase.inputFilename = pureFileName;
                                    userCase.userName = loggedInUser;

                                    // Set status of file to uploaded!
                                    userCase.status = "Uploaded";
          
                                    await file.CopyToAsync(fileStream);
                                }

                                // Add file to _context variable
                                _context.Add(userCase);

                                await _context.SaveChangesAsync();
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                // Save errore message to "ViewData", dictionary whico offers one way to pass data from controller to View.
                                ViewData["file_errormessage"] = "There has been error with directory creation in our database, please hold on for maintainance!";
                                return View("Create");
                            }
                        }
                        else
                        {
                            // Save errore message to "ViewData", dictionary whico offers one way to pass data from controller to View.
                            ViewData["file_errormessage"] = "File extension not appropriate! File has to be of .stl or .STL format";
                            return View("Create"); // WORKS:
                            //return RedirectToAction("Create", "userCases"); //looses VIEWDATA
                        }
                    }
                }
            }
            return RedirectToAction("Index", "userCases");
        }

        // GET: Method for obtaining model for viewing
        public async Task<IActionResult> viewFileUpload(int? id)
        {
            
            var inputfilename = "null";
            var casename = "null";
            // Get name of logged-in user
            var loggedInUser = getLoggedInUser();
            // Combine path to specfic file
            //var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            //var uploads = "http://www.localhost:51515/uploads";
            var uploads = "/uploads";

            // Loop through database and find correspondive ID
            foreach (var existingUpload in _context.userCase)
            {
                // If ID found, obtain name of case and file
                if (existingUpload.ID == id)
                {
                    casename = existingUpload.caseName;
                    inputfilename = existingUpload.inputFilename;
                    if (existingUpload.unitModel == "m")
                    {
                        ViewData["units"] = 1;
                    }
                    
                }
            }

            // If ID was not found, correspondive names remained null
            // If ID was found, names have changed accordingly
            if (inputfilename == "null" && casename == "null")
            {
                // In this case return "null" to ViewData
                ViewData["filePath"] = "null";
            }
            else
            {
                // In this case, save appropriate path to file
                var savePath = uploads + "\\" + loggedInUser + "\\" + casename + "\\" + inputfilename;
                ViewData["filePath"] = savePath;
            }

            // Obtain IQueriable which contains IEnumerable, requested at View page:
            //  by viewuserCase.cshtml header: @model IEnumerable<ThesisApplication.Models.userCase>
            //  ** source https://msdn.microsoft.com/en-us/library/bb534803(v=vs.110).aspx
            var userCase = await _context.userCase.Where(file => file.userName == getLoggedInUser()).ToListAsync();
            // Return IEnumerable collection to this view
            return View(userCase);
        }

        // GET: userCases
        public async Task<IActionResult> Index()
        {
            var userCase = await _context.userCase.Where(file => file.userName == getLoggedInUser()).ToListAsync();
            return View(userCase);
        }

        // GET: Meshing workbench index
        public async Task<IActionResult> MeshingIndex()
        {
            var userCase = await _context.userCase.Where(file => file.userName == getLoggedInUser()).ToListAsync();
            return View(userCase);
        }

        // GET: Create Mesh NOT USED?
        public async Task<IActionResult> CreateMesh(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meshingCase = await _context.userCase.SingleOrDefaultAsync(m => m.ID == id);
            if (meshingCase == null)
            {
                return NotFound();
            }

            return View("MeshingCreate",meshingCase);
            //return View();
        }

      
        // GET: userCases/Edit/5
        public async Task<IActionResult> MeshingParameters(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCase = await _context.userCase.SingleOrDefaultAsync(m => m.ID == id);
            if (userCase == null)
            {
                return NotFound();
            }
            return View(userCase);
        }

        // GET: Default mesh parameters
        public async Task<IActionResult> MeshDefaultParameters()
        {
            var idd = Request.Form["ID"];
            var id = 1;
            if (id == null)
            {
                return NotFound();
            }

            var userCase = await _context.userCase.SingleOrDefaultAsync(m => m.ID == id);
            if (userCase == null)
            {
                return NotFound();
            }
            return View(userCase);
        }

        // POST: For setting default mesh parameters and inserting into database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeshDefaultParameters(int id, [Bind("ID,caseName,inputFilename,mesh_expRatio,mesh_finLayerThickness,mesh_minThickness,mesh_numLayers,mesh_refRegDist1,mesh_refRegDist2,mesh_refRegDist3,mesh_refRegLvl1,mesh_refRegLvl2,mesh_refRegLvl3,mesh_refSurfLvlMax,mesh_refSurfLvlMin,meshedDate,status,unitModel,uploadDate,userName")] userCase userCase)
        {
            if (id != userCase.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    userCase.mesh_expRatio = 0.1F;
                    userCase.mesh_finLayerThickness = 0.005F;
                    userCase.mesh_minThickness = 0.0025F;
                    userCase.mesh_numLayers = 0;
                    userCase.mesh_refRegDist1 = 0.001F;
                    userCase.mesh_refRegLvl1 = 0;
                    userCase.mesh_refRegDist2 = 0.002F;
                    userCase.mesh_refRegLvl2 = 0;
                    userCase.mesh_refRegDist3 = 0.003F;
                    userCase.mesh_refRegLvl3 = 0;
                    userCase.mesh_refSurfLvlMax = 3;
                    userCase.mesh_refSurfLvlMin = 1;

                    _context.Update(userCase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!userCaseExists(userCase.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View("MeshingIndex", userCase);
            }
            return View("MeshingIndex",userCase);
        }


        // POST: For modifying mesh parameters and inserting querries into database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeshingParameters(int id, [Bind("ID,caseName,inputFilename,mesh_expRatio,mesh_finLayerThickness,mesh_minThickness,mesh_numLayers,mesh_refRegDist1,mesh_refRegDist2,mesh_refRegDist3,mesh_refRegLvl1,mesh_refRegLvl2,mesh_refRegLvl3,mesh_refSurfLvlMax,mesh_refSurfLvlMin,meshedDate,status,unitModel,uploadDate,userName,"+
            "visualization_operatingPressure,visualization_operatingtemperature,visualization_flowSpeed,visualization_flowDirectionX,visualization_flowDirectionY,visualization_flowDirectionZ,"+
            "visualization_numProbes,visualization_probe1x,visualization_probe1y,visualization_probe1z,visualization_probe2x,visualization_probe2y,visualization_probe2z,visualization_probe3x,visualization_probe3y,visualization_probe3z,visualization_numCuts,"+
            "visualization_point1x,visualization_point1y,visualization_point1z,visualization_normal1x,visualization_normal1y,visualization_normal1z,"+
            "visualization_point2x,visualization_point2y,visualization_point2z,visualization_normal2x,visualization_normal2y,visualization_normal2z,"+
            "visualization_point3x,visualization_point3y,visualization_point3z,visualization_normal3x,visualization_normal3y,visualization_normal3z")] userCase userCase)
        {
            if (id != userCase.ID)
            {
                return NotFound();
            }

            //Checking for any errors
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!userCaseExists(userCase.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("MeshingIndex");
            }
            ViewData["meshParamError"] = "Please input numbers with smaller precision";
            return View(userCase);
        }

        // GET: Mesh details
        public async Task<IActionResult> MeshingDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCase = await _context.userCase.SingleOrDefaultAsync(m => m.ID == id);
            if (userCase == null)
            {
                return NotFound();
            }
            return View(userCase);
        }

        // GET: Method for obtaining mesh for viewing
        public async Task<IActionResult> viewMesh(int? id)
        {
            /*  Input file name is a full name of file plus its extension
             *
             */ 

            var inputfilename = "null";
            var casename = "null";
            // Get name of logged-in user
            var loggedInUser = getLoggedInUser();
            // Combine path to specfic file
            //var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            //var uploads = "http://www.localhost:51515/uploads";
            var uploads = "/uploads";

            // Loop through database and find correspondive ID
            foreach (var existingUpload in _context.userCase)
            {
                // If ID found, obtain name of case and file
                if (existingUpload.ID == id)
                {
                    casename = existingUpload.caseName;
                    inputfilename = existingUpload.inputFilename;
                }
            }

            // If ID was not found, correspondive names remained null
            // If ID was found, names have changed accordingly
            if (inputfilename == "null" && casename == "null")
            {
                // In this case return "null" to ViewData
                ViewData["filePath"] = "null";
            }
            else
            {
                // In this case, save appropriate path to file
                // FOR NOW: VIEWING STL FILE, THEREFORE PATH IS NOT ENTIRELY CORRECT (the part after the mesh)      //TODO
                var fileNameNoExtension = Path.GetFileNameWithoutExtension(inputfilename);
                var meshPath = uploads + "\\" + loggedInUser + "\\" + casename + "\\" + fileNameNoExtension + "_mesh" + "\\" + fileNameNoExtension;
                ViewData["filePath"] = meshPath;
            }

            // Obtain IQueriable which contains IEnumerable, requested at View page:
            //  by viewuserCase.cshtml header: @model IEnumerable<ThesisApplication.Models.userCase>
            //  ** source https://msdn.microsoft.com/en-us/library/bb534803(v=vs.110).aspx
            var userCase = await _context.userCase.Where(file => file.userName == getLoggedInUser()).ToListAsync();
            // Return IEnumerable collection to this view
            return View(userCase);
        }

        // GET: Simulation workbench index
        public async Task<IActionResult> SimulationIndex()
        {
            var userCase = await _context.userCase.Where(file => file.userName == getLoggedInUser()).ToListAsync();
            return View("SimulationIndex",userCase);
        }

        // GET: Create Simulation
        public async Task<IActionResult> CreateSimulation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simulationCase = await _context.userCase.SingleOrDefaultAsync(m => m.ID == id);
            if (simulationCase == null)
            {
                return NotFound();
            }

            return View("SimulationCreate", simulationCase);
            //return View();
        }

        // POST: For modifying mesh parameters and inserting querries into database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSimulation(int id, [Bind("ID,caseName,inputFilename,mesh_expRatio,mesh_finLayerThickness,mesh_minThickness,mesh_numLayers,mesh_refRegDist1,mesh_refRegDist2,mesh_refRegDist3,mesh_refRegLvl1,mesh_refRegLvl2,mesh_refRegLvl3,mesh_refSurfLvlMax,mesh_refSurfLvlMin,meshedDate,status,unitModel,uploadDate,userName,"+
            "visualization_operatingPressure,visualization_operatingtemperature,visualization_flowSpeed,visualization_flowDirectionX,visualization_flowDirectionY,visualization_flowDirectionZ,"+
            "visualization_numProbes,visualization_probe1x,visualization_probe1y,visualization_probe1z,visualization_probe2x,visualization_probe2y,visualization_probe2z,visualization_probe3x,visualization_probe3y,visualization_probe3z,visualization_numCuts,"+
            "visualization_point1x,visualization_point1y,visualization_point1z,visualization_normal1x,visualization_normal1y,visualization_normal1z,"+
            "visualization_point2x,visualization_point2y,visualization_point2z,visualization_normal2x,visualization_normal2y,visualization_normal2z,"+
            "visualization_point3x,visualization_point3y,visualization_point3z,visualization_normal3x,visualization_normal3y,visualization_normal3z")] userCase userCase)
        {
            if (id != userCase.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!userCaseExists(userCase.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("SimulationIndex");
            }
            return View("SimulationCreate",userCase);
        }

        // GET: View simulation results
        public async Task<IActionResult> viewSimulationResults(int? id)
        {
            /*  Input file name is a full name of file plus its extension
             *
             */

            var inputfilename = "null";
            var casename = "null";
            // Get name of logged-in user
            var loggedInUser = getLoggedInUser();
            // Combine path to specfic file
            //var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            //var uploads = "http://www.localhost:51515/uploads";
            var uploads = "/uploads";

            // Loop through database and find correspondive ID
            foreach (var existingUpload in _context.userCase)
            {
                // If ID found, obtain name of case and file
                if (existingUpload.ID == id)
                {
                    casename = existingUpload.caseName;
                    inputfilename = existingUpload.inputFilename;
                }
            }

            // If ID was not found, correspondive names remained null
            // If ID was found, names have changed accordingly
            if (inputfilename == "null" && casename == "null")
            {
                // In this case return "null" to ViewData
                ViewData["filePath"] = "null";
            }
            else
            {
                // In this case, save appropriate path to file
                // FOR NOW: VIEWING STL FILE, THEREFORE PATH IS NOT ENTIRELY CORRECT (the part after the mesh)      //TODO
                var fileNameNoExtension = Path.GetFileNameWithoutExtension(inputfilename);
                var visualisationPath = uploads + "\\" + loggedInUser + "\\" + casename + "\\" + fileNameNoExtension + "_visualisation" + "\\";
                ViewData["filePath"] = visualisationPath;
            }

            if (id == null)
            {
                return NotFound();
            }

            var userCase = await _context.userCase.SingleOrDefaultAsync(m => m.ID == id);
            if (userCase == null)
            {
                return NotFound();
            }
            return View(userCase);
        }

        // END OF MY METHODS

        /*// GET: userCases
        public async Task<IActionResult> Index()
        {
            return View(await _context.userCase.ToListAsync());
        }*/

        // GET: userCases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCase = await _context.userCase.SingleOrDefaultAsync(m => m.ID == id);
            if (userCase == null)
            {
                return NotFound();
            }

            return View(userCase);
        }

        // GET: userCases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: userCases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,caseName,inputFilename,mesh_expRatio,mesh_finLayerThickness,mesh_minThickness,mesh_numLayers,mesh_refRegDist1,mesh_refRegDist2,mesh_refRegDist3,mesh_refRegLvl1,mesh_refRegLvl2,mesh_refRegLvl3,mesh_refSurfLvlMax,mesh_refSurfLvlMin,meshedDate,status,unitModel,uploadDate,userName")] userCase userCase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userCase);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userCase);
        }

        // GET: userCases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCase = await _context.userCase.SingleOrDefaultAsync(m => m.ID == id);
            if (userCase == null)
            {
                return NotFound();
            }
            return View(userCase);
        }

        // POST: userCases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,caseName,inputFilename,mesh_expRatio,mesh_finLayerThickness,mesh_minThickness,mesh_numLayers,mesh_refRegDist1,mesh_refRegDist2,mesh_refRegDist3,mesh_refRegLvl1,mesh_refRegLvl2,mesh_refRegLvl3,mesh_refSurfLvlMax,mesh_refSurfLvlMin,meshedDate,status,unitModel,uploadDate,userName")] userCase userCase)
        {
            if (id != userCase.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!userCaseExists(userCase.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(userCase);
        }

        // GET: userCases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCase = await _context.userCase.SingleOrDefaultAsync(m => m.ID == id);
            if (userCase == null)
            {
                return NotFound();
            }

            return View(userCase);
        }

        // POST: userCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userCase = await _context.userCase.SingleOrDefaultAsync(m => m.ID == id);
            _context.userCase.Remove(userCase);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool userCaseExists(int id)
        {
            return _context.userCase.Any(e => e.ID == id);
        }
    }
}
