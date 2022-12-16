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
    public class InventarioAlumbradoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper mapper;

        public InventarioAlumbradoController(AtoGobMxContext context, IMapper mapper)
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
                .FirstOrDefaultAsync(f => f.InventarioAlumbradoId == InventarioAlumbradoId);
            if (inventarioalum == null)
            {
                return NotFound();
            }
            return Ok(inventarioalum);

        }

        [HttpPost()]
        public async Task<ActionResult<InventarioAlumbrado>> PostInventarioAlumbrado(InventarioAlumbrado inventarioAlumbrado)
        {

            object value = _context.InventarioAlumbrado.Add(inventarioAlumbrado);
            await _context.SaveChangesAsync();
            return Ok("Inventario creado correctamente");

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

            inventarioalum.InventarioAlumbradoId = InventarioAlumbradoId;
            //inventarioalum.Tarea = inventarioAlumbrado.Tarea;
            //inventarioalum.FechaAlta = inventarioAlumbrado.FechaAlta;
            //inventarioalum.FechaBaja = inventarioAlumbrado.FechaBaja;
            //inventarioalum.Domicilio = inventarioAlumbrado.Domicilio;


            _context.InventarioAlumbrado.Update(inventarioalum);
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
