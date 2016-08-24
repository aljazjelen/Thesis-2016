using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThesisApplication.Data;
using ThesisApplication.Models;

namespace ThesisApplication.Controllers
{
    public class meshingStagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public meshingStagesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // METHOD: For getting logged in username
        private string getLoggedInUser()
        {
            return this.User.Identity.Name;
        }

        // GET: meshingStages
        public async Task<IActionResult> Index()
        {
            var fileUploads = await _context.fileUpload.Where(file => file.userName == getLoggedInUser()).ToListAsync();
            
            var meshingStage = await _context.meshingStage.Where(file => file.userName == getLoggedInUser()).ToListAsync();
            // Return IEnumerable collection to this view
            return View(meshingStage);
            //return View(await _context.fileUpload.ToListAsync());
        }

        // GET: Method for obtaining queried model and returning it into view for creating new mesh paramaters
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var meshingStage = await _context.meshingStage.SingleOrDefaultAsync(m => m.ID == id);
            if (meshingStage == null)
            {
                return NotFound();
            }

            return View(meshingStage);
            //return View();
        }

        // POST: meshingStages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,caseName,expRatio,finLayerThickness,inputFilename,minThickness,numLayers,refRegDist1,refRegDist2,refRegDist3,refRegLvl1,refRegLvl2,refRegLvl3,refSurfLvlMax,refSurfLvlMin,status,unitModel,uploadDate,userName")] meshingStage meshingStage)
        {
            //Checking for any errors
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meshingStage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!meshingStageExists(meshingStage.ID))
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
            return View(meshingStage);
        }

        // POST: meshingStages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,caseName,expRatio,finLayerThickness,inputFilename,minThickness,numLayers,refRegDist1,refRegDist2,refRegDist3,refRegLvl1,refRegLvl2,refRegLvl3,refSurfLvlMax,refSurfLvlMin,status,unitModel,uploadDate,userName")] meshingStage meshingStage)
        {
            //Checking for any errors
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            if (id != meshingStage.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meshingStage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!meshingStageExists(meshingStage.ID))
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
            return View(meshingStage);
        }



        /*  END OF MY METHODS   */


        /*// GET: meshingStages
        public async Task<IActionResult> Index()
        {
            return View(await _context.meshingStage.ToListAsync());
        }
        */

        // GET: meshingStages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meshingStage = await _context.meshingStage.SingleOrDefaultAsync(m => m.ID == id);
            if (meshingStage == null)
            {
                return NotFound();
            }

            return View(meshingStage);
        }

        /*// GET: meshingStages/Create
        public IActionResult Create(int? id)
        {
            return View();
        }*/

        /*// POST: meshingStages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,caseName,expRatio,finLayerThickness,inputFilename,minThickness,numLayers,refRegDist1,refRegDist2,refRegDist3,refRegLvl1,refRegLvl2,refRegLvl3,refSurfLvlMax,refSurfLvlMin,status,unitModel,uploadDate,userName")] meshingStage meshingStage, int id)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meshingStage);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(meshingStage);
        }*/

        // GET: meshingStages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meshingStage = await _context.meshingStage.SingleOrDefaultAsync(m => m.ID == id);
            if (meshingStage == null)
            {
                return NotFound();
            }
            return View(meshingStage);
        }

        /*// POST: meshingStages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("expRatio,finLayerThickness,minThickness,numLayers,refRegDist1,refRegDist2,refRegDist3,refRegLvl1,refRegLvl2,refRegLvl3,refSurfLvlMax,refSurfLvlMin")] meshingStage meshingStage)
        {
            if (id != meshingStage.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meshingStage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!meshingStageExists(meshingStage.ID))
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
            return View(meshingStage);
        }*/

        // GET: meshingStages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meshingStage = await _context.meshingStage.SingleOrDefaultAsync(m => m.ID == id);
            if (meshingStage == null)
            {
                return NotFound();
            }

            return View(meshingStage);
        }

        // POST: meshingStages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meshingStage = await _context.meshingStage.SingleOrDefaultAsync(m => m.ID == id);
            _context.meshingStage.Remove(meshingStage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool meshingStageExists(int id)
        {
            return _context.meshingStage.Any(e => e.ID == id);
        }
    }
}
