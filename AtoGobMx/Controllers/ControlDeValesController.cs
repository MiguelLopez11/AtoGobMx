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
    [Authorize]
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
                .Include(i => i.PROV_DetalleVales)
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

            var htmlContent = $"";

            var detallevaleHTML = "";
            foreach (var detalle in detallevale)
            {
                detallevaleHTML += $"";
            }

            htmlContent += detallevaleHTML;
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync(htmlContent);

            var pdfContent = await page.PdfStreamAsync(new PdfOptions
            {
                Format = PaperFormat.A0,
                PrintBackground = true
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
