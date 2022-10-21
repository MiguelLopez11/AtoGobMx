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
    public class ZonaController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public ZonaController(AtoGobMxContext Context, IMapper mapper)
        {
            _context = Context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Zona>> GetZona()
        {
            var zona = await _context.Zona
                
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<Zona>(s))
                .ToArrayAsync();
            return Ok(zona);
        }

        [HttpGet("ZonaId")]
        public async Task<ActionResult> GetZonaById(int ZonaId)
        {
            var zona = await _context.Zona
                //.Include(i => i.TareaTipoAlumbrado)
                //.Include(i => i.Estatus)
                .FirstOrDefaultAsync(f => f.zonaId == ZonaId);
            if (zona == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(zona);
        }

        [HttpPost]
        public async Task<ActionResult<Zona>> PostZona(Zona zona)
        {
            object value = _context.Zona.Add(zona);
            await _context.SaveChangesAsync();
            return Ok("Servicio de zona creado correctamente");
        }

        [HttpPut("{ZonaId}")]
        public async Task<ActionResult> PutZona(int ZonaId, Zona zona)
        {
            if (zona.zonaId != ZonaId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var zonaservicios = await _context.Zona.FirstOrDefaultAsync(f => f.zonaId == ZonaId);
            if (zonaservicios == null)
            {
                return BadRequest("El Registro la zona no existe");
            }

            zonaservicios.zonaId = ZonaId;
            zonaservicios.Nombre = zona.Nombre;
            zonaservicios.Descripcion = zona.Descripcion;
            zonaservicios.Archivado = zona.Archivado;

            _context.Zona.Update(zonaservicios);
            await _context.SaveChangesAsync();
            return Ok("Servicios de zonas actualizado correctamente");
        }

        [HttpDelete("{ZonaId}")]
        public async Task<IActionResult> DeleteZona(int ZonaId)
        {
            var zona = _context.Zona
                .FirstOrDefault(f => f.zonaId == ZonaId);
            if (zona == null)
            {
                return NotFound();
            }

            zona.Archivado = true;
            _context.Zona.Update(zona);
            await _context.SaveChangesAsync();
            return Ok("Servicios de zona Archivado");
        }
    }
}
