#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaTiendita.Models;
using LaTiendita.Stock;
using Microsoft.AspNetCore.Authorization;

namespace LaTiendita.Controllers
{
    
    public class TallesController : Controller
    {
        private readonly BaseDeDatos _context;

        public TallesController(BaseDeDatos context)
        {
            _context = context;
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Talles.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talle = await _context.Talles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (talle == null)
            {
                return NotFound();
            }

            return View(talle);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TalleId,Nombre")] Talle talle)
        {
            if (await ValidarTalle(talle.Nombre))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(talle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
               
            }

            return RedirectToAction("Create", "Talles");

        }

        private async Task<bool> ValidarTalle(string nombre)
        {
            bool validar = false;

            var nombreTalle = await _context.Talles
               .FirstOrDefaultAsync(m => m.Nombre.ToUpper() == nombre.ToUpper());

            if (nombreTalle == null)
            {
                validar = nombre.ToUpper() == "XS" ||
                          nombre.ToUpper() == "S" ||
                          nombre.ToUpper() == "M" ||
                          nombre.ToUpper() == "L" ||
                          nombre.ToUpper() == "XL" ||
                          nombre.ToUpper() == "XXL" ||
                          nombre.ToUpper() == "XXXL";
            }

            return validar; 
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talle = await _context.Talles.FindAsync(id);
            if (talle == null)
            {
                return NotFound();
            }
            return View(talle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, TalleId, Nombre")] Talle talle)
        {
            if (id != talle.Id)
            {
                return NotFound();
            }

            if (await ValidarTalle(talle.Nombre))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(talle);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TalleExists(talle.Id))
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
                return View(talle);
            }

            return RedirectToAction("Edit", "Talles");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var talle = await _context.Talles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (talle == null)
            {
                return NotFound();
            }

            return View(talle);
        }

        private bool sePuedeBorrar(List<ProductoTalle> productos)
        {
            bool hayProducto = true;

            int i = 0;

            while (i < productos.Count() && hayProducto)
            {
                if (productos[i].Cantidad > 0)
                {
                    hayProducto = false;
                }
                else
                {
                    i++;
                }
            }

            return hayProducto;
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productosAChequear = await _context.ProductoTalle
               .Where(x => x.Id == id)
               .ToListAsync();

            if (sePuedeBorrar(productosAChequear))
            {
                var talle = await _context.Talles.FindAsync(id);
                _context.Talles.Remove(talle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));

            }
        }

            private bool TalleExists(int id)
        {
            return _context.Talles.Any(e => e.Id == id);
        }
    }
}
