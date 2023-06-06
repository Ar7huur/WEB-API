using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Models;

namespace WebAPI.Controllers {
    public class VendasController : Controller {
        private readonly DataContext _context;

        public VendasController(DataContext context) {
            _context = context;
        }
        public async Task<IActionResult> ExibeVendas() {
            return View("~/Views/Vendas/VendaAjax.cshtml");
        }

        [HttpGet]//get Vendas
        public async Task<IActionResult> pegarVendas() {
            if (_context.Vendas != null) {
                return Json(await _context.Vendas.ToListAsync());
            }
            return Problem("Erro com o BD de departamentos");
        }

        [HttpPost]//nova venda
        public async Task<IActionResult> novaVenda(Venda venda) {
            if (ModelState.IsValid) {
                _context.Add(venda);
                await _context.SaveChangesAsync();
                return Json(venda);
            }
            return Json(ModelState);
        }

        [HttpGet]//pega venda pelo id
        public async Task<IActionResult> pegarVendaPeloID(int vendaId) {
            Venda venda = await _context.Vendas.FindAsync(vendaId);
            if (vendaId != null)
                return Json(venda);
            return Json(new { mensagem = "Venda nao encontrada no SGBD" });
        }
        [HttpPost] //att venda
        public async Task<IActionResult> atualizarVenda(Venda venda) {
            if (ModelState.IsValid) {
                _context.Vendas.Update(venda);
                await _context.SaveChangesAsync();
                return Json(venda);
            }
            return Json(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> deletarVenda(int vendaId) {
            Venda venda = await _context.Vendas.FindAsync(vendaId);
            if (venda != null) {
                _context.Vendas.Remove(venda);
                await _context.SaveChangesAsync();
                return Json("Deletado");
            }
            return Json(new { mensagem = "Venda nao encontrada" });
        }


        // GET: Vendas
        public async Task<IActionResult> Index() {
            return _context.Vendas != null ?
                        View(await _context.Vendas.ToListAsync()) :
                        Problem("Entity set 'DataContext.Vendas'  is null.");
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Vendas == null) {
                return NotFound();
            }

            var venda = await _context.Vendas
                .FirstOrDefaultAsync(m => m.VendaID == id);
            if (venda == null) {
                return NotFound();
            }

            return View(venda);
        }

        // GET: Vendas/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendaID,VendaData,VendaValor")] Venda venda) {
            if (ModelState.IsValid) {
                _context.Add(venda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Vendas == null) {
                return NotFound();
            }

            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null) {
                return NotFound();
            }
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendaID,VendaData,VendaValor")] Venda venda) {
            if (id != venda.VendaID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!VendaExists(venda.VendaID)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Vendas == null) {
                return NotFound();
            }

            var venda = await _context.Vendas
                .FirstOrDefaultAsync(m => m.VendaID == id);
            if (venda == null) {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Vendas == null) {
                return Problem("Entity set 'DataContext.Vendas'  is null.");
            }
            var venda = await _context.Vendas.FindAsync(id);
            if (venda != null) {
                _context.Vendas.Remove(venda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id) {
            return (_context.Vendas?.Any(e => e.VendaID == id)).GetValueOrDefault();
        }
    }
}
