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
                .Where(w => !w.Archivado)
                .OrderBy(o => o.EmpleadoId)
                .Select(s => _mapper.Map<Empleado>(s))
                .ToListAsync();
            return Ok(empleados);
        }

        [HttpGet("{EmpleadoId}")]
        public async Task<ActionResult> GetEmpleadosById(int EmpleadoId)
        {
            var Empleado = await _context.Empleados
                .FindAsync(EmpleadoId);
            if (Empleado == null)
            {
                Ok($"No se encuentra el area con el ID: {EmpleadoId}");
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
            emp.Nombre = empleado.Nombre;
            emp.ApellidoPaterno = empleado.ApellidoPaterno;
            emp.ApellidoMaterno = empleado.ApellidoMaterno;
            emp.FechaNacimiento = empleado.FechaNacimiento;
            emp.AreaId = empleado.AreaId;
            emp.Municipio = empleado.Municipio;
            emp.Calle = empleado.Calle;
            emp.NumeroExterior = empleado.NumeroExterior;
            emp.NumeroInterior = empleado.NumeroInterior;
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
