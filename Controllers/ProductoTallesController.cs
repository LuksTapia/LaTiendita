using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using LaTiendita.Models;
using LaTiendita.Stock;

namespace LaTiendita.Controllers
{
    
    public class ProductoTallesController : Controller
    {
        private readonly BaseDeDatos _context;

        public ProductoTallesController(BaseDeDatos context)
        {
            _context = context;
        }

        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index()
        {
            var baseDeDatos = _context.ProductoTalle.Include(p => p.Producto).Include(p => p.Talle);
            return View(await baseDeDatos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductoTalle == null)
            {
                return NotFound();
            }

            var productoTalle = await _context.ProductoTalle
                .Include(p => p.Producto)
                .Include(p => p.Talle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productoTalle == null)
            {
                return NotFound();
            }

            return View(productoTalle);
        }

    }
}
