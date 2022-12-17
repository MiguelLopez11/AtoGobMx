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
    public class CoordenadaRutaController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        public CoordenadaRutaController(AtoGobMxContext context)
        {
            _context = context;
        }

        [HttpGet("{RutaId}")]
        public async Task<ActionResult<Coordenadas_Rutas>> GetRuta(int RutaId)
        {
            var Ruta = await _context.Coordenadas_Ruta
                .Where(w => w.RutaId == RutaId)
                .ToListAsync();
            return Ok(Ruta);
        }

        [HttpGet("{CoordenadaId}")]
        public async Task<ActionResult> GetRutaById(int CoordenadaId)
        {
            var coordenadas = await _context.Coordenadas_Ruta
                .FirstOrDefaultAsync(f => f.CoordenadaId == CoordenadaId);
            if (coordenadas == null)
            {
                return NotFound();
            }
            return Ok(coordenadas);
        }

        [HttpPost]
        public async Task<ActionResult<Ruta>> PostRuta(Coordenadas_Rutas coordenadas_Rutas)
        {
            object value = _context.Coordenadas_Ruta.Add(coordenadas_Rutas);
            await _context.SaveChangesAsync();
            return Ok("coordenadas creadas correctamente");
        }

        [HttpPut("{CoordenadaId}")]
        public async Task<ActionResult> PutRuta(int CoordenadaId, Coordenadas_Rutas coordenadas_Ruta)
        {
            if (coordenadas_Ruta.RutaId != CoordenadaId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var coordenadas = await _context.Coordenadas_Ruta
                .FirstOrDefaultAsync(f => f.CoordenadaId == CoordenadaId);
            if (coordenadas == null)
            {
                return BadRequest("El Registro de la rutas de aseo no existe");
            }

            coordenadas.CoordenadaId = coordenadas_Ruta.CoordenadaId;
            coordenadas.Latitud = coordenadas_Ruta.Latitud;
            coordenadas.Longitud = coordenadas_Ruta.Longitud;
            coordenadas.RutaId = coordenadas_Ruta.RutaId;

            _context.Coordenadas_Ruta.Update(coordenadas);
            await _context.SaveChangesAsync();
            return Ok("Coordenadas actualizadas correctamente");
        }

        [HttpDelete("{CoordenadaId}")]
        public async Task<IActionResult> DeleteRuta(int CoordenadaId)
        {
            var RutaAseo = _context.Coordenadas_Ruta
                .FirstOrDefault(f => f.CoordenadaId == CoordenadaId);
            if (RutaAseo == null)
            {
                return NotFound();
            }

            _context.Coordenadas_Ruta.Remove(RutaAseo);
            await _context.SaveChangesAsync();
            return Ok("Coordenadas Archivadas");
        }
    }
}
