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
    public class ClientesController : Controller
    {
        
        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            ContextoMongodb dbContext = new ContextoMongodb();
            return View(await dbContext.Cliente.Find(c=> true).ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContextoMongodb dbContext = new ContextoMongodb();
            var cliente = await dbContext.Cliente.Find(c => c.Id == id).FirstOrDefaultAsync();
                
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
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
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone,Email")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                ContextoMongodb dbContext = new ContextoMongodb();
                cliente.Id = Guid.NewGuid();
                await dbContext.Cliente.InsertOneAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContextoMongodb dbContext = new ContextoMongodb();
            var cliente = await dbContext.Cliente.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Telefone,Email")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ContextoMongodb dbContext = new ContextoMongodb();
                    await dbContext.Cliente.ReplaceOneAsync(m=> m.Id == cliente.Id, cliente);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContextoMongodb dbContext = new ContextoMongodb();
            var cliente = await dbContext.Cliente.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            ContextoMongodb dbContext = new ContextoMongodb();
            await dbContext.Cliente.DeleteOneAsync(c => c.Id == id);

            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id)
        {
            ContextoMongodb dbContext = new ContextoMongodb();
            var cliente = dbContext.Cliente.Find(c => c.Id == id).Any();
            return cliente;
        }
    }
}
