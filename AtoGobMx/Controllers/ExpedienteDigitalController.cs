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
    public class ExpedienteDigitalController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public ExpedienteDigitalController(AtoGobMxContext Context, IMapper mapper)
        {
            _context = Context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<ExpedienteDigital>> GetExpedientesDigitales()
        {
            var expedientes = await _context.ExpedienteDigitales
                .Where(w => !w.Archivado)
                .OrderBy(o => o.ExpedienteDigitalId)
                .Select(s => _mapper.Map<ExpedienteDigital>(s))
                .ToListAsync();
            return Ok(expedientes);
        }
        [HttpGet("{ExpedienteDigitalId}")]
        public async Task<ActionResult> GetExpedienteById(int ExpedienteDigitalId)
        {
            var expedienteDigital = await _context.ExpedienteDigitales
                .FirstOrDefaultAsync(f => f.ExpedienteDigitalId == ExpedienteDigitalId);
            if (expedienteDigital == null)
            {
                return NotFound();
            }
            return Ok(expedienteDigital);
        }
        [HttpGet("/ExpedienteDigital/{EmpleadoId}")]
        public async Task<ActionResult> GetExpedienteDigitalByEmpleadoId(int EmpleadoId)
        {
            var expedienteDigital = await _context.ExpedienteDigitales
                .FirstOrDefaultAsync(f => f.EmpleadoId == EmpleadoId);
            if(expedienteDigital == null)
            {
                return NotFound();
            }
            return Ok(expedienteDigital);
        }
        [HttpPost]
        public async Task<ActionResult<ExpedienteDigital>> PostExpedienteEmpleado(ExpedienteDigital expedienteDigital)
        {

            _context.ExpedienteDigitales.Add(expedienteDigital);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetExpedienteDigitalByEmpleadoId", new { EmpleadoId = expedienteDigital.EmpleadoId }, expedienteDigital);
        }
        [HttpPut("{ExpedienteDitalId}")]
        public async Task<ActionResult> PutEmpleado(int ExpedienteDitalId, ExpedienteDigital expedienteDigital)
        {
            if (expedienteDigital.ExpedienteDigitalId != ExpedienteDitalId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var expediente = _context.ExpedienteDigitales.Find(ExpedienteDitalId);
            if (expediente == null)
            {
                return BadRequest("El empledo no existe");
            }
            expediente.ExpedienteDigitalId = expedienteDigital.ExpedienteDigitalId;
            expediente.EmpleadoId = expedienteDigital.EmpleadoId;
            expediente.Estado = expedienteDigital.Estado;
            expediente.Municipio = expedienteDigital.Municipio;
            expediente.Localidad = expedienteDigital.Localidad;
            expediente.Calle = expedienteDigital.Calle;
            expediente.NumeroExterior = expedienteDigital.NumeroInterior;
            expediente.NumeroInterior = expedienteDigital.NumeroInterior;
            expediente.CodigoPostal = expedienteDigital.CodigoPostal;
            expediente.CorreoElectronico = expedienteDigital.CorreoElectronico;
            expediente.Archivado = expedienteDigital.Archivado;

            _context.ExpedienteDigitales.Update(expediente);
            await _context.SaveChangesAsync();
            return Ok("Expediente actualizado correctamente");
        }
        [HttpDelete("{ExpedienteDigitalId}")]
        public async Task<IActionResult> DeleteExpedienteDigital(int ExpedienteDigitalId)
        {
            var expedienteDigital = _context.ExpedienteDigitales
                .FirstOrDefault(f => f.ExpedienteDigitalId == ExpedienteDigitalId);
            if (expedienteDigital == null)
            {
                return NotFound();
            }
            expedienteDigital.Archivado = true;
            _context.ExpedienteDigitales.Update(expedienteDigital);
            await _context.SaveChangesAsync();
            return Ok("Expediente Digital archivado");
        }
    }
}
