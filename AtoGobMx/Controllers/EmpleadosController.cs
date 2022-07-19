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
                .Where(w => !w.archivado)
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
        [HttpDelete]
        public async Task<IActionResult> DeleteEmpleados(int Id)
        {
            var empleado = _context.Empleados
                .FirstOrDefault(f => f.idEmpleado == Id);
            if(empleado == null)
            {
                return NotFound();
            }
            empleado.archivado = true; 
            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();
            return Ok("Empleado archivado");
        }
    }
}
