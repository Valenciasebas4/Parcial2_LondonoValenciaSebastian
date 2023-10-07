﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial2_LondonoValenciaSebastian.DAL;
using Parcial2_LondonoValenciaSebastian.DAL.Entities;

namespace Parcial2_LondonoValenciaSebastian.Controllers
{
    public class NaturalPersonsController : Controller
    {
        private readonly DataBaseContext _context;

        public NaturalPersonsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: NaturalPersons
        public async Task<IActionResult> Index()
        {
              return _context.NaturalsPersons != null ? 
                          View(await _context.NaturalsPersons.ToListAsync()) :
                          Problem("Entity set 'DataBaseContext.NaturalsPersons'  is null.");
        }

        // GET: NaturalPersons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.NaturalsPersons == null)
            {
                return NotFound();
            }

            var naturalPerson = await _context.NaturalsPersons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (naturalPerson == null)
            {
                return NotFound();
            }

            return View(naturalPerson);
        }

        // GET: NaturalPersons/Create
        public IActionResult Create()
        {
            return View();
        }

        private int CalculatAge(int year)
        {
            DateTime fechaActual = DateTime.Today;

            int age = fechaActual.Year - year;

            return age;
        }
        // POST: NaturalPersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( NaturalPerson naturalPerson)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    naturalPerson.Id = Guid.NewGuid();
                    naturalPerson.CreatedDate = DateTime.Now;
                    naturalPerson.Age = CalculatAge(naturalPerson.BirthYear);
                    _context.Add(naturalPerson);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                        ModelState.AddModelError(string.Empty, "Ya existe una persona con el mismo documento");
                    else
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            
            return View(naturalPerson);
        }

        // GET: NaturalPersons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.NaturalsPersons == null)
            {
                return NotFound();
            }

            var naturalPerson = await _context.NaturalsPersons.FindAsync(id);
            if (naturalPerson == null)
            {
                return NotFound();
            }
            return View(naturalPerson);
        }

        // POST: NaturalPersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,NaturalPerson naturalPerson)
        {
            if (id != naturalPerson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    naturalPerson.Age = CalcularEdad(naturalPerson.BirthYear);
                    naturalPerson.ModifiedDate= DateTime.Now;
                    _context.Update(naturalPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NaturalPersonExists(naturalPerson.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(naturalPerson);
        }

        // GET: NaturalPersons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.NaturalsPersons == null)
            {
                return NotFound();
            }

            var naturalPerson = await _context.NaturalsPersons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (naturalPerson == null)
            {
                return NotFound();
            }

            return View(naturalPerson);
        }

        // POST: NaturalPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.NaturalsPersons == null)
            {
                return Problem("Entity set 'DataBaseContext.NaturalsPersons'  is null.");
            }
            var naturalPerson = await _context.NaturalsPersons.FindAsync(id);
            if (naturalPerson != null)
            {
                _context.NaturalsPersons.Remove(naturalPerson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NaturalPersonExists(Guid id)
        {
          return (_context.NaturalsPersons?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
