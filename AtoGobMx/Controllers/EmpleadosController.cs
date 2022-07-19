using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        public EmpleadosController(AtoGobMxContext Context)
        {
            _context = Context;
        }

        [HttpGet]
        public async Task<ActionResult<empleados>> GetEmpleados()
        {
            var empleados = await _context.Empleados
                .Include(i => i.Area)
                .Where(w => !w.Archivado)
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
        public async Task<IActionResult> PostEmpleados(empleados Empleado)
        {
            _context.Empleados.Add(Empleado);
            await _context.SaveChangesAsync();
            return StatusCode(200,"Se ha credo exitosamente");
        }
        [HttpPut("{EmpleadoId}")]
        public async Task<IActionResult> PutEmpleado(int EmpleadoId, empleados empleado)
        {
           if(empleado.idEmpleado != EmpleadoId)
            {
                return BadRequest("Los ID ingresados no coinciden");
            }
            //var emp =  _context.Empleados.Find(EmpleadoId);
            //if(emp == null)
            //{
            //    return BadRequest("El empledo no existe");
            //}
            //emp.idEmpleado = empleado.idEmpleado;
            //emp.nombre = empleado.nombre;
            //emp.apellidoPaterno = empleado.apellidoPaterno;
            //emp.apellidoMaterno = empleado.apellidoMaterno;
            //emp.fechaNacimiento = empleado.fechaNacimiento;
            //emp.RFC = empleado.RFC;
            //emp.CURP = empleado.CURP;
            //emp.direccion = empleado.direccion;
            //emp.archivado = empleado.archivado;

            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();
            return Ok("Empleado actualizado correctamente");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmpleados(int Id)
        {
            var empleado = _context.Empleados
                .FirstOrDefault(f => f.idEmpleado == Id);
            if(empleado == null)
            {
                return NotFound();
            }
            empleado.Archivado = true; 
            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();
            return Ok("Empleado archivado");
        }
    }
}
