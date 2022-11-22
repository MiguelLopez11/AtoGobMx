using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SERMED_RecetaController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public SERMED_RecetaController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SERMED_Receta>>> GetRecetas()
        {
            var Recetas = await _context.Receta
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Recetas == null)
            {
                return BadRequest("No se encuentran citas registradas");
            }
            return Ok(Recetas);
        }

        [HttpGet("{RecetaId}")]
        public async Task<ActionResult<SERMED_Cita>> GetRecetaById(int RecetaId)
        {
            var Receta = await _context.Receta
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.RecetaId == RecetaId);

            if (Receta == null)
            {
                return NotFound();
            }

            return Ok(Receta);
        }
        [HttpPost]
        public async Task<ActionResult<SERMED_Receta>> PostInventario(SERMED_Receta receta)
        {
            _context.Receta.Add(receta);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo la receta exitosamente");
        }
        [HttpPut("{RecetaId}")]
        public async Task<IActionResult> PutCategoria(int RecetaId, SERMED_Receta receta)
        {
            if (receta.RecetaId != RecetaId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var Receta = await _context.Receta.FindAsync(RecetaId);
            if (Receta == null)
            {
                return NotFound();
            }
            Receta.RecetaId = receta.RecetaId;
            Receta.EmpleadoId = receta.EmpleadoId;
            Receta.diagnostico = receta.diagnostico;
            //Cita.Hora = cita.Hora;
            Receta.FechaAlta = receta.FechaAlta;
            Receta.Archivado = receta.Archivado;

            _context.Receta.Update(Receta);
            await _context.SaveChangesAsync();
            return Ok("Cita actualizada correctamente");
        }
        [HttpDelete("{RecetaId}")]
        public async Task<IActionResult> DeleteInventario(int RecetaId)
        {
            var receta = await _context.Receta
                .FirstOrDefaultAsync(f => f.RecetaId == RecetaId);
            if (receta == null)
            {
                return NotFound();
            }
            receta.Archivado = true;
            _context.Receta.Update(receta);
            await _context.SaveChangesAsync();
            return Ok("Cita archivada");
        }
    }
}
