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
    public class PROV_ProductoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public PROV_ProductoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PROV_Producto>> GetProducto()
        {
            var producto = await _context.PROV_Producto
                .OrderBy(o => o.ProductoId)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<PROV_Producto>(s))
                .ToArrayAsync();
            return Ok(producto);
        }

        [HttpGet("{ProductoId}")]
        public async Task<ActionResult> GetProductoById(int ProductoId)
        {
            var producto = await _context.PROV_Producto
                .FirstOrDefaultAsync(f => f.ProductoId == ProductoId);
            if (producto == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<PROV_Producto>> PostEstatus(PROV_Producto producto)
        {
            object value = _context.PROV_Producto.Add(producto);
            await _context.SaveChangesAsync();
            return Ok("Producto creado correctamente");
        }

        [HttpPut("{ProductoId}")]
        public async Task<ActionResult> PutProducto(int ProductoId, PROV_Producto producto)
        {
            if (producto.ProductoId != ProductoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var produc = await _context.PROV_Producto.FirstOrDefaultAsync(f => f.ProductoId == ProductoId);
            if (produc == null)
            {
                return BadRequest("El Registro del producto no existe");
            }

            produc.ProductoId = ProductoId;
            produc.Nombre = producto.Nombre;
            produc.Precio = producto.Precio;
            produc.Archivado = producto.Archivado;

            _context.PROV_Producto.Update(produc);
            await _context.SaveChangesAsync();
            return Ok("Producto actualizado correctamente");
        }

        [HttpDelete("{ProductoId}")]
        public async Task<IActionResult> DeleteProducto(int ProductoId)
        {
            var producto = _context.PROV_Producto
                .FirstOrDefault(f => f.ProductoId == ProductoId);
            if (producto == null)
            {
                return NotFound();
            }

            producto.Archivado = true;
            _context.PROV_Producto.Update(producto);
            await _context.SaveChangesAsync();
            return Ok("Producto Archivado");
        }
    }
}
