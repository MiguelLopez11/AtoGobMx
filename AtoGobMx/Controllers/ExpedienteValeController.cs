using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp.Media;
using PuppeteerSharp;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteValeController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public ExpedienteValeController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ExpedienteVale>> GetExpedienteVale()
        {
            var expedientevale = await _context.ExpedienteVale
                .Include(i => i.ControlDeVales)
                .Where(w => !w.Archivado)
                .OrderBy(o => o.ExpedienteValeId)
                .ToListAsync();
            return Ok(expedientevale);
        }
        
        [HttpGet("{ExpedienteValeId}")]
        public async Task<ActionResult> GetExpedienteValeById(int ExpedienteValeId)
        {
            var expedientevale = await _context.ExpedienteVale
                .Include(i => i.ControlDeVales)
                .FirstOrDefaultAsync(f => f.ExpedienteValeId == ExpedienteValeId);
            if (expedientevale == null)
            {
                return NotFound();
            }
            return Ok(expedientevale);
        }
        //"ControlDeVales/ControlValeId"
        [HttpGet("ControlDeVales/{ControlValeId}")]
        public async Task<ActionResult> GetExpedienteValeByControlDeVales(int ControlValeId)
        {
            var expedienteControdevale = await _context.ExpedienteVale
                .Include(i => i.ControlDeVales)
                .FirstOrDefaultAsync(f => f.ControlValeId == ControlValeId);
            if (expedienteControdevale == null)
            {
                return NotFound();
            }
            return Ok(expedienteControdevale);
        }
        //-----------creacion del expediente para la descarga del mismo----------------------//
        [HttpGet("ExpedienteVale/Download/{ExpedienteValeId}")]
        public async Task<ActionResult<ExpedienteVale>> DownloadExpedienteVale(int ExpedienteValeId)
        {
            //var departamento = "";
            //var vehicles = "";
            //var expefalla = await _context.ExpedienteAlumbrado
            //    .Include(i => i.Alumbrado)
            //    .Include(i => i.Departamentos)
            //    .Where(w => !w.Archivado)
            //    .FirstOrDefaultAsync(f => f.ExpedienteAlumbradoId == ExpedienteAlumbradoId);
            //Tomar vehiculos relacionados al expediente
            //var vehiculos = await _context.VehiculosAlumbrado
            //    .Include(i => i.Vehiculo)
            //    .Where(w => w.ExpedienteAlumbradoId == ExpedienteAlumbradoId)
            //    .ToListAsync();
            //if (expefalla.DepartamentoId != null)
            //{
            //    departamento = expefalla.Departamentos.Nombre;
            //}
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            var htmlContent = "<!DOCTYPE html>\r\n<html lang=\"en\" dir=\"ltr\">\r\n\r\n<head>\r\n  <meta charset=\"utf-8\" />\r\n  <title></title>\r\n  <link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\"\r\n    integrity=\"sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65\" crossorigin=\"anonymous\" />\r\n</head>\r\n<style>\r\n  .row {\r\n    width: 35%;\r\n    margin: 0% 0 0 63%\r\n  }\r\n\r\n  .label {\r\n    font-size: 15px;\r\n  }\r\n\r\n  .label2 {\r\n    font-size: 19px;\r\n  }\r\n\r\n  .row2 {\r\n    width: 96%;\r\n    margin: 0% 0 0 2%;\r\n  }\r\n\r\n  .row3 {\r\n    width: 96%;\r\n    margin: 0% 0 0 2%;\r\n  }\r\n  .row4{\r\n    width: 96%;\r\n    margin: 0% 0 0 2%;\r\n  }\r\n</style>\r\n\r\n<body>\r\n  <br>\r\n  <h1 style=\"text-align: center;\">Reporte de fallas en alumbrado publico\r\n    &#128161;</h1>\r\n  <br>\r\n  <div class=\"row\">\r\n    <table class=\"table table-bordered border-dark\">\r\n      <thead>\r\n        <tr>\r\n          <th scope=\"col\">\r\n            <h1 class=\"label\">\r\n              <p>Fecha: <a class=\"label2\">{expefalla.FechaAlta}</a></p>\r\n            </h1>\r\n          </th>\r\n        </tr>\r\n      </thead>\r\n    </table>\r\n  </div>\r\n  <h1 style=\"text-align: center;\">Registro de empleado <img src=\"https://img.icons8.com/doodle/48/null/group.png\" />\r\n  </h1>\r\n  <div class=\"row2\">\r\n    <table class=\"table table-bordered border-dark\">\r\n      <thead>\r\n        <tr>\r\n          <th>\r\n            <h1 class=\"label\">\r\n              <p style=\"text-align: center;\">Empleado</p>\r\n            </h1>\r\n          </th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n        <tr>\r\n          <td>\r\n            <h4>\r\n              <p class=\"label2\">{expefalla.EmpleadosAlumbrado}</p>\r\n            </h4>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table>\r\n  </div>\r\n  <h1 style=\"text-align: center;\">Registro de vehiculos <img\r\n      src=\"https://img.icons8.com/ios-filled/50/null/fire-truck.png\" /></h1>\r\n  <div class=\"row3\">\r\n    <table class=\"table table-bordered border-dark\">\r\n      <thead>\r\n        <tr>\r\n          <th>\r\n            Marca\r\n          </th>\r\n          <th>\r\n            Submarca\r\n          </th>\r\n          <th>\r\n            Placas\r\n          </th>\r\n          <th>\r\n            No. Serie\r\n          </th>\r\n          <th>\r\n            Modelo\r\n          </th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n";
            var vehiculosHTML = "";
            //foreach (var vehiculo in vehiculos)
            //{
            //    vehiculosHTML += $"<tr> \r\n <td>\r\n            {vehiculo.Vehiculo.Marca}\r\n          </td>\r\n          <td>\r\n            {vehiculo.Vehiculo.Modelo}\r\n          </td>\r\n          <td>\r\n            {vehiculo.Vehiculo.Placa}\r\n          </td>\r\n          <td>\r\n            {vehiculo.Vehiculo.NumeroSerie}\r\n          </td>\r\n          <td>\r\n            {vehiculo.Vehiculo.AñoLanzamiento}\r\n          </td> \r\n </tr> \r\n";
            //}
            htmlContent += vehiculosHTML;
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync(htmlContent);
            var pdfContent = await page.PdfStreamAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true
            });
            return File(pdfContent, "application/pdf", "converted.pdf");
        }

        [HttpPost()]
        public async Task<ActionResult<ExpedienteVale>> PostExpedienteVa(ExpedienteVale expedienteVale)
        {
            _context.ExpedienteVale.Add(expedienteVale);
            //expedienteVale.FechaAlta = DateTime.Now;
            var TareaVale = await _context.ControlDeVales
                .FirstOrDefaultAsync(f => f.ControlValeId == expedienteVale.ControlValeId);
            TareaVale.TieneExpediente = true;
            await _context.SaveChangesAsync();
            return Ok("Expediente creado correctamente");
        }

        [HttpPut("{ExpedienteValeId}")]
        public async Task<ActionResult> PutExpedientVale(int ExpedienteValeId, ExpedienteVale expedienteVale)
        {
            if (expedienteVale.ExpedienteValeId != ExpedienteValeId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var expedientvale = _context.ExpedienteVale.Find(ExpedienteValeId);
            if (expedientvale == null)
            {
                return BadRequest("El Registro del expediente control de vale no existe");
            }

            expedientvale.ExpedienteValeId = ExpedienteValeId;
            expedientvale.ControlValeId = expedienteVale.ControlValeId;
            expedientvale.Archivado = expedienteVale.Archivado;
            //expedientalumbra.AreaId = expedienteAlumbrado.AreaId;

            _context.ExpedienteVale.Update(expedientvale);
            await _context.SaveChangesAsync();
            return Ok("Expediente control de vales actualizado correctamente");
        }

        [HttpDelete("{ExpedienteValeId}")]
        public async Task<IActionResult> DeleteExpedienteVale(int ExpedienteValeId)
        {
            var expedientevale = _context.ExpedienteVale
                .FirstOrDefault(f => f.ExpedienteValeId == ExpedienteValeId);
            if (expedientevale == null)
            {
                return NotFound();
            }
            var servicioControldevale = await _context.ControlDeVales
                .FirstOrDefaultAsync(f => f.ControlValeId == expedientevale.ControlValeId);
            if (servicioControldevale == null)
            {
                expedientevale.Archivado = true;
                _context.ExpedienteVale.Update(expedientevale);
                await _context.SaveChangesAsync();
                return Ok("Expediente vale Archivado");
            }
            expedientevale.Archivado = true;
            servicioControldevale.TieneExpediente = false;
            _context.ExpedienteVale.Update(expedientevale);
            await _context.SaveChangesAsync();
            return Ok("Expediente vale Archivado");
        }

    }
}
