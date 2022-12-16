using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PAT_VehiculosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public PAT_VehiculosController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PAT_Vehiculo>>> GetVehiculos()
        {
            var vehiculos = await _context.Vehiculo
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(vehiculos);
        }

        [HttpGet("{VehiculoId}")]
        public async Task<ActionResult<PAT_Vehiculo>> GetVehiculo(int VehiculoId)
        {
            var vehiculo = await _context.Vehiculo.FirstOrDefaultAsync(f => f.VehiculoId == VehiculoId);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return Ok(vehiculo);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_Vehiculo>> PostMonitor(PAT_Vehiculo vehiculo)
        {
            _context.Vehiculo.Add(vehiculo);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{VehiculoId}")]
        public async Task<IActionResult> PutVehiculo(int VehiculoId, PAT_Vehiculo vehiculo)
        {
            if (vehiculo.VehiculoId != VehiculoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var Vehiculo = await _context.Vehiculo.FindAsync(VehiculoId);
            if (Vehiculo == null)
            {
                return NotFound();
            }
            Vehiculo.VehiculoId = vehiculo.VehiculoId;
            Vehiculo.Marca = vehiculo.Marca;
            Vehiculo.Modelo = vehiculo.Modelo;
            Vehiculo.Color = vehiculo.Color;
            Vehiculo.Placa = vehiculo.Placa;
            Vehiculo.NumeroSerie = vehiculo.NumeroSerie;
            Vehiculo.AñoLanzamiento = vehiculo.AñoLanzamiento;
            Vehiculo.Puertas = vehiculo.Puertas;
            Vehiculo.Transmisión = vehiculo.Transmisión;
            Vehiculo.Archivado = vehiculo.Archivado;

            _context.Vehiculo.Update(Vehiculo);
            await _context.SaveChangesAsync();
            return Ok("Monitor actualizado correctamente");
        }
        [HttpDelete("{VehiculoId}")]
        public async Task<IActionResult> DeleteVehiculo(int VehiculoId)
        {
            var vehiculo = await _context.Vehiculo
                .FirstOrDefaultAsync(f => f.VehiculoId == VehiculoId);
            if (vehiculo == null)
            {
                return NotFound();
            }
            vehiculo.Archivado = true;
            _context.Vehiculo.Update(vehiculo);
            await _context.SaveChangesAsync();
            return Ok("Producto archivado");
        }
    }
}
