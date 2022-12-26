using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace AtoGobMx.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteAlumbradoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public ExpedienteAlumbradoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ExpedienteAlumbrado>> GetExpedienteAlumbrado()
        {
            var expefalla = await _context.ExpedienteAlumbrado
                .Include(i => i.Alumbrado)
                .Where(w => !w.Archivado)
                .OrderBy(o => o.ExpedienteAlumbradoId)
                .ToListAsync();
            return Ok(expefalla);
        }

        //"Alumbrado/AlumbradoId"
        [HttpGet("{ExpedienteAlumbradoId}")]
        public async Task<ActionResult> GetExpedienteById(int ExpedienteAlumbradoId)
        {
            var expedienteAlumbrado = await _context.ExpedienteAlumbrado
                .Include(i => i.Alumbrado)
                .FirstOrDefaultAsync(f => f.ExpedienteAlumbradoId == ExpedienteAlumbradoId);
            if (expedienteAlumbrado == null)
            {
                return NotFound();
            }
            return Ok(expedienteAlumbrado);
        }
        [HttpGet("Alumbrado/{AlumbradoId}")]
        public async Task<ActionResult> GetExpedienteAlumbradoByAlumbrado(int AlumbradoId)
        {
            var expedienteAlumbrado = await _context.ExpedienteAlumbrado
                .Include(i => i.Alumbrado)
                .FirstOrDefaultAsync(f => f.AlumbradoId == AlumbradoId);
            if (expedienteAlumbrado == null)
            {
                return NotFound();
            }
            return Ok(expedienteAlumbrado);
        }
        [HttpGet("ExpedienteAlumbrado/Download/{ExpedienteAlumbradoId}")]
        public async Task<ActionResult<ExpedienteAlumbrado>> DownloadExpedienteAlumbrado(int ExpedienteAlumbradoId)
        {
            var departamento = "";
            //var vehicles = "";
            var expefalla = await _context.ExpedienteAlumbrado
                .Include(i => i.Alumbrado)
                .Include(i => i.Departamentos)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.ExpedienteAlumbradoId == ExpedienteAlumbradoId);
            //Tomar vehiculos relacionados al expediente
            var vehiculos = await _context.VehiculosAlumbrado
                .Where(w => w.ExpedienteAlumbradoId == ExpedienteAlumbradoId)
                .ToListAsync();
            foreach (var vehicle in vehiculos)
            {
                //if (!vehiculos.Contains(vehicle[0].ToString()))
                //{
                //    vehiculos.Add(vehicle[0].ToString(9));
                //}
            }
            if (expefalla.DepartamentoId != null)
            {
                departamento = expefalla.Departamentos.Nombre;
            }
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync($"<!DOCTYPE html>\r\n<html lang=\"en\" dir=\"ltr\">\r\n\r\n<head>\r\n  <meta charset=\"utf-8\" />\r\n  <title></title>\r\n  <link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\"\r\n    integrity=\"sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65\" crossorigin=\"anonymous\" />\r\n</head>\r\n<style>\r\n  .row {{\r\n    width: 35%;\r\n    margin: 0% 0 0 63%\r\n  }}\r\n\r\n  .label {{\r\n    font-size: 15px;\r\n  }}\r\n\r\n  .label2 {{\r\n    font-size: 19px;\r\n  }}\r\n\r\n  .row2 {{\r\n    width: 96%;\r\n    margin: 0% 0 0 2%;\r\n  }}\r\n\r\n  .row3 {{\r\n    width: 96%;\r\n    margin: 0% 0 0 2%;\r\n  }}\r\n  .row4{{\r\n    width: 96%;\r\n    margin: 0% 0 0 2%;\r\n  }}\r\n</style>\r\n\r\n<body>\r\n  <br>\r\n  <h1 style=\"text-align: center;\">Reporte de fallas en alumbrado publico\r\n    &#128161;</h1>\r\n  <br>\r\n  <div class=\"row\">\r\n    <table class=\"table table-bordered border-dark\">\r\n      <thead>\r\n        <tr>\r\n          <th scope=\"col\">\r\n            <h1 class=\"label\">\r\n              <p>Fecha: <a class=\"label2\">{expefalla.FechaAlta}</a></p>\r\n            </h1>\r\n          </th>\r\n        </tr>\r\n      </thead>\r\n    </table>\r\n  </div>\r\n  <h1 style=\"text-align: center;\">Registro de empleado <img src=\"https://img.icons8.com/doodle/48/null/group.png\" />\r\n  </h1>\r\n  <div class=\"row2\">\r\n    <table class=\"table table-bordered border-dark\">\r\n      <thead>\r\n        <tr>\r\n          <th>\r\n            <h1 class=\"label\">\r\n              <p style=\"text-align: center;\">Empleado</p>\r\n            </h1>\r\n          </th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n        <tr>\r\n          <td>\r\n            <h4>\r\n              <p class=\"label2\">{expefalla.EmpleadosAlumbrado}</p>\r\n            </h4>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table>\r\n  </div>\r\n  <h1 style=\"text-align: center;\">Registro de vehiculos <img\r\n      src=\"https://img.icons8.com/ios-filled/50/null/fire-truck.png\" /></h1>\r\n  <div class=\"row3\">\r\n    <table class=\"table table-bordered border-dark\">\r\n      <thead>\r\n        <tr>\r\n          <th>\r\n            <h1 class=\"label\">\r\n              <p style=\"text-align: center;\">Vehiculo</p>\r\n            </h1>\r\n          </th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n        <tr>\r\n          <td>\r\n            <h4>\r\n              <p class=\"lalbel2\">{{expefalla.VehiculosAlumbrado}}</p>\r\n            </h4>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table>\r\n  </div>\r\n  <h1 style=\"text-align: center;\">Registro de fallas <img src=\"https://img.icons8.com/external-flaticons-flat-flat-icons/64/null/external-lighting-renewable-energy-flaticons-flat-flat-icons-2.png\"/></h1>\r\n  <div class=\"row4\">\r\n    <table class=\"table table-bordered border-dark\">\r\n      <thead>\r\n        <tr>\r\n          <th>\r\n            <h1 class=\"label\">\r\n              <p style=\"text-align: center;\">Nomre del servicio</p>\r\n            </h1>\r\n          </th>\r\n          <th>\r\n            <h1 class=\"label\">\r\n              <p style=\"text-align: center;\">Nomenclatura</p>\r\n            </h1>\r\n          </th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n        <tr>\r\n          <td>\r\n            <h4>\r\n              <p class=\"label2\">{expefalla.Alumbrado.NombreObra}</p>\r\n            </h4>\r\n          </td>\r\n          <td>\r\n            <h4>\r\n              <p class=\"label2\">{expefalla.Nomenclatura}</p>\r\n            </h4>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table>\r\n  </div>\r\n  <div class=\"row4\">\r\n    <table class=\"table table-bordered border-dark\">\r\n      <thead>\r\n        <tr>\r\n          <th>\r\n            <h1 class=\"label\">\r\n              <p style=\"text-align: center;\">Lugar publico</p>\r\n            </h1>\r\n          </th>\r\n          <th>\r\n            <h1 class=\"label\">\r\n              <p style=\"text-align: center;\">Localidad</p>\r\n            </h1>\r\n          </th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n        <tr>\r\n          <td>\r\n            <h4>\r\n              <p class=\"label2\">{expefalla.Direccion}</p>\r\n            </h4>\r\n          </td>\r\n          <td>\r\n            <h4>\r\n              <p class=\"label2\">{expefalla.Localidad}</p>\r\n            </h4>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table>\r\n  </div>\r\n  <div class=\"row4\">\r\n    <table class=\"table table-bordered border-dark\">\r\n      <thead>\r\n        <tr>\r\n          <th>\r\n            <h1 class=\"label\">\r\n              <p>Departamento</p>\r\n            </h1>\r\n          </th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n        <tr>\r\n          <td>\r\n            <h4>\r\n              <p class=\"label2\">{departamento}</p>\r\n            </h4>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table>\r\n  </div>\r\n  <div class=\"row4\">\r\n    <table class=\"table table-bordered border-dark\">\r\n      <thead>\r\n        <tr>\r\n          <th>\r\n            <h1 class=\"label\">\r\n              <p>Descripcion: </p>\r\n            </h1>\r\n          </th>\r\n        </tr>\r\n      </thead>\r\n      <tbody>\r\n        <tr>\r\n          <td>\r\n            <h4>\r\n              <p class=\"label2\">{expefalla.DescripcionSolucion}</p>\r\n            </h4>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table>\r\n  </div>\r\n\r\n</body>\r\n\r\n</html>");
            var pdfContent = await page.PdfStreamAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true
            });
            return File(pdfContent, "application/pdf", "converted.pdf");
        }
        [HttpPost()]
        public async Task<ActionResult<ExpedienteAlumbrado>> PostExpedienteAlumbrado(ExpedienteAlumbrado expedienteAlumbrado)
        {
            _context.ExpedienteAlumbrado.Add(expedienteAlumbrado);
            expedienteAlumbrado.FechaAlta = DateTime.Now;
            var TareaAlumbrado = await _context.Alumbrado
                .FirstOrDefaultAsync(f => f.AlumbradoId == expedienteAlumbrado.AlumbradoId);
            TareaAlumbrado.TieneExpediente = true;
            await _context.SaveChangesAsync();
            return Ok("Expediente creado correctamente");
        }

        [HttpPut("{ExpedienteAlumbradoId}")]
        public async Task<ActionResult> PutExpedientAlumbrado(int ExpedienteAlumbradoId, ExpedienteAlumbrado expedienteAlumbrado)
        {
            if (expedienteAlumbrado.ExpedienteAlumbradoId != ExpedienteAlumbradoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var expedientalumbra = _context.ExpedienteAlumbrado.Find(ExpedienteAlumbradoId);
            if (expedientalumbra == null)
            {
                return BadRequest("El Registro del expediente alumbrado no existe");
            }

            expedientalumbra.ExpedienteAlumbradoId = ExpedienteAlumbradoId;
            expedientalumbra.DescripcionSolucion = expedienteAlumbrado.DescripcionSolucion;
            expedientalumbra.AlumbradoId = expedienteAlumbrado.AlumbradoId;
            expedientalumbra.FechaAlta = expedienteAlumbrado.FechaAlta;
            expedientalumbra.Nomenclatura = expedienteAlumbrado.Nomenclatura;
            expedientalumbra.Direccion = expedienteAlumbrado.Direccion;
            expedientalumbra.Localidad = expedienteAlumbrado.Localidad;
            expedientalumbra.DepartamentoId = expedienteAlumbrado.DepartamentoId;
            //expedientalumbra.AreaId = expedienteAlumbrado.AreaId;

            _context.ExpedienteAlumbrado.Update(expedientalumbra);
            await _context.SaveChangesAsync();
            return Ok("Expediente alumbrado actualizado correctamente");
        }

        [HttpDelete("{ExpedienteAlumbradoId}")]
        public async Task<IActionResult> DeleteExpedienteAlumbrado(int ExpedienteAlumbradoId)
        {
            var expedientalumbrado = _context.ExpedienteAlumbrado
                .FirstOrDefault(f => f.ExpedienteAlumbradoId == ExpedienteAlumbradoId);
            if (expedientalumbrado == null)
            {
                return NotFound();
            }
            var servicioAlumbrado = await _context.Alumbrado
                .FirstOrDefaultAsync(f => f.AlumbradoId == expedientalumbrado.AlumbradoId);
            if (servicioAlumbrado == null)
            {
                expedientalumbrado.Archivado = true;
                _context.ExpedienteAlumbrado.Update(expedientalumbrado);
                await _context.SaveChangesAsync();
                return Ok("Expediente alumbrado Archivado");
            }
            expedientalumbrado.Archivado = true;
            servicioAlumbrado.TieneExpediente = false;
            _context.ExpedienteAlumbrado.Update(expedientalumbrado);
            await _context.SaveChangesAsync();
            return Ok("Expediente alumbrado Archivado");
        }

    }
}
