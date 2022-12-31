using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OP_EmpleadosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public OP_EmpleadosController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<OP_Empleados>> GetEmpleadosObras()
        {
            var EmAlumbrado = await _context.OP_Empleados
                .Include(i => i.Empleados)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<OP_Empleados>(s))
                .ToArrayAsync();
            return Ok(EmAlumbrado);
        }

        [HttpGet("EmpleadoObraId")]
        public async Task<ActionResult> GetEmpleadosObrasById(int EmpleadoObrasId)
        {
            var empleado_obras = await _context.OP_Empleados
                .Include(i => i.Empleados)
                .Include(i => i.ObrasPublicas)
                .FirstOrDefaultAsync(f => f.EmpleadoObrasId == EmpleadoObrasId);
            if (empleado_obras == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(empleado_obras);
        }

        [HttpPost]
        public async Task<ActionResult<OP_Empleados>> PostEmpleadosObras(OP_Empleados empleados)
        {
            object value = _context.OP_Empleados.Add(empleados);
            await _context.SaveChangesAsync();
            return Ok("Los empleados de la obra se ha creado correctamente");
        }

        [HttpPut("{EmpleadoObraId}")]
        public async Task<ActionResult> PutEmpleadosObras(int EmpleadoObrasId, OP_Empleados empleados)
        {
            if (empleados.EmpleadoObrasId != EmpleadoObrasId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var empleadoObras = await _context.OP_Empleados.FirstOrDefaultAsync(f => f.EmpleadoObrasId == EmpleadoObrasId);
            if (empleadoObras == null)
            {
                return BadRequest("El Registro del empleado de obras no existe");
            }

            empleadoObras.EmpleadoObrasId = EmpleadoObrasId;
            empleadoObras.EmpleadoId = empleados.EmpleadoId;
            empleadoObras.ObraId = empleados.ObraId;
            empleadoObras.Archivado = empleados.Archivado;
             
            _context.OP_Empleados.Update(empleadoObras);
            await _context.SaveChangesAsync();
            return Ok("Los empleados de obras publicas actualizado correctamente");
        }

        [HttpDelete("{EmpleadoObrasId}")]
        public async Task<IActionResult> DeleteEmpleadosObras(int EmpleadoObrasId)
        {
            var empleadoObras = _context.OP_Empleados
                .FirstOrDefault(f => f.EmpleadoObrasId == EmpleadoObrasId);
            if (empleadoObras == null)
            {
                return NotFound();
            }

            empleadoObras.Archivado = true;
            _context.OP_Empleados.Update(empleadoObras);
            await _context.SaveChangesAsync();
            return Ok("Los Empleaos de la obra an sido Archivado");
        }
    }
}
