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
    public class PROV_DetalleValeController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public PROV_DetalleValeController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PROV_DetalleVale>> GetDetalleVale()
        {
            var detallevale = await _context.PROV_DetalleVale
                .OrderBy(o => o.DetalleValeId)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<PROV_DetalleVale>(s))
                .ToArrayAsync();
            return Ok(detallevale);
        }

        [HttpGet("{DetalleValeId}")]
        public async Task<ActionResult> GetDetalleValeById(int
            DetalleValeId)
        {
            var detallevale = await _context.PROV_DetalleVale
                .FirstOrDefaultAsync(f => f.DetalleValeId == DetalleValeId);
            if (detallevale == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(detallevale);
        }

        [HttpPost]
        public async Task<ActionResult<PROV_DetalleVale>> PostDetalleVale(PROV_DetalleVale detalleVale)
        {
            object value = _context.PROV_DetalleVale.Add(detalleVale);
            await _context.SaveChangesAsync();
            return Ok("Detalle Vale creado correctamente");
        }

        [HttpPut("{DetalleValeId}")]
        public async Task<ActionResult> PutDetalleVale(int DetalleValeId, PROV_DetalleVale detalleVale)
        {
            if (detalleVale.DetalleValeId != DetalleValeId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var detallevale = await _context.PROV_DetalleVale.FirstOrDefaultAsync(f => f.DetalleValeId == DetalleValeId);
            if (detallevale == null)
            {
                return BadRequest("El Registro del detalle vale no existe");
            }

            detallevale.DetalleValeId = DetalleValeId;
            detallevale.Cantidad = detalleVale.Cantidad;
            detallevale.Precio = detalleVale.Precio;
            detallevale.Total = detalleVale.Total;
            detallevale.IVA = detalleVale.IVA;
            detallevale.ProductoId = detalleVale.ProductoId;
            detallevale.Archivado = detalleVale.Archivado;

            _context.PROV_DetalleVale.Update(detallevale);
            await _context.SaveChangesAsync();
            return Ok("Detalle vale actualizado correctamente");
        }

        [HttpDelete("{DetalleValeId}")]
        public async Task<IActionResult> DeleteDetalleVale(int DetalleValeId)
        {
            var detallevale = _context.PROV_DetalleVale
                .FirstOrDefault(f => f.DetalleValeId == DetalleValeId);
            if (detallevale == null)
            {
                return NotFound();
            }

            detallevale.Archivado = true;
            _context.PROV_DetalleVale.Update(detallevale);
            await _context.SaveChangesAsync();
            return Ok("Detalle vale Archivado");
        }
    }
}
