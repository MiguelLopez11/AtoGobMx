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
            if(expediente == null)
            {
                return NotFound("no se encuentra expediente");
            }
            var image = System.IO.File.OpenRead($"Files/Images/{expediente.Nombre}");
            return File(image, "image/jpeg");

        }
        [HttpPost("FotoPerfil/{expedienteDigitalId}/{nombreUsuario}")]
        public async Task<IActionResult> UploadPhotoProfile(IFormFile file, int expedienteDigitalId, string nombreUsuario)
        {
            try
            {
                #region Comprobar si el expediente existe
                var expediente = await _context.ExpedienteDigital.FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);
                if (expediente == null)
                {
                    return NotFound("No se encuentra el expediente digital");
                }
                #endregion
                #region Comprobar que el archivo sea una imagen
                var fileName = file.FileName;
                var fileExtension = Path.GetExtension(fileName);

                if (fileExtension != ".png")
                {
                    if (fileExtension != ".jpg")
                    {
                        if (fileExtension != ".jpeg")
                        {
                            return BadRequest("El tipo de archivo no es válido para foto de perfil");
                        }
                    }
                }
                #endregion
                #region Comprobar si ya existe una imagen registrada al expediente, crearlo
                var archivoExpediente = await _context.Archivos
                    .Where(w => w.ExpedienteDigitalId == expedienteDigitalId)
                    .Where(w => w.TipoArchivo.Contains(".png") || w.TipoArchivo.Contains(".jpg") || w.TipoArchivo.Contains(".jpeg"))
                    .ToArrayAsync();
                if (archivoExpediente.Length < 1)
                {
                    var pathFolder = $@"Files/Images/{nombreUsuario}";
                    if (!Directory.Exists(pathFolder))
                    {
                        Directory.CreateDirectory(pathFolder);
                    }
                    var path = Path.Combine(Directory.GetCurrentDirectory(), pathFolder, file.FileName);
                    //Directory.CreateDirectory(path);
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    var archivo = new Archivos()
                    {
                        ArchivoId = 0,
                        Nombre = $"Foto_Perfil: {file.FileName}",
                        TipoArchivo = fileExtension,
                        ExpedienteDigitalId = expedienteDigitalId
                    };
                    _context.Archivos.Add(archivo);
                    await _context.SaveChangesAsync();
                    return Ok(new { length = file.Length, name = file.FileName });
                }
                else
                {
                    return BadRequest("Ya se encuentra una foto de perfil ");
                }
                #endregion
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
