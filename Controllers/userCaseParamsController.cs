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
    public class userCaseParamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public userCaseParamsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: userCaseParams
        public async Task<IActionResult> Index()
        {
            return View("Index");
            //return View(await _context.userCaseParam.ToListAsync());
        }

        // GET: test site
        public IActionResult testSite()
        {
            return View("Index2");
        }

        // GET: userCaseParams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCaseParam = await _context.userCaseParam.SingleOrDefaultAsync(m => m.ID == id);
            if (userCaseParam == null)
            {
                return NotFound();
            }

            return View(userCaseParam);
        }

        // GET: userCaseParams/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: userCaseParams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Genre,Price,caseDirectory,caseName,metricConversion,modelName,uploadDate,userName")] userCaseParam userCaseParam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userCaseParam);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userCaseParam);
        }

        // GET: userCaseParams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCaseParam = await _context.userCaseParam.SingleOrDefaultAsync(m => m.ID == id);
            if (userCaseParam == null)
            {
                return NotFound();
            }
            return View(userCaseParam);
        }

        // POST: userCaseParams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Genre,Price,caseDirectory,caseName,metricConversion,modelName,uploadDate,userName")] userCaseParam userCaseParam)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (id != userCaseParam.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCaseParam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!userCaseParamExists(userCaseParam.ID))
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
            return View(userCaseParam);
        }

        // GET: userCaseParams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCaseParam = await _context.userCaseParam.SingleOrDefaultAsync(m => m.ID == id);
            if (userCaseParam == null)
            {
                return NotFound();
            }

            return View(userCaseParam);
        }

        // POST: userCaseParams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userCaseParam = await _context.userCaseParam.SingleOrDefaultAsync(m => m.ID == id);
            _context.userCaseParam.Remove(userCaseParam);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool userCaseParamExists(int id)
        {
            return _context.userCaseParam.Any(e => e.ID == id);
        }
    }
}
