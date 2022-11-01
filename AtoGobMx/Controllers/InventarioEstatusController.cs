using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioEstatusController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public InventarioEstatusController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioEstatus>>> GetInventario()
        {
            var Estatus = await _context.InventarioEstatus
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Estatus);
        }

        [HttpGet("{InventarioEstatusId}")]
        public async Task<ActionResult<InventarioEstatus>> GetEstatusById(int InventarioEstatusId)
        {
            var estatus = await _context.InventarioEstatus.FirstOrDefaultAsync(f => f.InventarioEstatusId == InventarioEstatusId);

            if (estatus == null)
            {
                return NotFound();
            }

            return Ok(estatus);
        }
        [HttpPost]
        public async Task<ActionResult<InventarioEstatus>> PostInventario(InventarioEstatus estatus)
        {
            _context.InventarioEstatus.Add(estatus);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Estatus Se ha credo exitosamente");
        }
        [HttpPut("{InventarioEstatusId}")]
        public async Task<IActionResult> PutCategoria(int InventarioEstatusId, InventarioEstatus inventarioEstatus)
        {
            if (inventarioEstatus.InventarioEstatusId != InventarioEstatusId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var estatus = await _context.InventarioEstatus.FindAsync(InventarioEstatusId);
            if (estatus == null)
            {
                return NotFound();
            }
            estatus.InventarioEstatusId = inventarioEstatus.InventarioEstatusId;
            estatus.Nombre = inventarioEstatus.Nombre;
            estatus.Descripcion = inventarioEstatus.Descripcion;
            estatus.Archivado = inventarioEstatus.Archivado;

            _context.InventarioEstatus.Update(estatus);
            await _context.SaveChangesAsync();
            return Ok("Estatus actualizado correctamente");
        }
        [HttpDelete("{InventarioEstatusId}")]
        public async Task<IActionResult> DeleteInventario(int InventarioEstatusId)
        {
            var estatus = await _context.InventarioEstatus
                .FirstOrDefaultAsync(f => f.InventarioEstatusId == InventarioEstatusId);
            if (estatus == null)
            {
                return NotFound();
            }
            estatus.Archivado = true;
            _context.InventarioEstatus.Update(estatus);
            await _context.SaveChangesAsync();
            return Ok("Estatus archivado");
        }
    }
}
