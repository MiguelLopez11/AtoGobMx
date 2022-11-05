using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PAT_EstatusVehiculoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public PAT_EstatusVehiculoController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PAT_EstatusVehiculo>>> GetEstatusVehiculo()
        {
            var Estatus = await _context.EstatusVehiculo
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Estatus);
        }

        [HttpGet("{EstatusVehiculoId}")]
        public async Task<ActionResult<PAT_EstatusVehiculo>> GetEstatusById(int EstatusVehiculoId)
        {
            var estatus = await _context.EstatusVehiculo.FirstOrDefaultAsync(f => f.EstatusVehiculoId == EstatusVehiculoId);

            if (estatus == null)
            {
                return NotFound();
            }

            return Ok(estatus);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_EstatusVehiculo>> PostInventario(PAT_EstatusVehiculo estatus)
        {
            _context.EstatusVehiculo.Add(estatus);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Estatus Se ha credo exitosamente");
        }
        [HttpPut("{EstatusVehiculoId}")]
        public async Task<IActionResult> PutEstatusVehiculo(int EstatusVehiculoId, PAT_EstatusVehiculo estatusVehiculo)
        {
            if (estatusVehiculo.EstatusVehiculoId != EstatusVehiculoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var estatus = await _context.EstatusVehiculo.FindAsync(EstatusVehiculoId);
            if (estatus == null)
            {
                return NotFound();
            }
            estatus.EstatusVehiculoId = estatusVehiculo.EstatusVehiculoId;
            estatus.Nombre = estatusVehiculo.Nombre;
            estatus.Descripcion = estatusVehiculo.Descripcion;
            estatus.Archivado = estatusVehiculo.Archivado;

            _context.EstatusVehiculo.Update(estatus);
            await _context.SaveChangesAsync();
            return Ok("Estatus actualizado correctamente");
        }
        [HttpDelete("{EstatusVehiculoId}")]
        public async Task<IActionResult> DeleteInventario(int EstatusVehiculoId)
        {
            var estatus = await _context.EstatusVehiculo
                .FirstOrDefaultAsync(f => f.EstatusVehiculoId == EstatusVehiculoId);
            if (estatus == null)
            {
                return NotFound();
            }
            estatus.Archivado = true;
            _context.EstatusVehiculo.Update(estatus);
            await _context.SaveChangesAsync();
            return Ok("Estatus archivado");
        }
    }
}
