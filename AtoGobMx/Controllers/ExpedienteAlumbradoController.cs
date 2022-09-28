using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteAlumbradoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public ExpedienteAlumbradoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ExpedienteAlumbrado>> GetExpedienteFalla()
        {
            var expefalla = await _context.ExpedienteAlumbrado
                .Where(w => !w.Archivado)
                .OrderBy(o => o.ExpedienteAlumbradoId)
                .ToListAsync();
            return Ok(expefalla);
        }

        [HttpGet("ExpedienteAlumbradoId")]
        public async Task<ActionResult> GetExpedienteAlumbradoById(int ExpedienteAlumbradoId)
        {
            var expedienteAlumbrado = await _context.ExpedienteAlumbrado
                .Include(i => i.Alumbrado)
                .FirstOrDefaultAsync(f => f.ExpedienteAlumbradoId == ExpedienteAlumbradoId);
            if (expedienteAlumbrado == null)
            {
                return NotFound();
            }
            return Ok(expedienteAlumbrado);
        }

        [HttpPost("{AlumbradoId}")]
        public async Task<ActionResult<ExpedienteAlumbrado>> PostExpedienteAlumbrado(ExpedienteAlumbrado expedienteAlumbrado, int AlumbradoId)
        {
            var alumbrado = await _context.Alumbrado.FirstOrDefaultAsync(f => f.AlumbradoId == AlumbradoId);
            _context.ExpedienteAlumbrado.Add(expedienteAlumbrado);

            var expedientealumbra = CreatedAtAction("GetExpedienteAlumbradoById", new { ExpedienteAlumbradoId = expedienteAlumbrado.ExpedienteAlumbradoId }, expedienteAlumbrado);
            alumbrado.expedienteAlumbradoId = expedienteAlumbrado.ExpedienteAlumbradoId;
            await _context.SaveChangesAsync();
            return Ok(expedientealumbra);

        }

        [HttpPut("{ExpedienteAlumbradoId}")]
        public async Task<ActionResult> PutExpedientAlumbrado(int ExpedienteAlumbradoId, ExpedienteAlumbrado expedienteAlumbrado)
        {
            if (expedienteAlumbrado.ExpedienteAlumbradoId != ExpedienteAlumbradoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var expedientalumbra = _context.ExpedienteAlumbrado.Find(ExpedienteAlumbradoId);
            if (expedientalumbra == null)
            {
                return BadRequest("El Registro del expediente alumbrado no existe");
            }

            expedienteAlumbrado.ExpedienteAlumbradoId = expedientalumbra.ExpedienteAlumbradoId;
            expedienteAlumbrado.Tarea = expedientalumbra.Tarea;
            expedienteAlumbrado.FechaAlta = expedientalumbra.FechaAlta;
            expedienteAlumbrado.FechaBaja = expedientalumbra.FechaBaja;
            expedienteAlumbrado.Domicilio = expedientalumbra.Domicilio;
            expedienteAlumbrado.DescripcionDomicilio = expedientalumbra.DescripcionDomicilio;
            expedienteAlumbrado.DescripcionSolucion = expedientalumbra.DescripcionSolucion;

            _context.ExpedienteAlumbrado.Update(expedienteAlumbrado);
            await _context.SaveChangesAsync();
            return Ok("Expediente alumbrado actualizado correctamente");
        }

        [HttpDelete("{ExpedienteAlumbradoId}")]
        public async Task<IActionResult> DeleteExpedienteAlumbrado(int ExpedienteAlumbradoId)
        {
            var expedientalumbrado = _context.ExpedienteAlumbrado
                .FirstOrDefault(f => f.ExpedienteAlumbradoId == ExpedienteAlumbradoId);
            if (expedientalumbrado == null)
            {
                return NotFound();
            }

            expedientalumbrado.Archivado = true;
            _context.ExpedienteAlumbrado.Update(expedientalumbrado);
            await _context.SaveChangesAsync();
            return Ok("Expediente alumbrado Archivado");
        }

    }
}
