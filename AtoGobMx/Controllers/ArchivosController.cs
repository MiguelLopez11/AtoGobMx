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
        [HttpGet("FotoPerfil/{expedienteDigitalId}/")]
        public async Task<IActionResult> GetImagenPerfil(int expedienteDigitalId)
        {
            var expediente = await _context.ExpedienteDigital.FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);
            if (expediente == null)
            {
                return NotFound("El ID del expediente no existe.");
            }
            var fotoPerfil = await _context.Archivos.FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);
            if (fotoPerfil == null)
            {
                return NotFound("No se encuentra foto de perfil registrado a ese expediente.");
            }
            var empleado = await _context.Empleados
                //.Include(i => i.usuario)
                .Include(i => i.ExpedienteDigital)
                .FirstOrDefaultAsync(f => f.expedienteDigitalId == expedienteDigitalId);
            if (empleado == null)
            {
                return BadRequest("El empleado no tiene registrado el expediente");
            }
            //{ empleado.usuario.NombreUsuario}
            var image = System.IO.File.OpenRead($"Files/Images/{fotoPerfil.Nombre}");
            return File(image, "image/jpeg");

        }
        [HttpPost("FotoPerfil/{expedienteDigitalId}/")]
        public async Task<IActionResult> UploadPhotoProfile(IFormFile file, int expedienteDigitalId)
        {
            try
            {
                #region Comprobar si el expediente existe
                var expediente = await _context.ExpedienteDigital
                    .Include(i => i.Empleados)
                    .FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);

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
                if (archivoExpediente.Length == 0)
                {
                    var empleado = await _context.Empleados
                        //.Include(i => i.usuario)
                        .FirstOrDefaultAsync(f => f.expedienteDigitalId == expedienteDigitalId);
                    //if (empleado.usuario == null)
                    //{
                    //    return BadRequest("empleado no contiene un usuario asignado");
                    //}
                    var pathFolder = $@"Files/Images/";
                    //{ empleado.usuario.NombreUsuario}
                    if (!Directory.Exists(pathFolder))
                    {
                        Directory.CreateDirectory(pathFolder);
                    }
                    var path = Path.Combine(Directory.GetCurrentDirectory(), pathFolder, file.FileName);
                    //Directory.CreateDirectory(path);
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    stream.Close();
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
