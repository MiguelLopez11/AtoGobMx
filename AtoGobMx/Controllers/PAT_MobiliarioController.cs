using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PAT_MobiliarioController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public PAT_MobiliarioController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PAT_Mobiliario>>> GetMobiliarios()
        {
            var mobiliarios = await _context.PAT_Mobiliario
                .Include(i => i.Area)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(mobiliarios);
        }

        [HttpGet("{MobiliarioId}")]
        public async Task<ActionResult<PAT_Mobiliario>> GetMobiliarioById(int MobiliarioId)
        {
            var mobiliario = await _context.PAT_Mobiliario
                .Include(i => i.Area)
                .FirstOrDefaultAsync(f => f.MobiliarioId == MobiliarioId);

            if (mobiliario == null)
            {
                return NotFound();
            }

            return Ok(mobiliario);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_Mobiliario>> PostMobiliario(PAT_Mobiliario mobiliario)
        {
            _context.PAT_Mobiliario.Add(mobiliario);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{MobiliarioId}")]
        public async Task<IActionResult> PutMobiliario(int MobiliarioId, PAT_Mobiliario mobiliario)
        {
            if (mobiliario.MobiliarioId != MobiliarioId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var PAT_Mobiliario = await _context.PAT_Mobiliario.FindAsync(MobiliarioId);
            if (PAT_Mobiliario == null)
            {
                return NotFound();
            }
            PAT_Mobiliario.MobiliarioId = mobiliario.MobiliarioId;
            PAT_Mobiliario.Descripción = mobiliario.Descripción;
            PAT_Mobiliario.NombreMobiliario = mobiliario.NombreMobiliario;
            PAT_Mobiliario.AreaId = mobiliario.AreaId;
            PAT_Mobiliario.Archivado = mobiliario.Archivado;

            _context.PAT_Mobiliario.Update(PAT_Mobiliario);
            await _context.SaveChangesAsync();
            return Ok("Mobiliario actualizado correctamente");
        }
        [HttpDelete("{MobiliarioId}")]
        public async Task<IActionResult> DeleteEquipoComputo(int MobiliarioId)
        {
            var mobiliario = await _context.PAT_Mobiliario
                .FirstOrDefaultAsync(f => f.MobiliarioId == MobiliarioId);
            if (mobiliario == null)
            {
                return NotFound();
            }
            mobiliario.Archivado = true;
            _context.PAT_Mobiliario.Update(mobiliario);
            await _context.SaveChangesAsync();
            return Ok("Producto archivado");
        }
    }
}
