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
    public class InventarioAlumbradoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper mapper;

        public InventarioAlumbradoController (AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<InventarioAlumbrado>> GetInventarioAlumbrado()
        {
            var inventario = await _context.InventarioAlumbrado
                .Where(w => !w.Archivado)
                .OrderBy(o => o.InventarioAlumbradoId)
                .ToListAsync();
            return Ok(inventario);
        }

        [HttpGet("{InventarioAlumbradoId}")]
        public async Task<ActionResult> GetInventarioAlumbradoById(int InventarioAlumbradoId)
        {
            var inventarioalum = await _context.InventarioAlumbrado
                .Include(i => i.Alumbrado)
                .FirstOrDefaultAsync(f => f.InventarioAlumbradoId == InventarioAlumbradoId);
            if (inventarioalum == null)
            {
                return NotFound();
            }
            return Ok(inventarioalum);
        }

        [HttpPost()]
        public async Task<ActionResult<InventarioAlumbrado>> PostInventarioAlumbrado(InventarioAlumbrado inventarioAlumbrado, int AlumbradoId)
        {
            var alumbrado = await _context.Alumbrado.FirstOrDefaultAsync(f => f.AlumbradoId == AlumbradoId);
            _context.InventarioAlumbrado.Add(inventarioAlumbrado);

            var inventario = CreatedAtAction("GetInventarioAlumbradoById", new { InventarioAlumbradoId = inventarioAlumbrado.InventarioAlumbradoId }, inventarioAlumbrado);
            alumbrado.inventarioAlumbradoId = inventarioAlumbrado.InventarioAlumbradoId;
            await _context.SaveChangesAsync();
            return Ok(inventarioAlumbrado);
        }

        [HttpPut("{InventarioAlumbradoId}")]
        public async Task<ActionResult> PutInventarioAlumbrado(int InventarioAlumbradoId, InventarioAlumbrado inventarioAlumbrado)
        {
            if (inventarioAlumbrado.InventarioAlumbradoId != InventarioAlumbradoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var inventarioalum = _context.InventarioAlumbrado.Find(InventarioAlumbradoId);
            if (inventarioalum == null)
            {
                return BadRequest("El Registro del inventario alumbrado no existe");
            }

            inventarioAlumbrado.InventarioAlumbradoId = inventarioalum.InventarioAlumbradoId;
            inventarioAlumbrado.Tarea = inventarioalum.Tarea;
            inventarioAlumbrado.FechaAlta = inventarioalum.FechaAlta;
            inventarioAlumbrado.FechaBaja = inventarioalum.FechaBaja;
            inventarioAlumbrado.Domicilio = inventarioalum.Domicilio;


            _context.InventarioAlumbrado.Update(inventarioAlumbrado);
            await _context.SaveChangesAsync();
            return Ok("Tipo de inventario alumbrado actualizado correctamente");
        }
        [HttpDelete("{InventarioAlumbradoId}")]
        public async Task<ActionResult> DeleteInventarioAlumbrado(int InventarioAlumbradoId)
        {
            var inventario = _context.InventarioAlumbrado
                .FirstOrDefault(f => f.InventarioAlumbradoId == InventarioAlumbradoId);
            if (inventario == null)
            {
                return NotFound();
            }

            inventario.Archivado = true;
            _context.InventarioAlumbrado.Update(inventario);
            await _context.SaveChangesAsync();
            return Ok("Tipo de inventario alumbrado Archivado");
        }
    }
}
