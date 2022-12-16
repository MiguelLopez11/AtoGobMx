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
    public class DireccionCementerioController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public DireccionCementerioController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<DireccionCementerio>> GetDireccionCementerios()
        {
            var direccion = await _context.DireccionCementerio
                //.OrderBy(o => o.NombreCementerio)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<DireccionCementerio>(s))
                .ToArrayAsync();
            return Ok(direccion);
        }

        [HttpGet("{DireccionId}")]
        public async Task<ActionResult> GetDireccionCementeriosById(int DireccionId)
        {
            var direccion = await _context.DireccionCementerio
                .FirstOrDefaultAsync(f => f.DireccionId == DireccionId);
            if (direccion == null)
            {
                return NotFound();
            }
            return Ok(direccion);
        }

        [HttpPost]
        public async Task<ActionResult> PostDireccionCementerios(DireccionCementerio direccioncementerio)
        {
            _context.DireccionCementerio.Add(direccioncementerio);
            await _context.SaveChangesAsync();
            return Ok("Direccion cementerio creada correcta mente");
        }

        [HttpPut("{DireccionId}")]
        public async Task<ActionResult> PutDireccionCementerios(int DireccionId, DireccionCementerio direccioncementerio)
        {
            if (direccioncementerio.DireccionId != DireccionId)
            {
                return Ok("Lo ID no ingresados no coinciden");
            }

            var direccion = await _context.DireccionCementerio.FirstOrDefaultAsync(f => f.DireccionId == DireccionId);
            if (direccion == null)
            {
                return BadRequest("El registro de la direccion del cementerio no existe");
            }

            direccion.DireccionId = DireccionId;
            direccion.NombreCementerio = direccioncementerio.NombreCementerio;
            direccion.Municipio = direccioncementerio.Municipio;
            direccion.Localidad = direccioncementerio.Localidad;
            direccion.Calle = direccioncementerio.Calle;
            direccion.Latitud = direccioncementerio.Latitud;
            direccion.Longitud = direccioncementerio.Longitud;
            direccion.Archivado = direccioncementerio.Archivado;

            _context.DireccionCementerio.Update(direccion);
            await _context.SaveChangesAsync();
            return Ok("Direccion cementerio se actualizo correcta mente");
        }

        [HttpDelete("{DireccionId}")]
        public async Task<ActionResult> DeleteDireccionCementerios(int DireccionId)
        {
            var direccioncemen = _context.DireccionCementerio
                .FirstOrDefault(f => f.DireccionId == DireccionId);
            if (direccioncemen == null)
            {
                return NotFound();
            }

            direccioncemen.Archivado = true;
            _context.DireccionCementerio.Update(direccioncemen);
            await _context.SaveChangesAsync();
            return Ok("Direccion cementerio archivado");
        }
    }
}
