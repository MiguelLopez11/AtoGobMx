using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public ArchivosController(AtoGobMxContext Context, IMapper mapper)
        {
            _context = Context;
            _mapper = mapper;
        }
        [HttpGet("{expedienteDigitalId}")]
        public async Task<IActionResult> GetImagenPerfil(int expedienteDigitalId)
        {
            var expediente = await _context.Archivos.FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);
            var image = System.IO.File.OpenRead($"Files/Images/{expediente.Nombre}");
            return File(image, "image/jpeg");

        }
        [HttpPost("FotoPerfil/{expedienteDigitalId}")]
        public async Task<IActionResult> UploadPhotoProfile(IFormFile file, int expedienteDigitalId)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Files/Images", file.FileName);
                var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
                var fileName = file.FileName;
                var fileExtension = Path.GetExtension(fileName);

                if(fileExtension != ".png")
                {
                    if(fileExtension != ".jpg")
                    {
                        return BadRequest("El tipo de archivo no es válido para foto de perfil");
                    }
                }
                var archivoExpediente = await _context.Archivos
                    .Where(w => w.ExpedienteDigitalId == expedienteDigitalId)
                    .Where(w => w.TipoArchivo == ".png")
                    .ToListAsync();
                if (archivoExpediente != null)
                {
                    return Ok(archivoExpediente);
                }
                var archivo = new Archivos()
                {
                    ArchivoId = 0,
                    Nombre = file.FileName,
                    TipoArchivo = fileExtension,
                    ExpedienteDigitalId = expedienteDigitalId
                };
                _context.Archivos.Add(archivo);
                await _context.SaveChangesAsync();
                return Ok(new { length = file.Length, name = file.FileName });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
