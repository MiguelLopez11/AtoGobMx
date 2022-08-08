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
                .Select(s => _mapper.Map<Empleado>(s))
                .ToListAsync();
            return Ok(empleados);
        }

        [HttpGet("{EmpleadosId}")]
        public async Task<ActionResult> GetEmpleadosById(int EmpleadosId)
        {
            var Empleado = await _context.Empleados
                .FindAsync(EmpleadosId);
            if (Empleado == null)
            {
                Ok($"No se encuentra el area con el ID: {EmpleadosId}");
            }
            return Ok(Empleado);
        }
        [HttpPost]
        public async Task<IActionResult> PostEmpleados(Empleado Empleado)
        {

            _context.Empleados.Add(Empleado);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
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
            emp.Direccion = empleado.Direccion;
            emp.Archivado = empleado.Archivado;
            emp.FechaAlta = empleado.FechaAlta;

            _context.Empleados.Update(emp);
            await _context.SaveChangesAsync();
            return Ok("Empleado actualizado correctamente");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmpleados(int Id)
        {
            var empleado = _context.Empleados
                .FirstOrDefault(f => f.EmpleadoId == Id);
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
