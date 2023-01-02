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
    public class AseoVehiculoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public AseoVehiculoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AseoVehiculo>> GetAseoiVehiculos()
        {
            var aseovehiculo = await _context.AseoVehiculo
                .Include(i => i.Vehiculo)
                .Include(i => i.Ruta)
                .Where(w => !w.Archivado)
                .ToArrayAsync();
            return Ok(aseovehiculo);
        }

        [HttpGet("AseoVehiculoId")]
        public async Task<ActionResult> GetAseoVehiculosById(int AseoVehiculoId)
        {
            var aseovehiculo = await _context.AseoVehiculo
                .Include(i => i.Vehiculo)
                .Include(i => i.Ruta)
                .FirstOrDefaultAsync(f => f.AseoVehiculoId == AseoVehiculoId);
            if (aseovehiculo == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(aseovehiculo);
        }

        [HttpPost]
        public async Task<ActionResult<AseoVehiculo>> PostAseoVehiculos(AseoVehiculo aseoVehiculo)
        {
            object value = _context.AseoVehiculo.Add(aseoVehiculo);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos de aseo se a creado correctamente");
        }


        [HttpPut("{AseoVehiculoId}")]
        public async Task<ActionResult> PutAseoVehiculos(int AseoVehiculoId, AseoVehiculo aseoVehiculo)
        {
            if (aseoVehiculo.AseoVehiculoId != AseoVehiculoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var aseovehiculo = await _context.AseoVehiculo.FirstOrDefaultAsync(f => f.AseoVehiculoId == AseoVehiculoId);
            if (aseovehiculo == null)
            {
                return BadRequest("El Registro del vehiculo en aseo no existe");
            }

            aseovehiculo.AseoVehiculoId = AseoVehiculoId;
            aseovehiculo.VehiculoId = aseoVehiculo.VehiculoId;
            aseovehiculo.RutaId = aseoVehiculo.RutaId;
            aseovehiculo.Archivado = aseoVehiculo.Archivado;

            _context.AseoVehiculo.Update(aseovehiculo);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos de aseo publico actualizado correctamente");
        }

        [HttpDelete("{AseoVehiculoId}")]
        public async Task<IActionResult> DeleteAseoVehiculos(int AseoVehiculoId)
        {
            var aseovehiculo = _context.AseoVehiculo
                .FirstOrDefault(f => f.AseoVehiculoId == AseoVehiculoId);
            if (aseovehiculo == null)
            {
                return NotFound();
            }

            aseovehiculo.Archivado = true;
            _context.AseoVehiculo.Update(aseovehiculo);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos de aseo publico Archivado");
        }

    }
}
