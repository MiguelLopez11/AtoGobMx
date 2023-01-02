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
    public class AseoEmpleadoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public AseoEmpleadoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<EmpleadosAlumbrado>> GetAseoEmpleado()
        {
            var aseoempleado = await _context.AseoEmpleado
                .Include(i => i.Empleados)
                .Include(i => i.Ruta)
                .Where(w => !w.Archivado)
                .ToArrayAsync();
            return Ok(aseoempleado);
        }

        [HttpGet("AseoEmpleadoId")]
        public async Task<ActionResult> GetAseoEmpleadoById(int AseoEmpleadoId)
        {
            var aseoempleado = await _context.AseoEmpleado
                .Include(i => i.Empleados)
                .Include(i => i.Ruta)
                .FirstOrDefaultAsync(f => f.AseoEmpleadoId == AseoEmpleadoId);
            if (aseoempleado == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(aseoempleado);
        }

        [HttpPost]
        public async Task<ActionResult<AseoEmpleado>> PostAseoEmpleado(AseoEmpleado aseoEmpleado)
        {
            object value = _context.AseoEmpleado.Add(aseoEmpleado);
            await _context.SaveChangesAsync();
            return Ok("Empleados aseo creados correctamente");
        }

        [HttpPut("{AseoEmpleadoId}")]
        public async Task<ActionResult> PutAseoEmpleados(int AseoEmpleadoId, AseoEmpleado aseoEmpleado)
        {
            if (aseoEmpleado.AseoEmpleadoId != AseoEmpleadoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var aseoempleado = await _context.AseoEmpleado.FirstOrDefaultAsync(f => f.AseoEmpleadoId == AseoEmpleadoId);
            if (aseoempleado == null)
            {
                return BadRequest("El Registro del empleado aseo no existe");
            }

            aseoempleado.AseoEmpleadoId = AseoEmpleadoId;
            aseoempleado.EmpleadoId = aseoEmpleado.EmpleadoId;
            aseoempleado.RutaId = aseoEmpleado.RutaId;
            aseoempleado.Archivado = aseoEmpleado.Archivado;

            _context.AseoEmpleado.Update(aseoempleado);
            await _context.SaveChangesAsync();
            return Ok("Empleados aseo actualizado correctamente");
        }

        [HttpDelete("{AseoEmpleadoId}")]
        public async Task<IActionResult> DeleteAseoEmpleado(int AseoEmpleadoId)
        {
            var aseoempleado = _context.AseoEmpleado
                .FirstOrDefault(f => f.AseoEmpleadoId == AseoEmpleadoId);
            if (aseoempleado == null)
            {
                return NotFound();
            }

            aseoempleado.Archivado = true;
            _context.AseoEmpleado.Update(aseoempleado);
            await _context.SaveChangesAsync();
            return Ok("Empleados aseo Archivado");
        }
    }
}
