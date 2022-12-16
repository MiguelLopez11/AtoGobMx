using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SERMED_ProductoRecetaController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public SERMED_ProductoRecetaController(AtoGobMxContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SERMED_ProductosReceta>>> GetMedicamento()
        {
            var productos = await _context.ProductoReceta
                .Include(i => i.Producto)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(productos);
        }

        [HttpGet("{productoRecetaId}")]
        public async Task<ActionResult<SERMED_Producto>> GetMedicamentoById(int productoRecetaId)
        {
            var productoReceta = await _context.ProductoReceta
                .Include(i => i.Receta)
                .Include(i => i.Producto)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.ProductoRecetaId == productoRecetaId);

            if (productoReceta == null)
            {
                return NotFound();
            }
            return Ok(productoReceta);
        }
        [HttpGet("Receta/{RecetaId}")]
        public async Task<ActionResult<SERMED_Producto>> GetMedicamentoByRecetaId(int RecetaId)
        {
            var productoReceta = await _context.ProductoReceta
                .Include(i => i.Receta)
                .Include(i => i.Producto)
                .Where(w => !w.Archivado)
                .Where(w => w.RecetaId == RecetaId)
                .ToListAsync();

            if (productoReceta == null)
            {
                return NotFound();
            }

            return Ok(productoReceta);
        }
        [HttpPost]
        public async Task<ActionResult<SERMED_ProductosReceta>> PostMedicamento(SERMED_ProductosReceta productosReceta)
        {
            _context.ProductoReceta.Add(productosReceta);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Medicamento Se ha registrado exitosamente");
        }
        [HttpPut("{productoRecetaId}")]
        public async Task<IActionResult> PutMedicamento(int productoRecetaId, SERMED_ProductosReceta productoReceta)
        {
            if (productoReceta.ProductoRecetaId != productoRecetaId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var producto_Receta = await _context.ProductoReceta.FindAsync(productoRecetaId);
            if (producto_Receta == null)
            {
                return NotFound();
            }
            producto_Receta.ProductoRecetaId = productoReceta.ProductoRecetaId;
            producto_Receta.ProductoId = productoReceta.ProductoId;
            producto_Receta.RecetaId = productoReceta.RecetaId;
            producto_Receta.cantidad = productoReceta.cantidad;
            producto_Receta.Descripcion = productoReceta.Descripcion;
            producto_Receta.Archivado = productoReceta.Archivado;

            _context.ProductoReceta.Update(producto_Receta);
            await _context.SaveChangesAsync();
            return Ok("medicamento actualizado correctamente");
        }
        [HttpDelete("{productoRecetaId}")]
        public async Task<IActionResult> DeleteMedicamento(int productoRecetaId)
        {
            var Medicamento = await _context.ProductoReceta
                .FirstOrDefaultAsync(f => f.ProductoRecetaId == productoRecetaId);
            if (Medicamento == null)
            {
                return NotFound();
            }
            Medicamento.Archivado = true;
            _context.ProductoReceta.Update(Medicamento);
            await _context.SaveChangesAsync();
            return Ok("Medicamento archivada");
        }
        [HttpDelete("Receta/{RecetaId}/{productoRecetaId}")]
        public async Task<IActionResult> DeleteMedicamento(int RecetaId,  int productoRecetaId)
        {
            var Medicamento = await _context.ProductoReceta
                .Where(w => w.RecetaId == RecetaId)
                .FirstOrDefaultAsync(w => w.ProductoRecetaId == productoRecetaId);
            if (Medicamento == null)
            {
                return NotFound();
            }
            Medicamento.Archivado = true;
            _context.ProductoReceta.Update(Medicamento);
            await _context.SaveChangesAsync();
            return Ok("Medicamento archivada");
        }
    }
}
