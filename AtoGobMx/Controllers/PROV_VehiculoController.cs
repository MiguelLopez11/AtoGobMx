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
    public class PROV_VehiculoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public PROV_VehiculoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PROV_Vehiculo>> GetProvVehiculos()
        {
            var provvehiculo = await _context.PROV_Vehiculo
                .Include(i => i.Vehiculo)
                .Include(i => i.ControlDeVales)
                .Where(w => !w.Archivado)
                .ToArrayAsync();
            return Ok(provvehiculo);
        }

        [HttpGet("ProvVehiculoId")]
        public async Task<ActionResult> GetProvVehiculosById(int ProvVehiculoId)
        {
            var provvehiculo = await _context.PROV_Vehiculo
                .Include(i => i.Vehiculo)
                .Include(i => i.ControlDeVales)
                .FirstOrDefaultAsync(f => f.ProvVehiculoId == ProvVehiculoId);
            if (provvehiculo == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(provvehiculo);
        }

        [HttpPost]
        public async Task<ActionResult<PROV_Vehiculo>> PostProvVehiculos(PROV_Vehiculo provVehiculo)
        {
            object value = _context.PROV_Vehiculo.Add(provVehiculo);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos de proveeduria creado correctamente");
        }

        [HttpPut("{ProvVehiculoId}")]
        public async Task<ActionResult> PutProvVehiculos(int ProvVehiculoId, PROV_Vehiculo provVehiculo)
        {
            if (provVehiculo.ProvVehiculoId != ProvVehiculoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var provehiculo = await _context.PROV_Vehiculo.FirstOrDefaultAsync(f => f.ProvVehiculoId == ProvVehiculoId);
            if (provehiculo == null)
            {
                return BadRequest("El Registro del vehiculo en proveeduria no existe");
            }

            provehiculo.ProvVehiculoId = ProvVehiculoId;
            provehiculo.VehiculoId = provVehiculo.VehiculoId;
            provehiculo.ControlValeId = provVehiculo.ControlValeId;
            provehiculo.Archivado = provVehiculo.Archivado;

            _context.PROV_Vehiculo.Update(provehiculo);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos de proveeduria actualizado correctamente");
        }

        [HttpDelete("{ProvVehiculoId}")]
        public async Task<IActionResult> DeleteProvVehiculos(int ProvVehiculoId)
        {
            var provvehiculo = _context.PROV_Vehiculo
                .FirstOrDefault(f => f.ProvVehiculoId == ProvVehiculoId);
            if (provvehiculo == null)
            {
                return NotFound();
            }

            provvehiculo.Archivado = true;
            _context.PROV_Vehiculo.Update(provvehiculo);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos de proveeduria Archivado");
        }
    }
}
