using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMobiliarioController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public TipoMobiliarioController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PAT_TipoMobiliario>>> GetTiposMobiliario()
        {
            var tipoMobiliarios = await _context.TipoMobiliario
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(tipoMobiliarios);
        }

        [HttpGet("{TipoMobiliarioId}")]
        public async Task<ActionResult<PAT_TipoMobiliario>> GetTipoMobiliario(int TipoMobiliarioId)
        {
            var tipoMobiliario = await _context.TipoMobiliario.FirstOrDefaultAsync(f => f.TipoMobiliarioId == TipoMobiliarioId);

            if (tipoMobiliario == null)
            {
                return NotFound();
            }

            return Ok(tipoMobiliario);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_TipoMobiliario>> PostTipoMobiliario(PAT_TipoMobiliario tipoMobiliario)
        {
            _context.TipoMobiliario.Add(tipoMobiliario);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{TipoMobiliarioId}")]
        public async Task<IActionResult> PutTipoMobiliario(int TipoMobiliarioId, PAT_TipoMobiliario tipoMobiliario)
        {
            if (tipoMobiliario.TipoMobiliarioId != TipoMobiliarioId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var PAT_TipoMobiliario = await _context.TipoMobiliario.FindAsync(TipoMobiliarioId);
            if (PAT_TipoMobiliario == null)
            {
                return NotFound();
            }
            PAT_TipoMobiliario.TipoMobiliarioId = tipoMobiliario.TipoMobiliarioId;
            PAT_TipoMobiliario.Nombre = tipoMobiliario.Nombre;
            PAT_TipoMobiliario.Descripción = tipoMobiliario.Descripción;
            PAT_TipoMobiliario.Archivado = tipoMobiliario.Archivado;

            _context.TipoMobiliario.Update(PAT_TipoMobiliario);
            await _context.SaveChangesAsync();
            return Ok("Tipo Mobiliario actualizado correctamente");
        }
        [HttpDelete("{TipoMobiliarioId}")]
        public async Task<IActionResult> DeleteMonitor(int TipoMobiliarioId)
        {
            var tipoMobiliario = await _context.TipoMobiliario
                .FirstOrDefaultAsync(f => f.TipoMobiliarioId == TipoMobiliarioId);
            if (tipoMobiliario == null)
            {
                return NotFound();
            }
            tipoMobiliario.Archivado = true;
            _context.TipoMobiliario.Update(tipoMobiliario);
            await _context.SaveChangesAsync();
            return Ok("Tipo mobiliario archivado");
        }
    }
}
