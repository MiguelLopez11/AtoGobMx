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
    public class PuestoTrabajoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public PuestoTrabajoController(AtoGobMxContext Context, IMapper mapper)
        {
            _context = Context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<PuestoTrabajo>> GetPuestosTrabajo()
        {
            var puestosTrabajo = await _context.PuestoTrabajo
                .Include(i => i.Area)
                .Include(i => i.Departamentos)
                .Where(w => !w.Archivado)
                .OrderBy(o => o.PuestoTrabajoId)
                .ToListAsync();
            return Ok(puestosTrabajo);
        }
        [HttpGet("Departamento/{DepartamentoId}")]
        public async Task<ActionResult<PuestoTrabajo>> GetPuestosTrabajoDepartamento(int DepartamentoId)
        {
            var puestosTrabajo = await _context.PuestoTrabajo
                .Include(i => i.Area)
                .Where(w => !w.Archivado)
                .Where(w => w.DepartamentoId == DepartamentoId)
                .OrderBy(o => o.PuestoTrabajoId)
                .ToListAsync();
            return Ok(puestosTrabajo);
        }
        [HttpGet("{PuestoTrabajoId}")]
        public async Task<ActionResult> GetPuestoTrabajoById(int PuestoTrabajoId)
        {
            var puestoTrabajo = await _context.PuestoTrabajo
                .Include(i => i.Area)
                .Include(i => i.Departamentos)
                .FirstOrDefaultAsync(f => f.PuestoTrabajoId == PuestoTrabajoId);
            if (puestoTrabajo == null)
            {
                return NotFound();
            }
            return Ok(puestoTrabajo);
        }
        [HttpPost]
        public async Task<ActionResult<PuestoTrabajo>> PostExpedienteEmpleado(PuestoTrabajo puestoTrabajo)
        {
            _context.PuestoTrabajo.Add(puestoTrabajo);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha creado area correctamente");

        }
        [HttpPut("{PuestoTrabajoId}")]
        public async Task<ActionResult> PutExpediente(int PuestoTrabajoId, PuestoTrabajo puestoTrabajo)
        {
            if (puestoTrabajo.PuestoTrabajoId != PuestoTrabajoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var puesto = await _context.PuestoTrabajo.FirstOrDefaultAsync(f => f.PuestoTrabajoId == PuestoTrabajoId);
            if (puesto == null)
            {
                return BadRequest("El puesto de trabajo no existe");
            }
            puesto.PuestoTrabajoId = puestoTrabajo.PuestoTrabajoId;
            puesto.Nombre = puestoTrabajo.Nombre;
            puesto.AreaId = puestoTrabajo.AreaId;
            puesto.DepartamentoId = puestoTrabajo.DepartamentoId;

            _context.PuestoTrabajo.Update(puesto);
            await _context.SaveChangesAsync();
            return Ok("Puesto trabajo actualizado correctamente");
        }
        [HttpDelete("{PuestoTrabajoId}")]
        public async Task<IActionResult> DeleteExpedienteDigital(int PuestoTrabajoId)
        {
            var puestoTrabajo = await _context.PuestoTrabajo
                .FirstOrDefaultAsync(f => f.PuestoTrabajoId == PuestoTrabajoId);
            if (puestoTrabajo == null)
            {
                return NotFound();
            }
            puestoTrabajo.Archivado = true;
            _context.PuestoTrabajo.Update(puestoTrabajo);
            await _context.SaveChangesAsync();
            return Ok("Puesto de trabajo archivado");
        }
    }
}
