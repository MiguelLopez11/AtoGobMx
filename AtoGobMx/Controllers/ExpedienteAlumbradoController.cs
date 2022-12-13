using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;
using iText.Layout.Properties;

namespace AtoGobMx.Controllers
{
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
        [HttpGet("Download/{ExpedienteAlumbradoId}")]
        public void DownloadExpedienteAlumbrado(int ExpedienteAlumbradoId)
        {
            PdfWriter writer = new PdfWriter("C:/Users/Arturo/Desktop/demo.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("HEADER")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);

            document.Add(header);
            document.Close();
            //GlobalFontSettings.FontResolver = new FontResolver();
            //var document = new PdfDocument();
            //var page = document.AddPage();
            //var gfx = XGraphics.FromPdfPage(page);
            //var font = new XFont("Arial", 20, XFontStyle.Bold);
            //var textColor = XBrushes.Black;
            //var layout = new XRect(20, 20, page.Width, page.Height);
            //var format = XStringFormats.Center;
            //gfx.DrawString("Hello World!", font, textColor, layout, format);
            //document.Save("helloworld.pdf");
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
