using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly DataContext _context;

        public VendedoresController(DataContext context) {
            _context = context;
        }


        public async Task<IActionResult> ExibirVendedores() {
            return View("~/Views/Vendedores/VendedorAjax.cshtml");

        }
        [HttpGet]
        public async Task<IActionResult> pegarVendedores() {
            if (_context.Vendedores != null) {
                return Json(await _context.Vendedores.ToListAsync());
            }
            return Problem("SGBD esta nullo.");
        }

        [HttpPost]
        public async Task<IActionResult> novoVendedor(Vendedor vendedor) {
            if (ModelState.IsValid) {
                _context.Add(vendedor);
                await _context.SaveChangesAsync();
                return Json(vendedor);
            }
            return Json(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> pegarVendedorID(int vendedorId) {
            Vendedor vendedor = await _context.Vendedores.FindAsync(vendedorId);
            if (vendedor != null)
                return Json(vendedor);
            return Json(new { mensagem = "Vendedor não encontrado" });
        }

        [HttpPost]
        public async Task<IActionResult> atualizarVendedores(Vendedor vendedor) {
            if (ModelState.IsValid) {
                _context.Vendedores.Update(vendedor);
                await _context.SaveChangesAsync();
                return Json(vendedor);
            }

            return Json(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> excluirVendedores(int vendedorId) {
            Vendedor vendedor = await _context.Vendedores.FindAsync(vendedorId);
            if (vendedor != null) {
                _context.Vendedores.Remove(vendedor);
                await _context.SaveChangesAsync();
                return Json("Removido com sucesso");
            }
            return Json(new { mensagem = "Vendedor não encontrado" });
        }
        // GET: Vendedores
        public async Task<IActionResult> Index()
        {
              return _context.Vendedores != null ? 
                          View(await _context.Vendedores.ToListAsync()) :
                          Problem("Entity set 'DataContext.Vendedores'  is null.");
        }

        // GET: Vendedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vendedores == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedores
                .FirstOrDefaultAsync(m => m.VendedorID == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // GET: Vendedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendedorID,VendedorNome,VendedorEmail,VendedorSalario")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendedor);
        }

        // GET: Vendedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vendedores == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedores.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }
            return View(vendedor);
        }

        // POST: Vendedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendedorID,VendedorNome,VendedorEmail,VendedorSalario")] Vendedor vendedor)
        {
            if (id != vendedor.VendedorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendedorExists(vendedor.VendedorID))
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
            return View(vendedor);
        }

        // GET: Vendedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vendedores == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedores
                .FirstOrDefaultAsync(m => m.VendedorID == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // POST: Vendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vendedores == null)
            {
                return Problem("Entity set 'DataContext.Vendedores'  is null.");
            }
            var vendedor = await _context.Vendedores.FindAsync(id);
            if (vendedor != null)
            {
                _context.Vendedores.Remove(vendedor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendedorExists(int id)
        {
          return (_context.Vendedores?.Any(e => e.VendedorID == id)).GetValueOrDefault();
        }
    }
}
