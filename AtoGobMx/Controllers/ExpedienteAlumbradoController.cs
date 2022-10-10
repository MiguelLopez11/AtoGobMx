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

        [HttpGet("Alumbrado/AlumbradoId")]
        public async Task<ActionResult> GetExpedienteAlumbradoByAlumbrado(int AlumbradoId)
        {
            var expedienteAlumbrado = await _context.ExpedienteAlumbrado
                .Include(i => i.Alumbrado)
                .FirstOrDefaultAsync(f => f.AlumbradoId == AlumbradoId);
            if (expedienteAlumbrado == null)
            {
                return NotFound();
            }
            return Ok(expedienteAlumbrado);
        }

        [HttpGet("{ExpedienteAlumbradoId}")]
        public async Task<ActionResult> GetExpedienteAlumbradoById(int ExpedienteAlumbradoId)
        {
            var expediente = await _context.ExpedienteAlumbrado
                .FirstOrDefaultAsync(f => f.ExpedienteAlumbradoId == ExpedienteAlumbradoId);
            if (expediente == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(expediente);
        }

        [HttpPost()]
        public async Task<ActionResult<ExpedienteAlumbrado>> PostExpedienteAlumbrado(ExpedienteAlumbrado expedienteAlumbrado)
        {
            _context.ExpedienteAlumbrado.Add(expedienteAlumbrado);
            await _context.SaveChangesAsync();
            return Ok("Expediente creado correctamente");
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

            expedientalumbra.ExpedienteAlumbradoId = ExpedienteAlumbradoId;
            expedientalumbra.Tarea = expedienteAlumbrado.Tarea;
            expedientalumbra.FechaAlta = expedienteAlumbrado.FechaAlta;
            expedientalumbra.FechaBaja = expedienteAlumbrado.FechaBaja;
            expedientalumbra.Domicilio = expedienteAlumbrado.Domicilio;
            expedientalumbra.DescripcionDomicilio = expedienteAlumbrado.DescripcionDomicilio;
            expedienteAlumbrado.DescripcionSolucion = expedientalumbra.DescripcionSolucion;
            expedientalumbra.AlumbradoId = expedienteAlumbrado.AlumbradoId;

            _context.ExpedienteAlumbrado.Update(expedientalumbra);
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
