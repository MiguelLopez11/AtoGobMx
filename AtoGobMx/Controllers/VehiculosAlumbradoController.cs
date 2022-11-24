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
    public class VehiculosAlumbradoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public VehiculosAlumbradoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<VehiculosAlumbrado>> GetVehiculoAlumbrado()
        {
            var vehiculos = await _context.VehiculosAlumbrado
                .OrderBy(o => o.VehiculoAlumbradoId)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<VehiculosAlumbrado>(s))
                .ToArrayAsync();
            return Ok(vehiculos);
        }

        [HttpGet("{VehiculoAlumbradoId}")]
        public async Task<ActionResult> GetVehiculoAlumbradoById(int VehiculoAlumbradoId)
        {
            var vehiculo = await _context.VehiculosAlumbrado
                .FirstOrDefaultAsync(f => f.VehiculoAlumbradoId == VehiculoAlumbradoId);
            if (vehiculo == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(vehiculo);
        }

        [HttpPost]
        public async Task<ActionResult<VehiculosAlumbrado>> PostVehiculoAlumbrado(VehiculosAlumbrado vehiculosAlumbrado)
        {
            object value = _context.VehiculosAlumbrado.Add(vehiculosAlumbrado);
            await _context.SaveChangesAsync();
            return Ok("Vehiculo Alumbrado creado correctamente");
        }

        [HttpPut("{VehiculoAlumbradoId}")]
        public async Task<ActionResult> PutVehiculoAlumbrado(int VehiculoAlumbradoId, VehiculosAlumbrado vehiculosAlumbrado)
        {
            if (vehiculosAlumbrado.VehiculoAlumbradoId != VehiculoAlumbradoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var vehiculo = await _context.VehiculosAlumbrado.FirstOrDefaultAsync(f => f.VehiculoAlumbradoId == VehiculoAlumbradoId);
            if (vehiculo == null)
            {
                return BadRequest("El Registro del proveedor no existe");
            }

            vehiculo.VehiculoAlumbradoId = VehiculoAlumbradoId;
            vehiculo.EstatusVehiculoId = vehiculosAlumbrado.EstatusVehiculoId;
            vehiculo.VehiculoId = vehiculosAlumbrado.VehiculoId;
            vehiculo.ExpedienteAlumbradoId = vehiculosAlumbrado.ExpedienteAlumbradoId;
            vehiculo.Archivado = vehiculosAlumbrado.Archivado;

            _context.VehiculosAlumbrado.Update(vehiculo);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos alumbrado actualizado correctamente");
        }

        [HttpDelete("{VehiculoAlumbradoId}")]
        public async Task<IActionResult> DeleteVehiculoAlumbrado(int VehiculoAlumbradoId)
        {
            var vehiculo = _context.VehiculosAlumbrado
                .FirstOrDefault(f => f.VehiculoAlumbradoId == VehiculoAlumbradoId);
            if (vehiculo == null)
            {
                return NotFound();
            }

            vehiculo.Archivado = true;
            _context.VehiculosAlumbrado.Update(vehiculo);
            await _context.SaveChangesAsync();
            return Ok("Vehiculo alumbrado Archivado");
        }
    }
}
