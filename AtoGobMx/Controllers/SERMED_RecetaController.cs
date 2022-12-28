using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SERMED_RecetaController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public SERMED_RecetaController(AtoGobMxContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SERMED_Receta>>> GetRecetas()
        {
            var Recetas = await _context.Receta
                .Include(i => i.Empleados)
                .Include(i => i.EstatusReceta)
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Recetas == null)
            {
                return BadRequest("No se encuentran citas registradas");
            }
            return Ok(Recetas);
        }

        [HttpGet("/SERMER_Receta/Download/{RecetaId}")]
        public async Task<ActionResult<IEnumerable<SERMED_Receta>>> DownloadReceta(int RecetaId)
        {
            var Receta = await _context.Receta
                .Include(i => i.Empleados)
                .Include(i => i.EstatusReceta)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.RecetaId == RecetaId);
            if (Receta == null)
            {
                return BadRequest("No se encuentran citas registradas");
            }
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions { Headless = true }
            );
            var htmlContent = $"";
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync($"");
            var pdfContent = await page.PdfStreamAsync(
                new PdfOptions { Format = PaperFormat.A4, PrintBackground = true }
            );
            return File(pdfContent, "application/pdf", "converted.pdf");
        }

        [HttpGet("Pendientes")]
        public async Task<ActionResult<IEnumerable<SERMED_Receta>>> GetRecetasPendientes()
        {
            var Recetas = await _context.Receta
                .Include(i => i.Empleados)
                .Include(i => i.EstatusReceta)
                .Where(w => w.EstatusReceta.Nombre == "Pendiente")
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Recetas == null)
            {
                return BadRequest("No se encuentran citas registradas");
            }
            return Ok(Recetas);
        }

        [HttpGet("{RecetaId}")]
        public async Task<ActionResult<SERMED_Cita>> GetRecetaById(int RecetaId)
        {
            var Receta = await _context.Receta
                .Include(i => i.Empleados)
                .Include(i => i.EstatusReceta)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.RecetaId == RecetaId);

            if (Receta == null)
            {
                return NotFound();
            }

            return Ok(Receta);
        }

        [HttpGet("empleado/{EmpleadoId}")]
        public async Task<ActionResult<SERMED_Cita>> GetRecetaByEmpleadoID(int EmpleadoId)
        {
            var Recetas = await _context.Receta
                .Include(i => i.Empleados)
                .Include(i => i.EstatusReceta)
                .Where(w => !w.Archivado)
                .Where(w => w.EmpleadoId == EmpleadoId)
                .ToListAsync();

            return Ok(Recetas);
        }

        [HttpPost]
        public async Task<ActionResult<SERMED_Receta>> PostInventario(SERMED_Receta receta)
        {
            var estatus = await _context.EstatusReceta
                .Where(w => w.Nombre == "Pendiente")
                .FirstOrDefaultAsync();
            receta.FechaAlta = DateTime.Now;
            receta.EstatusRecetaId = estatus.EstatusRecetaId;
            _context.Receta.Add(receta);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRecetaById", new { RecetaId = receta.RecetaId }, receta);
        }

        [HttpPost("SurtirReceta/{RecetaId}")]
        public async Task<ActionResult<SERMED_Receta>> SurtirReceta(int RecetaId)
        {
            var receta = await _context.Receta.FirstOrDefaultAsync(f => f.RecetaId == RecetaId);
            var productoReceta = await _context.ProductoReceta
                .Include(i => i.Receta)
                .Where(f => f.RecetaId == RecetaId)
                .ToListAsync();
            if (productoReceta.Count == 0)
            {
                return BadRequest();
            }
            for (int i = 0; i < productoReceta.Count; i++)
            {
                var product = await _context.Medicamento.FirstOrDefaultAsync(
                    f => f.ProductoId == productoReceta[i].ProductoId
                );
                if (product.CantidadDisponible < productoReceta[i].cantidad)
                {
                    var faltante = (product.CantidadDisponible) - (productoReceta[i].cantidad);
                    faltante = faltante * -1;
                    product.CantidadDisponible = 0;
                    product.CantidadFaltante = product.CantidadFaltante + faltante;
                    _context.Medicamento.Update(product);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    product.CantidadDisponible -= productoReceta[i].cantidad;
                    _context.Medicamento.Update(product);
                    await _context.SaveChangesAsync();
                }
            }
            receta.EstatusRecetaId = 2;
            _context.Receta.Update(receta);
            await _context.SaveChangesAsync();
            return Ok("Receta surtida");
        }

        [HttpPut("{RecetaId}")]
        public async Task<IActionResult> PutCategoria(int RecetaId, SERMED_Receta receta)
        {
            if (receta.RecetaId != RecetaId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var Receta = await _context.Receta.FindAsync(RecetaId);
            if (Receta == null)
            {
                return NotFound();
            }
            Receta.RecetaId = receta.RecetaId;
            Receta.EmpleadoId = receta.EmpleadoId;
            Receta.diagnostico = receta.diagnostico;
            //Cita.Hora = cita.Hora;
            Receta.FechaAlta = receta.FechaAlta;
            Receta.Archivado = receta.Archivado;

            _context.Receta.Update(Receta);
            await _context.SaveChangesAsync();
            return Ok("Cita actualizada correctamente");
        }

        [HttpDelete("{RecetaId}")]
        public async Task<IActionResult> DeleteInventario(int RecetaId)
        {
            var receta = await _context.Receta.FirstOrDefaultAsync(f => f.RecetaId == RecetaId);
            if (receta == null)
            {
                return NotFound();
            }
            receta.Archivado = true;
            _context.Receta.Update(receta);
            await _context.SaveChangesAsync();
            return Ok("Cita archivada");
        }
    }
}
