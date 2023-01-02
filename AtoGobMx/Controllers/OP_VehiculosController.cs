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
    public class OP_VehiculosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public OP_VehiculosController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<OP_Vehiculos>> GetObrasVehiculos()
        {
            var obravehiculo = await _context.OP_Vehiculos
                .Include(i => i.Vehiculo)
                .Include(i => i.ObrasPublicas)
                .Where(w => !w.Archivado)
                .ToArrayAsync();
            return Ok(obravehiculo);
        }

        [HttpGet("OpVehiculoId")]
        public async Task<ActionResult> GetObraVehiculosById(int OpVehiculoId)
        {
            var obravehiculo = await _context.OP_Vehiculos
                .Include(i => i.Vehiculo)
                .Include(i => i.ObrasPublicas)
                .FirstOrDefaultAsync(f => f.OpVehiculoId == OpVehiculoId);
            if (obravehiculo == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(obravehiculo);
        }

        [HttpPost]
        public async Task<ActionResult<OP_Vehiculos>> PostObraVehiculos(OP_Vehiculos opVehiculos)
        {
            object value = _context.OP_Vehiculos.Add(opVehiculos);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos de obra publicas creado correctamente");
        }

        [HttpPut("{OpVehiculoId}")]
        public async Task<ActionResult> PutObraVehiculos(int OpVehiculoId, OP_Vehiculos opVehiculos)
        {
            if (opVehiculos.OpVehiculoId != OpVehiculoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var obravehiculo = await _context.OP_Vehiculos.FirstOrDefaultAsync(f => f.OpVehiculoId == OpVehiculoId);
            if (obravehiculo == null)
            {
                return BadRequest("El Registro del vehiculo alumbrado no existe");
            }

            obravehiculo.OpVehiculoId = OpVehiculoId;
            obravehiculo.VehiculoId = opVehiculos.VehiculoId;
            obravehiculo.ObraId = opVehiculos.ObraId;
            obravehiculo.Archivado = opVehiculos.Archivado;

            _context.OP_Vehiculos.Update(obravehiculo);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos de obras publicas actualizado correctamente");
        }

        [HttpDelete("{OpVehiculoId}")]
        public async Task<IActionResult> DeleteObraVehiculos(int OpVehiculoId)
        {
            var obravehiculo = _context.OP_Vehiculos
                .FirstOrDefault(f => f.OpVehiculoId == OpVehiculoId);
            if (obravehiculo == null)
            {
                return NotFound();
            }

            obravehiculo.Archivado = true;
            _context.OP_Vehiculos.Update(obravehiculo);
            await _context.SaveChangesAsync();
            return Ok("Vehiculos de obras publicas Archivado");
        }
    }
}
