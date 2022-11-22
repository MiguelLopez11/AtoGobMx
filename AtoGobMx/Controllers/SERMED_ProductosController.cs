using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SERMED_ProductosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public SERMED_ProductosController(AtoGobMxContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SERMED_Producto>>> GetMedicamento()
        {
            var Medicamentos = await _context.Medicamento
                .Where(w => w.Archivado)
                .ToListAsync();
            if (Medicamentos == null)
            {
                return BadRequest("No se encuentran citas registradas");
            }
            return Ok(Medicamentos);
        }

        [HttpGet("{productoId}")]
        public async Task<ActionResult<SERMED_Producto>> GetMedicamentoById(int productoId)
        {
            var Medicamento = await _context.Medicamento
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.ProductoId == productoId);

            if (Medicamento == null)
            {
                return NotFound();
            }

            return Ok(Medicamento);
        }
        [HttpPost]
        public async Task<ActionResult<SERMED_Producto>> PostMedicamento(SERMED_Producto producto)
        {
            _context.Medicamento.Add(producto);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Medicamento credo exitosamente");
        }
        [HttpPut("{productoId}")]
        public async Task<IActionResult> PutMedicamento(int productoId, SERMED_Producto producto)
        {
            if (producto.ProductoId != productoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var Medicamento = await _context.Medicamento.FindAsync(productoId);
            if (Medicamento == null)
            {
                return NotFound();
            }
            Medicamento.ProductoId = producto.ProductoId;
            Medicamento.CategoriaMedicamentoId = producto.CategoriaMedicamentoId;
            Medicamento.Nombre = producto.Nombre;
            Medicamento.Contenido = producto.Contenido;
            Medicamento.FechaVencimiento = producto.FechaVencimiento;
            Medicamento.CantidadDisponible = producto.CantidadDisponible;
            Medicamento.Archivado = producto.Archivado;

            _context.Medicamento.Update(Medicamento);
            await _context.SaveChangesAsync();
            return Ok("Medicamento actualizado correctamente");
        }
        [HttpDelete("{productoId}")]
        public async Task<IActionResult> DeleteMedicamento(int productoId)
        {
            var Medicamento = await _context.Medicamento
                .FirstOrDefaultAsync(f => f.ProductoId == productoId);
            if (Medicamento == null)
            {
                return NotFound();
            }
            Medicamento.Archivado = true;
            _context.Medicamento.Update(Medicamento);
            await _context.SaveChangesAsync();
            return Ok("Medicamento archivada");
        }
    }
}
