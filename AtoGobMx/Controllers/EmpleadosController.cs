using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public EmpleadosController(AtoGobMxContext Context, IMapper mapper)
        {
            _context = Context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Empleado>> GetEmpleados()
        {
            var empleados = await _context.Empleados
                .Include(i => i.Area)
                .Include(i => i.usuario.Role)
                .Include(i => i.ExpedienteDigital)
                .Where(w => !w.Archivado)
                .OrderBy(o => o.EmpleadoId)
                .ToListAsync();
            return Ok(empleados);
        }

        [HttpGet("{EmpleadoId}")]
        public async Task<ActionResult> GetEmpleadosById(int EmpleadoId)
        {
            var Empleado = await _context.Empleados
                .Include(i => i.Area)
                //.Include(i => i.usuario)
                .Include(i => i.usuario.Role)
                .Include(i => i.ExpedienteDigital)
                .FirstOrDefaultAsync(f => f.EmpleadoId == EmpleadoId);
            if (Empleado == null)
            {
                return NotFound();
            }
            return Ok(Empleado);
        }
        [HttpGet("EmpleadoExpedientes")]
        public async Task<ActionResult> GetEmpleadoExpedientes()
        {
            var Empleado = await _context.Empleados
                .Include(i => i.Area)
                //.Include(i => i.usuario)
                .Include(i => i.usuario.Role)
                .Include(i => i.ExpedienteDigital)
                .Where(w => w.expedienteDigitalId != null)
                .ToListAsync();
            if (Empleado == null)
            {
                return BadRequest();
            }
            return Ok(Empleado);
        }
        [HttpGet("EmpleadosSinExpedientes")]
        public async Task<ActionResult> GetEmpleadosSinExpediente()
        {
            var Empleado = await _context.Empleados
                .Include(i => i.Area)
                .Include(i => i.usuario)
                .Include(i => i.usuario.Role)
                .Include(i => i.ExpedienteDigital)
                .Where(w => w.expedienteDigitalId == null)
                .ToListAsync();
            if (Empleado == null)
            {
                return BadRequest();
            }
            return Ok(Empleado);
        }
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleados(Empleado Empleado)
        {

            _context.Empleados.Add(Empleado);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEmpleadosById", new { EmpleadoId = Empleado.EmpleadoId }, Empleado);
        }
        [HttpPut("{EmpleadoId}")]
        public async Task<ActionResult> PutEmpleado(int EmpleadoId, Empleado empleado)
        {
            if (empleado.EmpleadoId != EmpleadoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var emp = _context.Empleados.Find(EmpleadoId);
            if (emp == null)
            {
                return BadRequest("El empledo no existe");
            }
            emp.EmpleadoId = empleado.EmpleadoId;
            emp.NombreCompleto = empleado.NombreCompleto;
            emp.FechaNacimiento = empleado.FechaNacimiento;
            emp.AreaId = empleado.AreaId;
            emp.UsuarioId = empleado.UsuarioId;
            emp.expedienteDigitalId = empleado.expedienteDigitalId;
            emp.Archivado = empleado.Archivado;
            emp.FechaAlta = empleado.FechaAlta;

            _context.Empleados.Update(emp);
            await _context.SaveChangesAsync();
            return Ok("Empleado actualizado correctamente");
        }
        [HttpDelete("{empleadoId}")]
        public async Task<IActionResult> DeleteEmpleados(int empleadoId)
        {
            var empleado = _context.Empleados
                .FirstOrDefault(f => f.EmpleadoId == empleadoId);
            if (empleado == null)
            {
                return NotFound();
            }
            empleado.Archivado = true;
            empleado.FechaBaja = DateTime.Today;
            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();
            return Ok("Empleado archivado");
        }
    }
}
