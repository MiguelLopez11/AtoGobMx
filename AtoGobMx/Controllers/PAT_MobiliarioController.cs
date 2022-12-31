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
            var mobiliarios = await _context.Mobiliario
                .Include(i => i.Departamentos)
                //.Include(i => i.Area)
                .Include(i => i.TipoMobiliario)
                .ToListAsync();
            return Ok(mobiliarios);
        }

        [HttpGet("{MobiliarioId}")]
        public async Task<ActionResult<PAT_Mobiliario>> GetMobiliarioById(int MobiliarioId)
        {
            var mobiliario = await _context.Mobiliario
                .Include(i => i.Departamentos)
                //.Include(i => i.Area)
                .Include(i => i.TipoMobiliario)
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
            _context.Mobiliario.Add(mobiliario);
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
            var PAT_Mobiliario = await _context.Mobiliario.FindAsync(MobiliarioId);
            if (PAT_Mobiliario == null)
            {
                return NotFound();
            }
            PAT_Mobiliario.MobiliarioId = mobiliario.MobiliarioId;
            PAT_Mobiliario.CodigoInventario = mobiliario.CodigoInventario;
            PAT_Mobiliario.Descripción = mobiliario.Descripción;
            PAT_Mobiliario.FechaAdquisición = mobiliario.FechaAdquisición;
            PAT_Mobiliario.Costo = mobiliario.Costo;
            PAT_Mobiliario.DepartamentoId = mobiliario.DepartamentoId;
            PAT_Mobiliario.TipoMobiliarioId = mobiliario.TipoMobiliarioId;
            PAT_Mobiliario.Archivado = mobiliario.Archivado;

            _context.Mobiliario.Update(PAT_Mobiliario);
            await _context.SaveChangesAsync();
            return Ok("Mobiliario actualizado correctamente");
        }
        [HttpDelete("{MobiliarioId}")]
        public async Task<IActionResult> DeleteEquipoComputo(int MobiliarioId)
        {
            var mobiliario = await _context.Mobiliario
                .FirstOrDefaultAsync(f => f.MobiliarioId == MobiliarioId);
            if (mobiliario == null)
            {
                return NotFound();
            }
            mobiliario.Archivado = true;
            _context.Mobiliario.Update(mobiliario);
            await _context.SaveChangesAsync();
            return Ok("Producto archivado");
        }
    }
}
