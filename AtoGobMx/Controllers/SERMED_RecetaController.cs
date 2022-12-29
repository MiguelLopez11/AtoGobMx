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
                .Include(i => i.ProductosReceta)
                .FirstOrDefaultAsync(f => f.RecetaId == RecetaId);
            if (Receta == null)
            {
                return BadRequest("No se encuentran citas registradas");
            }
            var products = await _context.ProductoReceta
                .Include(i => i.Producto)
                .Where(w => w.RecetaId == RecetaId)
                .ToListAsync();
            if(products == null)
            {
                return BadRequest("Receta vacía");
            }
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions { Headless = true }
            );
            var htmlContent = $"<!doctype html>\r\n<html lang=\"en\">\r\n\r\n<head>\r\n    <meta charset=\"utf-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n    <link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\"\r\n        integrity=\"sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65\" crossorigin=\"anonymous\">\r\n</head>\r\n<style type=\"text/css\">\r\n    footer {{\r\n        display: block;\r\n        text-align: center;\r\n        margin-top: 100px;\r\n    }}\r\n\r\n    #Fecha {{\r\n        float: right;\r\n    }}\r\n\r\n    body {{\r\n        height: auto;\r\n    }}\r\n</style>\r\n\r\n<body>\r\n    <div class=\"container-fluid text-center\">\r\n        <div>\r\n\r\n            <div>\r\n                <table class=\"table w-25\" id=\"Fecha\">\r\n                    <thead>\r\n                        <tr>\r\n                            <th scope=\"col\">Fecha:</th>\r\n                            <td>{Receta.FechaAlta}</td>\r\n                        </tr>\r\n                    </thead>\r\n                </table>\r\n            </div>\r\n            <div>\r\n                <div class=\"row\">\r\n                    <div class=\"col\">\r\n                        <table class=\"table w-50 mt-5\">\r\n                            <thead>\r\n                                <tr>\r\n                                    <th scope=\"col\">Nombre:</th>\r\n                                    <td>{Receta.Empleados.NombreCompleto}</td>\r\n                                </tr>\r\n                            </thead>\r\n                        </table>\r\n                    </div>\r\n                </div>\r\n                <div class=\"row\">\r\n                    <div class=\"col\">\r\n                        <table class=\"table mt-4\">\r\n                            <thead>\r\n                                <tr>\r\n                                    <th scope=\"col\">Diagnostico</th>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n                                <tr>\r\n                                    <td>{Receta.diagnostico}</td>\r\n                                </tr>\r\n                            </tbody>\r\n                        </table>\r\n                    </div>\r\n                </div>\r\n                <div class=\"row\">\r\n                    <div class=\"col\">\r\n                        <table class=\"table mt-4\">\r\n                            <thead>\r\n                                <tr>\r\n                                    <th scope=\"col\">Medicamento</th>\r\n                                    <th scope=\"col\">Contenido</th>\r\n                                    <th scope=\"col\">Cantidad</th>\r\n                                    <th scope=\"col\">Sugerencias</th>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>";
            var productosContent = "";
            foreach (var product in products)
            {
                productosContent += $" <tr>\r\n                                    <td>{product.Producto.Nombre}</td>\r\n                                    <td> {product.Producto.Contenido}</td>\r\n                                    <td>{product.cantidad}</td>\r\n                                    <td>{product.Descripcion}</td>\r\n                                </tr>";
            }
            htmlContent += productosContent;
            htmlContent += " </tbody>\r\n                        </table>\r\n                    </div>\r\n                </div>\r\n                <div class=\"row\">\r\n                    <div class=\"col\">\r\n                        <div id=\"Firma\">\r\n\r\n\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <footer>\r\n            <hr class=\"\">\r\n            <p>Firma</p>\r\n        </footer>\r\n    </div>\r\n</body>\r\n\r\n</html>";
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync(htmlContent + "<hr>" + htmlContent);
            var pdfContent = await page.PdfStreamAsync(
                new PdfOptions
                {
                    Format = PaperFormat.A4,
                    PrintBackground = true,
                    MarginOptions = new MarginOptions
                    {
                        Top = "20px",
                        Right = "20px",
                        Bottom = "40px",
                        Left = "20px"
                    }
                });
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
