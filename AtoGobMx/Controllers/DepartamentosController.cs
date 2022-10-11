using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        public DepartamentosController(AtoGobMxContext Context)
        {
            _context = Context;
        }
        [HttpGet]
        public async Task<ActionResult> GetDepartamentos()
        {
            var areas = await _context.Departamentos
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(areas);
        }
        [HttpGet("{DepartamentoId}")]
        public async Task<ActionResult> GetDepartamentoById(int DepartamentoId)
        {
            var Departamento = await _context.Departamentos.FindAsync(DepartamentoId);
            if (Departamento == null)
            {
                Ok($"No se encuentra el area con el ID: {DepartamentoId}");
            }
            return Ok(Departamento);
        }
        [HttpPost]
        public async Task<ActionResult<Departamentos>> PostDepartamento(Departamentos departamentos)
        {
            _context.Departamentos.Add(departamentos);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha creado el departamento correctamente");
        }
        [HttpPut("{DepartamentoId}")]
        public async Task<ActionResult> PutDepartamento(int DepartamentoId, Departamentos departamentos)
        {
            if (departamentos.DepartamentoId != DepartamentoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var DepartamentoValue = await _context.Departamentos.FindAsync(DepartamentoId);
            if (DepartamentoValue == null)
            {
                return BadRequest("El Departamento no existe");
            }
            DepartamentoValue.DepartamentoId = departamentos.DepartamentoId;
            DepartamentoValue.Nombre = departamentos.Nombre;
            DepartamentoValue.Descripcion = departamentos.Descripcion;
            DepartamentoValue.Archivado = departamentos.Archivado;

            _context.Departamentos.Update(DepartamentoValue);
            await _context.SaveChangesAsync();
            return Ok("EL Departamento ha sido actualizada correctamente");
        }
        [HttpDelete("{DepartamentoId}")]
        public async Task<IActionResult> DeleteDepartamento(int DepartamentoId)
        {
            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(f => f.DepartamentoId == DepartamentoId);
            if (departamento == null)
            {
                return NotFound();
            }
            var areas = await _context.Area
                .Where(w => w.DepartamentoId == DepartamentoId)
                .ToListAsync();
            foreach (var area in areas)
            {
                area.Archivado = true;
            }
            var puestos = await _context.PuestoTrabajo
                .Where(w => w.DepartamentoId == DepartamentoId)
                .ToListAsync();
            foreach (var puesto in puestos)
            {
                puesto.Archivado = true;
            }
            //var empleados = await _context.Empleados
            //    .Where(w => w.DepartamentoId == DepartamentoId)
            //    .ToListAsync();
            //foreach (var empleado in empleados)
            //{
            //    empleado.DepartamentoId = null;
            //    empleado.AreaId = null;
            //    empleado.PuestoTrabajoId = null;
            //}
            departamento.Archivado = true;
            _context.Departamentos.Update(departamento);
            await _context.SaveChangesAsync();
            return Ok("El Departamento ha sido archivado correctamente");
        }
    }
}
