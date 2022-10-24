using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public InventarioController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventario>>> GetInventario()
        {
            var Inventario = await _context.Inventario
                .Include(i => i.InventarioEstatus)
                .Include(i => i.Departamentos)
                .Include(i => i.CategoriaInventario)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Inventario);
        }

        [HttpGet("{InventarioId}")]
        public async Task<ActionResult<Inventario>> GetInventarioById(int InventarioId)
        {
            var inventario = await _context.Inventario.FirstOrDefaultAsync(f => f.InventarioId == InventarioId);

            if (inventario == null)
            {
                return NotFound();
            }

            return Ok(inventario);
        }
        [HttpPost]
        public async Task<ActionResult<Inventario>> PostInventario(Inventario inventario)
        {
            _context.Inventario.Add(inventario);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{InventarioId}")]
        public async Task<IActionResult> PutInventario(int InventarioId, Inventario inventario)
        {
            if (inventario.InventarioId != InventarioId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var Inventario = await _context.Inventario.FindAsync(InventarioId);
            if (Inventario == null)
            {
                return NotFound();
            }
            Inventario.InventarioId = inventario.InventarioId;
            Inventario.Folio = inventario.Folio;
            Inventario.isOnDependency = inventario.isOnDependency;
            Inventario.Dependencia = inventario.Dependencia;
            Inventario.DepartamentoId = inventario.DepartamentoId;
            Inventario.CategoriaInventarioId = inventario.CategoriaInventarioId;
            Inventario.InventarioEstatus = inventario.InventarioEstatus;
            Inventario.Archivado = inventario.Archivado;

            _context.Inventario.Update(Inventario);
            await _context.SaveChangesAsync();
            return Ok("Producto actualizado correctamente");
        }
        [HttpDelete("{InventarioId}")]
        public async Task<IActionResult> DeleteInventario(int InventarioId)
        {
            var inventario = await _context.Inventario
                .FirstOrDefaultAsync(f => f.InventarioId == InventarioId);
            if (inventario == null)
            {
                return NotFound();
            }
            inventario.Archivado = true;
            _context.Inventario.Update(inventario);
            await _context.SaveChangesAsync();
            return Ok("Producto archivado");
        }
    }
}

