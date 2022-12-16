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
                .Where(w => !w.Archivado)
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
            producto.CantidadFaltante = 0;
            _context.Medicamento.Add(producto);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Medicamento credo exitosamente");
        }
        [HttpPut("SurtirProducto/{ProductoId}/{Cantidad}")]
        public async Task<IActionResult> SurtirMedicamento(int ProductoId, int Cantidad)
        {
            var medicamento = await _context.Medicamento
                .FirstOrDefaultAsync(f => f.ProductoId == ProductoId);
            if (Cantidad <= medicamento.CantidadFaltante)
            {
                medicamento.CantidadFaltante -= Cantidad;
                medicamento.CantidadDisponible += Cantidad;
                _context.Medicamento.Update(medicamento);
                await _context.SaveChangesAsync();
                return StatusCode(200, "Medicamento credo exitosamente");
            }
            else
            {
                var med = medicamento.CantidadFaltante -= Cantidad;
                med *= -1;
                medicamento.CantidadFaltante = 0;
                medicamento.CantidadDisponible += med;
                _context.Medicamento.Update(medicamento);
                await _context.SaveChangesAsync();
                return StatusCode(200, "Medicamento credo exitosamente");
            }
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
            Medicamento.Nombre = producto.Nombre;
            Medicamento.Contenido = producto.Contenido;
            Medicamento.FechaVencimiento = producto.FechaVencimiento;
            Medicamento.CantidadDisponible = producto.CantidadDisponible;
            Medicamento.CantidadFaltante = producto.CantidadFaltante;
            Medicamento.Archivado = producto.Archivado;
            if (Medicamento.CantidadFaltante > 0)
            {
                if (Medicamento.CantidadDisponible >= Medicamento.CantidadFaltante)
                {
                    Medicamento.CantidadFaltante = 0;
                    _context.Medicamento.Update(Medicamento);
                    await _context.SaveChangesAsync();
                    return Ok("Medicamento actualizado correctamente");
                }
                else
                {
                Medicamento.CantidadFaltante = Medicamento.CantidadFaltante - Medicamento.CantidadDisponible;
                _context.Medicamento.Update(Medicamento);
                await _context.SaveChangesAsync();
                return Ok("Medicamento actualizado correctamente");
                }
            }
            return BadRequest();
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
