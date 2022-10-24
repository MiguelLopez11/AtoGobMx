using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaInventarioController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public CategoriaInventarioController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaInventario>>> GetInventario()
        {
            var Inventario = await _context.CategoriaInventario
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Inventario);
        }

        [HttpGet("{CategoriaInventarioId}")]
        public async Task<ActionResult<CategoriaInventario>> GetCategoriaById(int CategoriaInventarioId)
        {
            var categoria = await _context.CategoriaInventario.FirstOrDefaultAsync(f => f.CategoriaInventarioId == CategoriaInventarioId);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }
        [HttpPost]
        public async Task<ActionResult<CategoriaInventario>> PostInventario(CategoriaInventario categoriaInventario)
        {
            _context.CategoriaInventario.Add(categoriaInventario);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{CategoriaInventarioId}")]
        public async Task<IActionResult> PutCategoria(int CategoriaInventarioId, CategoriaInventario categoriaInventario)
        {
            if (categoriaInventario.CategoriaInventarioId != CategoriaInventarioId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var categoria = await _context.CategoriaInventario.FindAsync(CategoriaInventarioId);
            if (categoria == null)
            {
                return NotFound();
            }
            categoria.CategoriaInventarioId = categoriaInventario.CategoriaInventarioId;
            categoria.NombreCategoria = categoriaInventario.NombreCategoria;
            categoria.Descripcion = categoriaInventario.Descripcion;
            categoria.Archivado = categoriaInventario.Archivado;

            _context.CategoriaInventario.Update(categoria);
            await _context.SaveChangesAsync();
            return Ok("Categoria actualizado correctamente");
        }
        [HttpDelete("{CategoriaInventarioId}")]
        public async Task<IActionResult> DeleteInventario(int CategoriaInventarioId)
        {
            var categoria = await _context.CategoriaInventario
                .FirstOrDefaultAsync(f => f.CategoriaInventarioId == CategoriaInventarioId);
            if (categoria == null)
            {
                return NotFound();
            }
            categoria.Archivado = true;
            _context.CategoriaInventario.Update(categoria);
            await _context.SaveChangesAsync();
            return Ok("Categoria archivado");
        }
    }
}
