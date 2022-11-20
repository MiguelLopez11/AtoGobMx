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
    public class TipoValesController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public TipoValesController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PROV_TipoVales>> GetTipoVales()
        {
            var tipovale = await _context.TipoVales
                .OrderBy(o => o.TipoId)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<PROV_TipoVales>(s))
                .ToArrayAsync();
            return Ok(tipovale);
        }

        [HttpGet("{TipoId}")]
        public async Task<ActionResult> GetTipoValesById(int TipoId)
        {
            var tipovale = await _context.TipoVales
                .FirstOrDefaultAsync(f => f.TipoId == TipoId);
            if (tipovale == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(tipovale);
        }

        [HttpPost]
        public async Task<ActionResult<PROV_TipoVales>> PostTipoVales(PROV_TipoVales tipoVales)
        {
            object value = _context.TipoVales.Add(tipoVales);
            await _context.SaveChangesAsync();
            return Ok("Tipo de vale creado correctamente");
        }

        [HttpPut("{TipoId}")]
        public async Task<ActionResult> PutTipoVales(int TipoId, PROV_TipoVales tipoVales)
        {
            if (tipoVales.TipoId != TipoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var tipovale = await _context.TipoVales.FirstOrDefaultAsync(f => f.TipoId == TipoId);
            if (tipovale == null)
            {
                return BadRequest("El Registro del Tipo de vale no existe");
            }

            tipovale.TipoId = TipoId;
            tipovale.Nombre = tipoVales.Nombre;
            tipovale.Descripcion = tipoVales.Descripcion;
            tipovale.Archivado = tipoVales.Archivado;

            _context.TipoVales.Update(tipovale);
            await _context.SaveChangesAsync();
            return Ok("Tipo de vale actualizado correctamente");
        }

        [HttpDelete("{TipoId}")]
        public async Task<IActionResult> DeleteTipoVales(int TipoId)
        {
            var tipovale = _context.TipoVales
                .FirstOrDefault(f => f.TipoId == TipoId);
            if (tipovale == null)
            {
                return NotFound();
            }

            tipovale.Archivado = true;
            _context.TipoVales.Update(tipovale);
            await _context.SaveChangesAsync();
            return Ok("Tipo de vale Archivado");
        }
    }
}
