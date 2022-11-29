using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SERMED_ExpedienteMedicoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public SERMED_ExpedienteMedicoController(AtoGobMxContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SERMED_ExpedienteMedico>>> GetExpedientesMedicos()
        {
            var expedientesMedicos = await _context.ExpedienteMedico
                .Include(i => i.Empleados)
                .ToListAsync();
            if (expedientesMedicos == null)
            {
                return BadRequest("No se encuentran citas registradas");
            }
            return Ok(expedientesMedicos);
        }
        [HttpGet("Empleado/{EmpleadoId}")]
        public async Task<ActionResult<IEnumerable<SERMED_ExpedienteMedico>>> GetExpedienteMedicoByEmpleadoId(int EmpleadoId)
        {
            var expedientesMedicos = await _context.ExpedienteMedico
                .Include(i => i.Empleados)
                .Where(w => w.EmpleadoId == EmpleadoId)
                .ToListAsync();
            if (expedientesMedicos == null)
            {
                return BadRequest("No se encuentran citas registradas");
            }
            return Ok(expedientesMedicos);
        }

        [HttpGet("{ExpedienteMedicoId}")]
        public async Task<ActionResult<SERMED_ExpedienteMedico>> GetCitaById(int ExpedienteMedicoId)
        {
            var ExpedienteMedico = await _context.ExpedienteMedico
                .FirstOrDefaultAsync(f => f.ExpedienteMedicoId == ExpedienteMedicoId);

            if (ExpedienteMedico == null)
            {
                return NotFound();
            }

            return Ok(ExpedienteMedico);
        }
        [HttpPost]
        public async Task<ActionResult<SERMED_ExpedienteMedico>> PostInventario(SERMED_ExpedienteMedico ExpedienteMedico)
        {
            _context.ExpedienteMedico.Add(ExpedienteMedico);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Expediente medico Se ha credo exitosamente");
        }
        [HttpPut("{ExpedienteMedicoId}")]
        public async Task<IActionResult> PutCategoria(int ExpedienteMedicoId, SERMED_ExpedienteMedico ExpedienteMedico)
        {
            if (ExpedienteMedico.ExpedienteMedicoId != ExpedienteMedicoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var expedienteMedico = await _context.ExpedienteMedico.FindAsync(ExpedienteMedicoId);
            if (expedienteMedico == null)
            {
                return NotFound();
            }
            expedienteMedico.ExpedienteMedicoId = ExpedienteMedico.ExpedienteMedicoId;
            expedienteMedico.EmpleadoId = ExpedienteMedico.EmpleadoId;
            expedienteMedico.Estatura = ExpedienteMedico.Estatura;
            expedienteMedico.Peso = ExpedienteMedico.Peso;
            expedienteMedico.TipoSangre = ExpedienteMedico.TipoSangre;
            expedienteMedico.Sexo = ExpedienteMedico.Sexo;
            expedienteMedico.Alergias = ExpedienteMedico.Alergias;
            expedienteMedico.Discapacidad = ExpedienteMedico.Discapacidad;
            expedienteMedico.AntecedentesPersonales = ExpedienteMedico.AntecedentesPersonales;
            expedienteMedico.AntecedentesFamiliares = ExpedienteMedico.AntecedentesFamiliares;
            expedienteMedico.Archivado = ExpedienteMedico.Archivado;

            _context.ExpedienteMedico.Update(expedienteMedico);
            await _context.SaveChangesAsync();
            return Ok("Expediente actualizada correctamente");
        }
        [HttpDelete("{ExpedienteMedicoId}")]
        public async Task<IActionResult> DeleteInventario(int ExpedienteMedicoId)
        {
            var expedienteMedico = await _context.ExpedienteMedico
                .FirstOrDefaultAsync(f => f.ExpedienteMedicoId == ExpedienteMedicoId);
            if (expedienteMedico == null)
            {
                return NotFound();
            }
            expedienteMedico.Archivado = true;
            _context.ExpedienteMedico.Update(expedienteMedico);
            await _context.SaveChangesAsync();
            return Ok("Expediente medico archivada");
        }
    }
}
