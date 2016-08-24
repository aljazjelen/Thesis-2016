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
using Microsoft.AspNetCore.Http;
using System.IO;
using ThesisApplication.Controllers;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;


using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.PlatformAbstractions;

namespace ThesisApplication.Controllers
{
    public class fileUploadsController : Controller
    {
        private double maxFileSize = 10 * 1024 * 1024;
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;
  
        // Environmental variables for databse and local files
        public fileUploadsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        string path01 = PlatformServices.Default.Application.ApplicationBasePath;

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
            return RedirectToAction("Index", "fileUploads");
        }


        // POST: For uploading files and inserting querries into database
        [HttpPost]
        public async Task<IActionResult> CreateUploadButton([Bind("ID,userName,caseName,unitModel")] fileUpload fileUpload, ICollection<IFormFile> uploadedFile)
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
                    //return RedirectToAction("Create", "fileUploads"); //looses VIEWDATA
                }else if (uploadedFile.First().Length > maxFileSize)
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
                            var savePath = uploads + "\\" + loggedInUser + "\\" + fileUpload.caseName;

                            // Assemble path to where mesh and visualization directory are to be created
                            var meshVisuPath = uploads + "\\" + loggedInUser + "\\" + fileUpload.caseName + "\\" + pureFileName; ;

                            // Check if file already exists in user's database
                            foreach (var existingUpload in _context.fileUpload)
                            {
                                foreach (var sameUser in existingUpload.userName)
                                {
                                    if (existingUpload.caseName == fileUpload.caseName)
                                    {
                                        // Save errore message to "ViewData", dictionary whico offers one way to pass data from controller to View.
                                        ViewData["case_errormessage"] = "Case name you have entered already exists on list of your experiments!";
                                        return View("Create");
                                    }
                                }
                            }

                            // Obtain directory info of given path
                            DirectoryInfo dirInfo = new DirectoryInfo(savePath);

                            // Check if directory exists
                            if (!dirInfo.Exists)
                            {
                                dirInfo = Directory.CreateDirectory(savePath);
                                // Create stream with specified save-path and mode at which we operate with file
                                using (var fileStream = new FileStream(Path.Combine(savePath, file.FileName), FileMode.Create))
                                {
                                    fileUpload.uploadDate = DateTime.Now;
                                    fileUpload.inputFilename = file.FileName;
                                    fileUpload.userName = loggedInUser;
                                    fileUpload.status = "Uploaded";
                                    await file.CopyToAsync(fileStream);
                                }

                                // Meshing stage needs to be initialized as well
                                //  Create new meshingStage in database and assign
                                //  status to "initialized"
                                meshingStage newMesh = new meshingStage();
                                newMesh.caseName = fileUpload.caseName;
                                newMesh.ID = fileUpload.ID;
                                newMesh.inputFilename = fileUpload.inputFilename;
                                newMesh.uploadDate = fileUpload.uploadDate;
                                newMesh.userName = fileUpload.userName;
                                newMesh.status = "initialized";

                                // Add file to _context variable
                                _context.Add(fileUpload);
                                _context.Add(newMesh);
                                await _context.SaveChangesAsync();
                                // Set status of file to uploaded!
                                

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
                            //return RedirectToAction("Create", "fileUploads"); //looses VIEWDATA
                        }
                    }
                }
            }
            return RedirectToAction("Index", "fileUploads");
        }

        // GET: Method for obtaining files
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
            foreach (var existingUpload in _context.fileUpload)
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
                var savePath = uploads + "\\" + loggedInUser + "\\" + casename + "\\" + inputfilename;
                ViewData["filePath"] = savePath;
            }

            // Obtain IQueriable which contains IEnumerable, requested at View page:
            //  by viewFileUpload.cshtml header: @model IEnumerable<ThesisApplication.Models.fileUpload>
            //  ** source https://msdn.microsoft.com/en-us/library/bb534803(v=vs.110).aspx
            var fileUpload = await _context.fileUpload.Where(file => file.userName == getLoggedInUser()).ToListAsync();
            // Return IEnumerable collection to this view
            return View(fileUpload);
        }



        /* METHOD: Async fileViewer
         * This method is not used since "Where"(for querying) keyword doesnt work asynchonically
        public async Task<IActionResult> viewFileUpload(int? id)
        {
            var inputfilename = "";
            var casename = "";

            if (id == null)
            {
                var existing_upload = _context.fileUpload;
                var first_ele = existing_upload.First();
                id = first_ele.ID;
            }
            // Get name of logged-in user
            var loggedInUser = getLoggedInUser();
            // Combine path to specfic file
            //var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            //var uploads = "http://www.localhost:51515/uploads";
            var uploads = "/uploads";

            foreach (var existingUpload in _context.fileUpload)
            {
                if (existingUpload.ID == id)
                {
                    casename = existingUpload.caseName;
                    inputfilename = existingUpload.inputFilename;
                }
            }

            var savePath = uploads + "\\" + loggedInUser + "\\" + casename + "\\" + inputfilename;
            ViewData["filePath"] = savePath;

            return await Task.Run(() =>
            {
                if (Session[userName] == null)
                {
                    Account account = db.accounts.Where(a => a.userName.Equals(userName)).FirstOrDefault();
                    if (account == null)
                    {
                        //log out
                        return null;
                    }
                    Session[userName] = account;
                }
                return Session[userName] as Account;
            });
            var fileUpload = await _context.fileUpload.w WhereAsync(file => file.userName == getLoggedInUser());
            //var fileUpload = await _context.fileUpload.SingleOrDefaultAsync(m => m.ID == id);

            return View(fileUpload);
            //return View(await _context.fileUpload.ToListAsync());
        }
        */




        // END OF CUSTOM FUNCTIONS

        // GET: fileUploads
        public async Task<IActionResult> Index()
        {
            var fileUpload = await _context.fileUpload.Where(file => file.userName == getLoggedInUser()).ToListAsync();
            // Return IEnumerable collection to this view
            return View(fileUpload);
            //return View(await _context.fileUpload.ToListAsync());
        }

        // GET: fileUploads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //var fileUpload = await _context.fileUpload.
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.fileUpload.SingleOrDefaultAsync(m => m.ID == id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            return View(fileUpload);
        }

        // GET: fileUploads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: fileUploads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,userName")] fileUpload fileUpload)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileUpload);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fileUpload);
        }

        // GET: fileUploads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.fileUpload.SingleOrDefaultAsync(m => m.ID == id);
            if (fileUpload == null)
            {
                return NotFound();
            }
            return View(fileUpload);
        }

        // POST: fileUploads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,userName")] fileUpload fileUpload)
        {
            if (id != fileUpload.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileUpload);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!fileUploadExists(fileUpload.ID))
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
            return View(fileUpload);
        }

        // GET: fileUploads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.fileUpload.SingleOrDefaultAsync(m => m.ID == id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            return View(fileUpload);
        }

        // POST: fileUploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var fileUpload = await _context.fileUpload.SingleOrDefaultAsync(m => m.ID == id);

            var fileUpload_case = fileUpload.caseName;
            var fileUpload_filename = fileUpload.inputFilename;


            var meshingStage = await _context.meshingStage.Where(file => file.inputFilename == fileUpload_filename && file.caseName == fileUpload_case).SingleOrDefaultAsync();

            if (meshingStage.caseName != "" && meshingStage.inputFilename != "" && meshingStage.userName != "")
            {
                // MESH WITH THAT NAME EXISTS, delete that too
                _context.meshingStage.Remove(meshingStage);
            }

            _context.fileUpload.Remove(fileUpload);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool fileUploadExists(int id)
        {
            return _context.fileUpload.Any(e => e.ID == id);
        }
    }
}
