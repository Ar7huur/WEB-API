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
    public class DepartamentosController : Controller
    {
        private readonly DataContext _context;

        public DepartamentosController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ExibeDepartamentos() {
            return View("~/Views/Departamentos/DepartamentoAjax.cshtml");
        }

        [HttpGet]//get departamentos
        public async Task<IActionResult> pegarDepartamentos() {
            if (_context.Departamentos != null) {
                return Json(await _context.Departamentos.ToListAsync());
            }
            return Problem("Erro com o BD de departamentos");
        }

        [HttpPost]//novo departamento
        public async Task<IActionResult> novoDepartamento(Departamento departamento) {
            if (ModelState.IsValid) {
                _context.Add(departamento);
                await _context.SaveChangesAsync();
                return Json(departamento);
            }
            return Json(ModelState);
        }

        [HttpGet]//pega o departamento pelo id
        public async Task<IActionResult> pegarDepartamentoPeloID(int departamentoId) {
            Departamento departamento = await _context.Departamentos.FindAsync(departamentoId);
            if (departamentoId != null)
                return Json(departamento);
            return Json(new { mensagem = "Departamento nao encontrado no SGBD" });
        }
        [HttpPost] //att venda
        public async Task<IActionResult> atualizarDepartamento(Departamento departamento) {
            if (ModelState.IsValid) {
                _context.Departamentos.Update(departamento);
                await _context.SaveChangesAsync();
                return Json(departamento);
            }
            return Json(ModelState);
        }
        [HttpPost]
        public async Task<IActionResult> deletarDepartamento(int departamentoId) {
            Departamento departamento = await _context.Departamentos.FindAsync(departamentoId);
            if (departamento != null) {
                _context.Departamentos.Remove(departamento);
                await _context.SaveChangesAsync();
                return Json("Deletado");
            }
            return Json(new { mensagem = "Departamento nao encontrado" });
        }


        //Métodos gerados automaticamente
        // GET: Departamentos
        public async Task<IActionResult> Index()
        {
              return _context.Departamentos != null ? 
                          View(await _context.Departamentos.ToListAsync()) :
                          Problem("Entity set 'DataContext.Departamentos'  is null.");
        }

        // GET: Departamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departamentos == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: Departamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: Departamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departamentos == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        // POST: Departamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome")] Departamento departamento)
        {
            if (id != departamento.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.ID))
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
            return View(departamento);
        }

        // GET: Departamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departamentos == null)
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: Departamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departamentos == null)
            {
                return Problem("Entity set 'DataContext.Departamentos'  is null.");
            }
            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento != null)
            {
                _context.Departamentos.Remove(departamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoExists(int id)
        {
          return (_context.Departamentos?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
