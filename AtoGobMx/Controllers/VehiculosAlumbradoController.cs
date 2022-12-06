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
        public async Task<ActionResult<VehiculosAlumbrado>> GetVehiculosAlumbrado()
        {
            var vehiculos = await _context.VehiculosAlumbrado
                .Include(i => i.Vehiculo)
                .Include(i => i.ExpedienteAlumbrado)
                .Where(w => !w.Archivado)
                .ToArrayAsync();
            return Ok(vehiculos);
        }

        [HttpGet("VehiculoAlumbradoId")]
        public async Task<ActionResult> GetVehiculosAlumbradoById(int VehiculoAlumbradoId)
        {
            var vehiculos = await _context.VehiculosAlumbrado
                //.Include(i => i.TareaTipoAlumbrado)
                //.Include(i => i.Estatus)
                .FirstOrDefaultAsync(f => f.VehiculoAlumbradoId == VehiculoAlumbradoId);
            if (vehiculos == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(vehiculos);
        }

        [HttpPost]
        public async Task<ActionResult<VehiculosAlumbrado>> PostVehiculosAlumbrado(VehiculosAlumbrado vehiculosAlumbrado)
        {
            object value = _context.VehiculosAlumbrado.Add(vehiculosAlumbrado);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos Alumbrado creado correctamente");
        }

        [HttpPut("{VehiculoAlumbradoId}")]
        public async Task<ActionResult> PutVehiculosAlumbrado(int VehiculoAlumbradoId, VehiculosAlumbrado vehiculosAlumbrado)
        {
            if (vehiculosAlumbrado.VehiculoAlumbradoId != VehiculoAlumbradoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var vehicles = await _context.VehiculosAlumbrado.FirstOrDefaultAsync(f => f.VehiculoAlumbradoId == VehiculoAlumbradoId);
            if (vehicles == null)
            {
                return BadRequest("El Registro del vehiculo alumbrado no existe");
            }

            vehicles.VehiculoAlumbradoId = VehiculoAlumbradoId;
            vehicles.VehiculoId = vehiculosAlumbrado.VehiculoId;
            vehicles.ExpedienteAlumbradoId = vehiculosAlumbrado.ExpedienteAlumbradoId;
            vehicles.Archivado = vehiculosAlumbrado.Archivado;

            _context.VehiculosAlumbrado.Update(vehicles);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos alumbrado actualizado correctamente");
        }

        [HttpDelete("{VehiculoAlumbradoId}")]
        public async Task<IActionResult> DeleteVehiculosAlumbrado(int VehiculoAlumbradoId)
        {
            var vehicle = _context.VehiculosAlumbrado
                .FirstOrDefault(f => f.VehiculoAlumbradoId == VehiculoAlumbradoId);
            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.Archivado = true;
            _context.VehiculosAlumbrado.Update(vehicle);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos alumbrado Archivado");
        }
    }
}
