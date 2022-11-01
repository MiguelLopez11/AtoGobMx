using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PAT_EstatusEquipoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public PAT_EstatusEquipoController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PAT_EstatusEquipo>>> GetInventario()
        {
            var Estatus = await _context.InventarioEstatus
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Estatus);
        }

        [HttpGet("{EstatusEquipoId}")]
        public async Task<ActionResult<PAT_EstatusEquipo>> GetEstatusById(int EstatusEquipoId)
        {
            var estatus = await _context.InventarioEstatus.FirstOrDefaultAsync(f => f.EstatusEquipoId == EstatusEquipoId);

            if (estatus == null)
            {
                return NotFound();
            }

            return Ok(estatus);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_EstatusEquipo>> PostInventario(PAT_EstatusEquipo estatus)
        {
            _context.InventarioEstatus.Add(estatus);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Estatus Se ha credo exitosamente");
        }
        [HttpPut("{EstatusEquipoId}")]
        public async Task<IActionResult> PutCategoria(int EstatusEquipoId, PAT_EstatusEquipo estatusEquipo)
        {
            if (estatusEquipo.EstatusEquipoId != EstatusEquipoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var estatus = await _context.InventarioEstatus.FindAsync(EstatusEquipoId);
            if (estatus == null)
            {
                return NotFound();
            }
            estatus.EstatusEquipoId = estatusEquipo.EstatusEquipoId;
            estatus.Nombre = estatusEquipo.Nombre;
            estatus.Descripcion = estatusEquipo.Descripcion;
            estatus.Archivado = estatusEquipo.Archivado;

            _context.InventarioEstatus.Update(estatus);
            await _context.SaveChangesAsync();
            return Ok("Estatus actualizado correctamente");
        }
        [HttpDelete("{EstatusEquipoId}")]
        public async Task<IActionResult> DeleteInventario(int EstatusEquipoId)
        {
            var estatus = await _context.InventarioEstatus
                .FirstOrDefaultAsync(f => f.EstatusEquipoId == EstatusEquipoId);
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
