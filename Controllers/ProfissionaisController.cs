#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Unionmongo.Data;
using Unionmongo.Models;

namespace Unionmongo.Controllers
{
    public class ProfissionaisController : Controller
    {
        
        // GET: Profissionais
        public async Task<IActionResult> Index()
        {
            ContextoMongodb dbContext = new ContextoMongodb();
            return View(await dbContext.Profissional.Find(c=> true).ToListAsync());
        }

        // GET: Profissional/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContextoMongodb dbContext = new ContextoMongodb();
            var profissional = await dbContext.Profissional.Find(c => c.Id == id).FirstOrDefaultAsync();
                
            if (profissional == null)
            {
                return NotFound();
            }

            return View(profissional);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone")] Profissional profissional)
        {
            if (ModelState.IsValid)
            {
                ContextoMongodb dbContext = new ContextoMongodb();
                profissional.Id = Guid.NewGuid();
                await dbContext.Profissional.InsertOneAsync(profissional);
                return RedirectToAction(nameof(Index));
            }
            return View(profissional);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContextoMongodb dbContext = new ContextoMongodb();
            var profissional = await dbContext.Profissional.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (profissional == null)
            {
                return NotFound();
            }
            return View(profissional);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Telefone")] Profissional profissional)
        {
            if (id != profissional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ContextoMongodb dbContext = new ContextoMongodb();
                    await dbContext.Profissional.ReplaceOneAsync(m=> m.Id == profissional.Id, profissional);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfissionalExists(profissional.Id))
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
            return View(profissional);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContextoMongodb dbContext = new ContextoMongodb();
            var profissional = await dbContext.Profissional.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (profissional == null)
            {
                return NotFound();
            }

            return View(profissional);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            ContextoMongodb dbContext = new ContextoMongodb();
            await dbContext.Profissional.DeleteOneAsync(c => c.Id == id);

            return RedirectToAction(nameof(Index));
        }

        private bool ProfissionalExists(Guid id)
        {
            ContextoMongodb dbContext = new ContextoMongodb();
            var profissional = dbContext.Profissional.Find(c => c.Id == id).Any();
            return profissional;
        }
    }
}
