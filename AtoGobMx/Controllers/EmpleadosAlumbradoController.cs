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
    public class EmpleadosAlumbradoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public EmpleadosAlumbradoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<EmpleadosAlumbrado>> GetEmpleadosAlumbrado()
        {
            var EmAlumbrado = await _context.EmpleadosAlumbrado
                .Include(i => i.Empleados)
                .Include(i => i.ExpedienteAlumbrado)
                .Where(w => !w.Archivado)
                .ToArrayAsync();
            return Ok(EmAlumbrado);
        }

        [HttpGet("AlumbradoEmpleadoId")]
        public async Task<ActionResult> GetEmpleadosAlumbradoById(int AlumbradoEmpleadoId)
        {
            var emalumbrado = await _context.EmpleadosAlumbrado
                //.Include(i => i.TareaTipoAlumbrado)
                //.Include(i => i.Estatus)
                .FirstOrDefaultAsync(f => f.AlumbradoEmpleadoId == AlumbradoEmpleadoId);
            if (emalumbrado == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(emalumbrado);
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadosAlumbrado>> PostEmpleadosAlumbrado(EmpleadosAlumbrado empleadosAlumbrado)
        {
            object value = _context.EmpleadosAlumbrado.Add(empleadosAlumbrado);
            await _context.SaveChangesAsync();
            return Ok("Empleados Alumbrado creado correctamente");
        }

        [HttpPut("{AlumbradoEmpleadoId}")]
        public async Task<ActionResult> PutEmpleadosAlumbrado(int AlumbradoEmpleadoId, EmpleadosAlumbrado empleadosAlumbrado)
        {
            if (empleadosAlumbrado.AlumbradoEmpleadoId != AlumbradoEmpleadoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var emAlumbrado = await _context.EmpleadosAlumbrado.FirstOrDefaultAsync(f => f.AlumbradoEmpleadoId == AlumbradoEmpleadoId);
            if (emAlumbrado == null)
            {
                return BadRequest("El Registro del empleado alumbrado no existe");
            }

            emAlumbrado.AlumbradoEmpleadoId = AlumbradoEmpleadoId;
            emAlumbrado.EmpleadoId = empleadosAlumbrado.EmpleadoId;
            emAlumbrado.ExpedienteAlumbradoId = empleadosAlumbrado.ExpedienteAlumbradoId;
            emAlumbrado.Archivado = empleadosAlumbrado.Archivado;

            _context.EmpleadosAlumbrado.Update(emAlumbrado);
            await _context.SaveChangesAsync();
            return Ok("Empleados alumbrado actualizado correctamente");
        }

        [HttpDelete("{AlumbradoEmpleadoId}")]
        public async Task<IActionResult> DeleteEmpleadosAlumbrado(int AlumbradoEmpleadoId)
        {
            var emAlumbrado = _context.EmpleadosAlumbrado
                .FirstOrDefault(f => f.AlumbradoEmpleadoId == AlumbradoEmpleadoId);
            if (emAlumbrado == null)
            {
                return NotFound();
            }

            emAlumbrado.Archivado = true;
            _context.EmpleadosAlumbrado.Update(emAlumbrado);
            await _context.SaveChangesAsync();
            return Ok("Empleados alumbrado Archivado");
        }
    }
}
