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
    public class PROV_DetalleProductoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public PROV_DetalleProductoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PROV_DetalleProducto>> GetDetalleProducto()
        {
            var detalleproducto = await _context.PROV_DetalleProducto
                .Include(i => i.PROV_Producto)
                .Include(i => i.PROV_DetalleVale)
                .OrderBy(o => o.DetalleProductoId)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<PROV_DetalleProducto>(s))
                .ToArrayAsync();
            return Ok(detalleproducto);
        }

        [HttpGet("{DetalleProductoId}")]
        public async Task<ActionResult> GetDetalleProductoById(int DetalleProductoId)
        {
            var detalleproducto = await _context.PROV_DetalleProducto
                .Include(i => i.PROV_Producto)
                .Include(i => i.PROV_DetalleVale)
                .FirstOrDefaultAsync(f => f.DetalleProductoId == DetalleProductoId);
            if (detalleproducto == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(detalleproducto);
        }

        [HttpPost]
        public async Task<ActionResult<PROV_DetalleProducto>> PostDetalleProducto(PROV_DetalleProducto detalleProducto)
        {
            object value = _context.PROV_DetalleProducto.Add(detalleProducto);
            await _context.SaveChangesAsync();
            return Ok("Detalle producto creado correctamente");
        }

        [HttpPut("{DetalleProductoId}")]
        public async Task<ActionResult> PutDetalleProducto(int DetalleProductoId, PROV_DetalleProducto detalleProducto)
        {
            if (detalleProducto.DetalleProductoId != DetalleProductoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var detalleproducto = await _context.PROV_DetalleProducto.FirstOrDefaultAsync(f => f.DetalleProductoId == DetalleProductoId);
            if (detalleproducto == null)
            {
                return BadRequest("El Registro del detalle producto no existe");
            }

            detalleproducto.DetalleProductoId = DetalleProductoId;
            detalleproducto.ProductoId = detalleProducto.ProductoId;
            detalleproducto.DetalleProductoId = detalleProducto.DetalleProductoId;
            detalleproducto.Archivado = detalleProducto.Archivado;

            _context.PROV_DetalleProducto.Update(detalleproducto);
            await _context.SaveChangesAsync();
            return Ok("Detalle producto actualizado correctamente");
        }

        [HttpDelete("{DetalleProductoId}")]
        public async Task<IActionResult> DeleteDetalleProducto(int DetalleProductoId)
        {
            var detalleproducto = _context.PROV_DetalleProducto
                .FirstOrDefault(f => f.DetalleProductoId == DetalleProductoId);
            if (detalleproducto == null)
            {
                return NotFound();
            }

            detalleproducto.Archivado = true;
            _context.PROV_DetalleProducto.Update(detalleproducto);
            await _context.SaveChangesAsync();
            return Ok("Detallle producto Archivado");
        }
    }
}
