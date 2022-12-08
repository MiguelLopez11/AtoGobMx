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
using Aspose.Pdf.Text;
using Aspose.Pdf;
using Table = Aspose.Pdf.Table;

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
        [HttpGet("Download/{ExpedienteAlumbradoId}")]
        public async Task<ActionResult<ExpedienteAlumbrado>> DownloadExpedienteAlumbrado(int ExpedienteAlumbradoId)
        {
            var document = new Document
            {
                PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 42) }
            };

            // Step 2
            var pdfPage = document.Pages.Add();

            // Step 3
            var textFragment = new TextFragment($"Reporte de alumbrado");
            // ..........................................

            var table = new Table
            {
                // Set column auto widths of the table
                ColumnWidths = "25% 25% 25% 25%",
                // Set cell padding
                DefaultCellPadding = new MarginInfo(10, 5, 10, 5), // Left Bottom Right Top
                                                                   // Set the table border color as Green
                Border = new BorderInfo(BorderSide.All, .5f, Color.Green),
                // Set the border for table cells as Black
                DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Color.Green),
            };
            for (var rowCount = 0; rowCount < 10; rowCount++)
            {
                // Add row to table
                var row = table.Rows.Add();
                // Add table cells
                for (int i = 0; i < 4; i++)
                {
                    row.Cells.Add($"Cell ({i + 1}, {rowCount + 1})");
                }
            }
            // Add table object to first page of input document
            document.Pages[1].Paragraphs.Add(table);

            // Step 4
            pdfPage.Paragraphs.Add(textFragment);
            pdfPage.Paragraphs.Add(table);


            // Step 5
            using (var streamOut = new MemoryStream())
            {
                document.Save(streamOut);
                return new FileContentResult(streamOut.ToArray(), "application/pdf")
                {
                    FileDownloadName = "tenants.pdf"
                };
            }
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
