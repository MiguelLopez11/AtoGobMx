using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp.Media;
using PuppeteerSharp;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlDeValesController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public ControlDeValesController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PROV_ControlVales>> GetControlDeVales()
        {
            var controlvale = await _context.ControlDeVales
                .Include(i => i.Departamentos)
                .Include(i => i.PROV_Proveedor)
                .Include(i => i.PROV_EstatusVale)
                .Include(i => i.TipoVales)
                .OrderBy(o => o.ControlValeId)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<PROV_ControlVales>(s))
                .ToArrayAsync();
            return Ok(controlvale);
        }

        [HttpGet("ControlDeVale/Download/{ControlValeId}")]
        public async Task<ActionResult<PROV_ControlVales>> DownloadControlDeVale(int ControlValeId)
        {
            //var departamento = "";
            var vale = await _context.ControlDeVales
                .Include(i => i.Departamentos)
                .Include(i => i.PROV_Proveedor)
                .Include(i => i.PROV_EstatusVale)
                .Include(i => i.TipoVales)
                .Include(i => i.PROV_DetalleVale)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.ControlValeId == ControlValeId);
            //Tomar detalle relacionados al vale
            var detallevale = await _context.PROV_DetalleVale
                .Include(i => i.PROV_Producto)
                .Where(w => w.ControlValeId == ControlValeId)
                .ToListAsync();
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });

            var htmlContent = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>vale</title>\r\n    <link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\"\r\n        integrity=\"sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65\" crossorigin=\"anonymous\" />\r\n</head>\r\n<style>\r\n    .car {{\r\n        width: 90%;\r\n        margin: 0% 0 0 3%;\r\n    }}\r\n\r\n    .line {{\r\n        border-top: 2px solid black;\r\n        width: 102%;\r\n        margin: 1% 0 0 0%;\r\n    }}\r\n\r\n    .tables {{\r\n        width: 18%;\r\n        margin: 0% 0 0 85%;\r\n    }}\r\n\r\n    /* .line2 {{\r\n        border-top: 3px solid orangered;\r\n        width: 33%;\r\n        margin: -5% 0 0 33%;\r\n    }}\r\n\r\n    .line3 {{\r\n        border-top: 3px solid blue;\r\n        width: 33%;\r\n        margin: -51px 0 0 66%;\r\n    }} */\r\n\r\n    .line4 {{\r\n        border-top: 2px solid black;\r\n        \r\n    }}\r\n\r\n    .line5 {{\r\n        border-top: 2px solid black;\r\n    }}\r\n\r\n    th {{\r\n        font-size: 12px;\r\n    }}\r\n\r\n    td {{\r\n        font-size: 12px;\r\n    }}\r\n\r\n    div {{\r\n        font-size: 12px;\r\n    }}\r\n</style>\r\n\r\n<body>\r\n    <div class=\"car\">\r\n        <h1>\r\n            <p style=\"margin: 0% 0 0 75%;\">Vale</p>\r\n            <div class=\"line\"></div>\r\n        </h1>\r\n        <div class=\"row\">\r\n            <div class=\"col\">\r\n                <br>\r\n                <p>Fecha emicion: {vale.FechaEmicion} </p>\r\n\r\n                <p>Dependencia: {vale.Dependencia} </p>\r\n\r\n                <p>Subprograma: {vale.Subprograma} </p>\r\n\r\n                <p>Usuario: {vale.Usuario}</p>\r\n            </div>\r\n            <div class=\"col\">\r\n                <br>\r\n                <p>Fecha vigaencia: {vale.FechaVigencia} </p>\r\n\r\n                <p>Proveedor: {vale.PROV_Proveedor.Nombre} </p>\r\n\r\n                <p>Vale de: {vale.TipoVales.Nombre} </p>\r\n            </div>\r\n        </div>\r\n        <table class=\"table\">\r\n            <thead>\r\n                <tr>\r\n                    <th scope=\"col\">\r\n                        ID\r\n                    </th>\r\n                    <th scope=\"col\">\r\n                        Cantidad\r\n                    </th>\r\n                    <th scope=\"col\">\r\n                        Medida\r\n                    </th>\r\n                    <th scope=\"col\">\r\n                        Producto\r\n                    </th>\r\n                    <th scope=\"col\">\r\n                        Precio\r\n                    </th>\r\n                    <th scope=\"col\">\r\n                        Importe\r\n                    </th>\r\n                    <th>\r\n                        Total\r\n                    </th>\r\n                </tr>\r\n            </thead>\r\n            <tbody class=\"table-group-divider\">";

            var detallevaleHTML = "";
            foreach (var detalle in detallevale)
            {
                detallevaleHTML += $"<tr>\r\n                    <td>\r\n                        {detalle.DetalleValeId}\r\n                    </td>\r\n                    <td>\r\n                        {detalle.Cantidad}\r\n                    </td>\r\n                    <td>\r\n                        {{detalle.detalle.Medida}}\r\n                    </td>\r\n                    <td>\r\n                        {detalle.PROV_Producto.Nombre}\r\n                    </td>\r\n                    <td>\r\n                        {detalle.Precio}\r\n                    </td>\r\n                    <td>\r\n                        {detalle.Importe}\r\n                    </td>\r\n                    <td>\r\n                        {detalle.Total}\r\n                    </td>\r\n                </tr>";
            }
            htmlContent += detallevaleHTML;

            htmlContent += $"</tbody>\r\n        </table>\r\n        <br>\r\n        <br>\r\n        <br>\r\n        <div class=\"row\">\r\n            <div class=\"col\">\r\n                <h4>\r\n                    <div class=\"line4\"></div>\r\n                    <a>{vale.UsuarioAutoriza}</a>\r\n                    <p style=\"text-align: center;\">Autorizo</p>\r\n                </h4>\r\n            </div>\r\n            <div class=\"col\">\r\n                <h4>\r\n                    <div class=\"line5\"></div>\r\n                    <a>{vale.Recibio}</a>\r\n                    <p style=\"text-align: center;\">Recibio</p>\r\n                </h4>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <br> </body>\r\n\r\n</html>";

            var HTMLContent = htmlContent;
            HTMLContent += "\r\n <br> \r\n <hr>";
            HTMLContent += htmlContent;
            HTMLContent += "\r\n <br> \r\n <hr>";
            HTMLContent += htmlContent;
            //HTMLContent += $"</tbody>\r\n        </table>\r\n        <br>\r\n        <br>\r\n        <br>\r\n        <div class=\"row\">\r\n            <div class=\"col\">\r\n                <h4>\r\n                    <div class=\"line4\"></div>\r\n                    <a>{vale.UsuarioAutoriza}</a>\r\n                    <p style=\"text-align: center;\">Autorizo</p>\r\n                </h4>\r\n            </div>\r\n            <div class=\"col\">\r\n                <h4>\r\n                    <div class=\"line5\"></div>\r\n                    <a>{vale.Recibio}</a>\r\n                    <p style=\"text-align: center;\">Recibio</p>\r\n                </h4>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <br> </body>\r\n\r\n</html>";
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync(HTMLContent);
            var pdfContent = await page.PdfStreamAsync(new PdfOptions
            {
                Format = PaperFormat.Legal
            });
            return File(pdfContent, "application/pdf", $"Vale.pdf");
        }

        [HttpGet("{ControlValeId}")]
        public async Task<ActionResult> GetControlDeValesById(int ControlValeId)
        {
            var controlvale = await _context.ControlDeVales
                .Include(i => i.Departamentos)
                .Include(i => i.PROV_Proveedor)
                .Include(i => i.PROV_EstatusVale)
                .Include(i => i.TipoVales)
                .FirstOrDefaultAsync(f => f.ControlValeId == ControlValeId);
            if (controlvale == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(controlvale);
        }

        [HttpPost]
        public async Task<ActionResult<PROV_ControlVales>> PostControlDeVales(PROV_ControlVales controlDeVales)
        {
            _context.ControlDeVales.Add(controlDeVales);
            controlDeVales.FechaEmicion = DateTime.Now;
            _context.ControlDeVales.Add(controlDeVales);
            controlDeVales.FechaVigencia = DateTime.Now;
            object value = _context.ControlDeVales.Add(controlDeVales);
            await _context.SaveChangesAsync();
            return Ok("Control de vales creado correctamente");
        }

        [HttpPut("{ControlValeId}")]
        public async Task<ActionResult> PutControlDeVales(int ControlValeId, PROV_ControlVales controlDeVales)
        {
            if (controlDeVales.ControlValeId != ControlValeId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var controlvale = await _context.ControlDeVales.FirstOrDefaultAsync(f => f.ControlValeId == ControlValeId);
            if (controlvale == null)
            {
                return BadRequest("El Registro de control de vales no existe");
            }

            controlvale.ControlValeId = ControlValeId;
            controlvale.FechaEmicion = controlDeVales.FechaEmicion;
            controlvale.FechaVigencia = controlDeVales.FechaVigencia;
            controlvale.Usuario = controlDeVales.Usuario;
            controlvale.Recibio = controlDeVales.Recibio;
            controlvale.UsuarioAutoriza = controlDeVales.UsuarioAutoriza;
            controlvale.DepartamentoId = controlDeVales.DepartamentoId;
            controlvale.ProveedorId = controlDeVales.ProveedorId;
            controlvale.EstatusValeId = controlDeVales.EstatusValeId;
            controlvale.TipoId = controlDeVales.TipoId;
            controlvale.Archivado = controlDeVales.Archivado;

            _context.ControlDeVales.Update(controlvale);
            await _context.SaveChangesAsync();
            return Ok("Control de vales actualizado correctamente");
        }

        [HttpDelete("{ControlValeId}")]
        public async Task<IActionResult> DeleteControlDeVales(int ControlValeId)
        {
            var controlvale = _context.ControlDeVales
                .FirstOrDefault(f => f.ControlValeId == ControlValeId);
            if (controlvale == null)
            {
                return NotFound();
            }

            controlvale.Archivado = true;
            _context.ControlDeVales.Update(controlvale);
            await _context.SaveChangesAsync();
            return Ok("Control de vales Archivado");
        }
    }
}
