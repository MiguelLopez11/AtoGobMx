using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SERMED_EstatusRecetaController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public SERMED_EstatusRecetaController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SERMED_EstatusReceta>>> GetEstatusRecetas()
        {
            var Estatus = await _context.EstatusReceta
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Estatus);
        }

        [HttpGet("{EstatusRecetaId}")]
        public async Task<ActionResult<PAT_EstatusVehiculo>> GetEstatusById(int EstatusRecetaId)
        {
            var estatus = await _context.EstatusReceta.FirstOrDefaultAsync(f => f.EstatusRecetaId == EstatusRecetaId);

            if (estatus == null)
            {
                return NotFound();
            }

            return Ok(estatus);
        }
        [HttpPost]
        public async Task<ActionResult<SERMED_EstatusReceta>> PostEstatusReceta(SERMED_EstatusReceta estatus)
        {
            _context.EstatusReceta.Add(estatus);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Estatus Se ha credo exitosamente");
        }
        [HttpPut("{EstatusRecetaId}")]
        public async Task<IActionResult> PutEstatusReceta(int EstatusRecetaId, SERMED_EstatusReceta estatusReceta)
        {
            if (estatusReceta.EstatusRecetaId != EstatusRecetaId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var estatus = await _context.EstatusReceta.FindAsync(EstatusRecetaId);
            if (estatus == null)
            {
                return NotFound();
            }
            estatus.EstatusRecetaId = estatusReceta.EstatusRecetaId;
            estatus.Nombre = estatusReceta.Nombre;
            estatus.Descripcion = estatusReceta.Descripcion;
            estatus.Archivado = estatusReceta.Archivado;

            _context.EstatusReceta.Update(estatus);
            await _context.SaveChangesAsync();
            return Ok("Estatus actualizado correctamente");
        }
        [HttpDelete("{EstatusRecetaId}")]
        public async Task<IActionResult> DeleteEstatusReceta (int EstatusRecetaId)
        {
            var estatus = await _context.EstatusReceta
                .FirstOrDefaultAsync(f => f.EstatusRecetaId == EstatusRecetaId);
            if (estatus == null)
            {
                return NotFound();
            }
            estatus.Archivado = true;
            _context.EstatusReceta.Update(estatus);
            await _context.SaveChangesAsync();
            return Ok("Estatus archivado");
        }
    }
}
