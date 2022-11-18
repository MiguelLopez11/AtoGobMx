using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SERMED_CitaController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public SERMED_CitaController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SERMED_Cita>>> GetCitas()
        {
            var Citas = await _context.Cita
                .ToListAsync();
            if (Citas == null)
            {
                return BadRequest("No se encuentran citas registradas"); 
            }
            return Ok(Citas);
        }

        [HttpGet("{CitaId}")]
        public async Task<ActionResult<SERMED_Cita>> GetCitaById(int CitaId)
        {
            var Cita = await _context.Cita.FirstOrDefaultAsync(f => f.CitaId == CitaId);

            if (Cita == null)
            {
                return NotFound();
            }

            return Ok(Cita);
        }
        [HttpPost]
        public async Task<ActionResult<SERMED_Cita>> PostInventario(SERMED_Cita cita)
        {
            _context.Cita.Add(cita);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Cita Se ha credo exitosamente");
        }
        [HttpPut("{CitaId}")]
        public async Task<IActionResult> PutCategoria(int CitaId, SERMED_Cita cita)
        {
            if (cita.CitaId != CitaId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var Cita = await _context.Cita.FindAsync(CitaId);
            if (Cita == null)
            {
                return NotFound();
            }
            Cita.CitaId = cita.CitaId;
            //Cita.NombreCitante = cita.NombreCitante;
            Cita.FechaHora = cita.FechaHora;
            //Cita.Hora = cita.Hora;
            Cita.Descripcion = cita.Descripcion;
            Cita.Archivado = cita.Archivado;

            _context.Cita.Update(Cita);
            await _context.SaveChangesAsync();
            return Ok("Cita actualizada correctamente");
        }
        [HttpDelete("{CitaId}")]
        public async Task<IActionResult> DeleteInventario(int CitaId)
        {
            var cita = await _context.Cita
                .FirstOrDefaultAsync(f => f.CitaId == CitaId);
            if (cita == null)
            {
                return NotFound();
            }
            cita.Archivado = true;
            _context.Cita.Update(cita);
            await _context.SaveChangesAsync();
            return Ok("Cita archivada");
        }
    }
}
