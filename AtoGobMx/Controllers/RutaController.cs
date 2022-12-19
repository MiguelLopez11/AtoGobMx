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
    public class RutaController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public RutaController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Ruta>> GetRuta()
        {
            var Ruta = await _context.Ruta
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<Ruta>(s))
                .ToArrayAsync();
            return Ok(Ruta);
        }

        [HttpGet("{RutaId}")]
        public async Task<ActionResult> GetRutaById(int RutaId)
        {
            var rutaaseo = await _context.Ruta
                .FirstOrDefaultAsync(f => f.RutaId == RutaId);
            if (rutaaseo == null)
            {
                return NotFound();
            }
            return Ok(rutaaseo);
        }

        [HttpPost]
        public async Task<ActionResult<Ruta>> PostRuta(Ruta ruta)
        {
            _context.Ruta.Add(ruta);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRutaById", new { RutaId = ruta.RutaId }, ruta);
        }

        [HttpPut("{RutaId}")]
        public async Task<ActionResult> PutRuta(int RutaId, Ruta ruta)
        {
            if (ruta.RutaId != RutaId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var rutaAseo = await _context.Ruta.FirstOrDefaultAsync(f => f.RutaId == RutaId);
            if (rutaAseo == null)
            {
                return BadRequest("El Registro de la rutas de aseo no existe");
            }

            rutaAseo.RutaId = RutaId;
            rutaAseo.Nombre = ruta.Nombre;
            rutaAseo.Horario = ruta.Horario;
            rutaAseo.Observacion = ruta.Observacion;
            rutaAseo.Archivado = ruta.Archivado;

            _context.Ruta.Update(rutaAseo);
            await _context.SaveChangesAsync();
            return Ok("Ruta aseo actualizado correctamente");
        }

        [HttpDelete("{RutaId}")]
        public async Task<IActionResult> DeleteRuta(int RutaId)
        {
            var RutaAseo = _context.Ruta
                .FirstOrDefault(f => f.RutaId == RutaId);
            if (RutaAseo == null)
            {
                return NotFound();
            }
            var coordenadas = await _context.Coordenadas_Ruta
                .Where(f => f.RutaId == RutaId)
                .ToListAsync();
            foreach(var coord in coordenadas)
            {
                coord.Archivado = true;
                _context.Coordenadas_Ruta.Update(coord);
            }
            RutaAseo.Archivado = true;
            _context.Ruta.Update(RutaAseo);
            await _context.SaveChangesAsync();
            return Ok("Ruta aseo Archivado");
        }
    }
}
