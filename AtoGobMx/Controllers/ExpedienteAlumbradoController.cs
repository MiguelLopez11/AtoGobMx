using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteAlumbradoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public ExpedienteAlumbradoController(AtoGobMxContext context, IMapper mapper, IConverter converter)
        {
            _context = context;
            _mapper = mapper;
            _converter = converter;
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
        [HttpGet("Download")]
        public async Task<ActionResult<ExpedienteAlumbrado>> DownloadExpedienteAlumbrado()
        {
            //var expefalla = await _context.ExpedienteAlumbrado
            //    .Include(i => i.Alumbrado)
            //    .Include(i => i.Departamentos)
            //    .Include(i => i.Area)
            //    .Where(w => !w.Archivado)
            //    .FirstOrDefaultAsync(f => f.ExpedienteAlumbradoId == ExpedienteAlumbradoID);
            var html = $@"
              <!DOCTYPE html>
<html lang=""en"" dir=""ltr"">
  <head>
    <meta charset=""utf-8"" />
    <title>Reporte PDF</title>
    <link
      href=""https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css""
      rel=""stylesheet""
      integrity=""sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65""
      crossorigin=""anonymous""
    />
  </head>
  <body>
    <h1 style=""text-align: center;"">
      Reporte de fallas en alumbrado publico &#128161;
    </h1>
    <table
      class=""table table-striped table-striped table-bordered""
      style=""
        width: 30%;
        text-align: center;
        margin: 60px 0 0 702px;
        border: black 3px solid;
      ""
    >
      <tr style=""border: black 3px solid; background-color: #A5F1E9;"">
        <th scope=""col"">Fecha</th>
      </tr>
      <tbody>
        <tr>
          <td></td>
        </tr>
      </tbody>
    </table>

    <table
      class=""table table-striped table-striped table-bordered""
      style=""
        width: 97%;
        text-align: center;
        margin: 25px 0 0 10px;
        border: black 3px solid;
      ""
    >
      <tr style=""border: black 3px solid; background-color: #A5F1E9"">
        <th scope=""col"">Descripcion solucion</th>
      </tr>
      <tbody>
        <tr>
          <td style=""text-align: left;""></td>
        </tr>
      </tbody>
    </table>

    <table
      class=""table table-striped table-striped table-bordered""
      style=""
        width: 25%;
        text-align: center;
        margin: 25px 0 0 10px;
        border: black 3px solid;
      ""
    >
      <tr style=""border: black 3px solid; background-color: #A5F1E9;"">
        <th scope=""col"">Nomenclatura</th>
      </tr>
      <tbody>
        <tr>
          <td style=""text-align: left;""></td>
        </tr>
      </tbody>
    </table>

    <table
      class=""table table-striped table-striped table-bordered""
      style=""
        width: 25%;
        text-align: center;
        margin: -62px 0 0 380px;
        border: black 3px solid;
      ""
    >
      <tr style=""border: black 3px solid; background-color: #A5F1E9;"">
        <th scope=""col"">Lugar publico</th>
      </tr>
      <tbody>
        <tr>
          <td style=""text-align: left;""></td>
        </tr>
      </tbody>
    </table>

    <table
      class=""table table-striped table-striped table-bordered""
      style=""
        width: 25%;
        text-align: center;
        margin: -62px 0 0 755px;
        border: black 3px solid;
      ""
    >
      <tr style=""border: black 3px solid; background-color: #A5F1E9;"">
        <th scope=""col"">Localidad</th>
      </tr>
      <tbody>
        <tr>
          <td style=""text-align: left;""></td>
        </tr>
      </tbody>
    </table>

    <table
      class=""table table-striped table-striped table-bordered""
      style=""
        width: 25%;
        text-align: center;
        margin: 25px 0 0 10px;
        border: black 3px solid;
      ""
    >
      <tr style=""border: black 3px solid; background-color: #A5F1E9;"">
        <th scope=""col"">Nombre del departamento</th>
      </tr>
      <tbody>
        <tr>
          <td style=""text-align: left;""></td>
        </tr>
      </tbody>
    </table>

    <table
      class=""table table-striped table-striped table-bordered""
      style=""
        width: 25%;
        text-align: center;
        margin: -62px 0 0 380px;
        border: black 3px solid;
      ""
    >
      <tr style=""border: black 3px solid; background-color: #A5F1E9;"">
        <th scope=""col"">Nombre del are</th>
      </tr>
      <tbody>
        <tr>
          <td style=""text-align: left;""></td>
        </tr>
      </tbody>
    </table>

    <table
      class=""table table-striped table-striped table-bordered""
      style=""
        width: 25%;
        text-align: center;
        margin: -62px 0 0 755px;
        border: black 3px solid;
      ""
    >
      <tr style=""border: black 3px solid; background-color: #A5F1E9;"">
        <th scope=""col"">Nombre del empleado</th>
      </tr>
      <tbody>
        <tr>
          <td style=""text-align: left;""></td>
        </tr>
      </tbody>
    </table>

    <div class="" >
      < p > putos </ p >
    </ div >
  </ body >
</ html >


              ";
            //{ expefalla.FechaAlta}
            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Portrait;
            globalSettings.PaperSize = PaperKind.A4;
            globalSettings.Margins = new MarginSettings { Top = 25, Bottom = 25 };
            ObjectSettings objectSettings = new ObjectSettings();
            //objectSettings.PagesCount = true;
            objectSettings.HtmlContent = html;
            WebSettings webSettings = new WebSettings();
            webSettings.DefaultEncoding = "utf-8";
            HeaderSettings headerSettings = new HeaderSettings();
            headerSettings.FontSize = 15;
            headerSettings.FontName = "Ariel";
            //headerSettings.Right = "Page [page] of [toPage]";
            headerSettings.Line = true;
            //FooterSettings footerSettings = new FooterSettings();
            //footerSettings.FontSize = 12;
            //footerSettings.FontName = "Ariel";
            //footerSettings.Center = "This is for demonstration purposes only.";
            //footerSettings.Line = true;
            objectSettings.HeaderSettings = headerSettings;
            //objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            var pdfFile = _converter.Convert(htmlToPdfDocument); ;
            return File(pdfFile,
            "application/octet-stream", "DemoPdf.pdf");
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
            expedientalumbra.LugarPublico = expedienteAlumbrado.LugarPublico;
            expedientalumbra.Localidad = expedienteAlumbrado.Localidad;
            expedientalumbra.DepartamentoId = expedienteAlumbrado.DepartamentoId;
            expedientalumbra.AreaId = expedienteAlumbrado.AreaId;

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
