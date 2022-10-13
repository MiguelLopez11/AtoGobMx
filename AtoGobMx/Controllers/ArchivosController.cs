using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

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
            var expediente = await _context.ExpedienteDigital
                .Include(i => i.Empleados)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);
            if (expediente == null)
            {
                return NotFound("El ID del expediente no existe.");
            }
            var fotoPerfil = await _context.Archivos.FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);
            if (fotoPerfil == null)
            {
                return NotFound("No se encuentra foto de perfil registrado a ese expediente.");
            }
            var image = System.IO.File.OpenRead($"Files/images/{expediente.Empleados.NombreCompleto}/{fotoPerfil.Nombre}");
            return File(image, "image/jpeg");

        }
        [HttpGet("Documentos/Descargar/{ExpedienteDigitalId}/{ArchivoId}")]
        public async Task<IActionResult> DownloadFile(int ExpedienteDigitalId, int ArchivoId)
        {
            try
            {

            var expediente = await _context.ExpedienteDigital
                .Include(i => i.Empleados)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.ExpedienteDigitalId == ExpedienteDigitalId);
            if (expediente == null)
            {
                return NotFound("El ID del expediente no existe.");
            }
            var documento = await _context.Archivos
                .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                .FirstOrDefaultAsync(f => f.ArchivoId == ArchivoId);
            if (documento == null)
            {
                return NotFound("No se encuentra Archivo");
            }
            var filePath = $"Files/Documentos/{expediente.Empleados.NombreCompleto}/{documento.Nombre}";
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
            }catch(Exception e)
            {
                return NoContent();
            }

        }
        [HttpGet("Documentos/{ExpedienteDigitalId}")]
        public async Task<IActionResult> GetDocumentos(int ExpedienteDigitalId)
        {
            var Documentos = await _context.Archivos
                .Include(i => i.expedienteDigital)
                .Where(w => w.ExpedienteDigitalId == ExpedienteDigitalId)
                .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Documentos == null)
            {
                return BadRequest("No se encuentran documentos registrados, ");
            }
            return Ok(Documentos);
        }
        [HttpGet("Documentos/{ExpedienteDigitalId}/Zip")]
        public async Task<IActionResult> DownloadFilesZip(int ExpedienteDigitalId)
        {
            var expediente = await _context.ExpedienteDigital
                .Include(i => i.Empleados)
                .FirstOrDefaultAsync(f => f.ExpedienteDigitalId == ExpedienteDigitalId);

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Files/Documentos/{expediente.Empleados.NombreCompleto}");
            var FilePaths = Directory.GetFiles(FolderPath);
            var zipFileMemoryStream = new MemoryStream();
            using (ZipArchive archive = new ZipArchive(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
            {
                foreach (var FilePath in FilePaths)
                {
                    var FileName = Path.GetFileName(FilePath);
                    var entry = archive.CreateEntry(FileName);
                    using (var entryStream = entry.Open())
                    using (var fileStream = System.IO.File.OpenRead(FilePath))
                    {
                        await fileStream.CopyToAsync(entryStream);
                    }
                }
            }

            zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
            return File(zipFileMemoryStream, "application/octet-stream", $"Documentos_{DateOnly.FromDateTime(DateTime.Now)}_{expediente.Empleados.NombreCompleto}.zip");
        
    }
        [HttpPost("Imagen/{expedienteDigitalId}/")]
        public async Task<IActionResult> UploadPhotoProfile(IFormFile file, int expedienteDigitalId)
        {
                #region Comprobar si el expediente existe
            try
            {
                var expediente = await _context.ExpedienteDigital
                    .Where(w => !w.Archivado)
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

                if (fileExtension != ".png" && fileExtension != ".jpg" && fileExtension != ".jpeg")
                {
                    return BadRequest("El tipo de archivo no es válido para foto de perfil");
                }
                #endregion
                #region Comprobar si ya existe una imagen registrada al expediente, crearlo
                var archivoExpediente = await _context.Archivos
                    .Where(w => w.TipoArchivo.Contains(".png") || w.TipoArchivo.Contains(".jpg") || w.TipoArchivo.Contains(".jpeg"))
                    .FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);
                if (archivoExpediente == null)
                {
                    var pathFolder = $@"Files/images/{expediente.Empleados.NombreCompleto}";
                    if (!Directory.Exists(pathFolder))
                    {
                        Directory.CreateDirectory(pathFolder);
                    }
                    var path = Path.Combine(Directory.GetCurrentDirectory(), pathFolder, file.FileName);
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
                    return Ok("Foto de perfil registrada correctamente.");
                }
                else
                {
                    var path = $@"Files/images/{expediente.Empleados.NombreCompleto}/{archivoExpediente.Nombre}";

                    System.IO.File.Delete(path);
                    var pathFolder = $@"Files/images/{expediente.Empleados.NombreCompleto}";
                    var newPath = Path.Combine(Directory.GetCurrentDirectory(), pathFolder, file.FileName);
                    var stream = new FileStream(newPath, FileMode.Create);
                    await file.CopyToAsync(stream);
                    stream.Close();
                    archivoExpediente.Nombre = file.FileName;
                    _context.Archivos.Update(archivoExpediente);
                    await _context.SaveChangesAsync();


                    return Ok("Foto de perfil registrada correctamente.");
                }
            }
            catch
            {
                return BadRequest("Ah ocurrido un error inesperado");
            }
        #endregion
        }
        [HttpPost("Documentos/{expedienteDigitalId}/")]
        public async Task<IActionResult> UploadDocuments(List<IFormFile> Files, int expedienteDigitalId)
        {
            #region Cargar Archivos
            try
            {
                #region Comprobar si el expediente existe
                var expediente = await _context.ExpedienteDigital
                    .Where(w => !w.Archivado)
                    .Include(i => i.Empleados)
                    .FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);

                if (expediente == null)
                {
                    return NotFound("No se encuentra el expediente digital");
                }
                #endregion
                if (Files.Count > 0)
                {
                    foreach (var file in Files)
                    {
                        #region Comprobar que el archivo sea una imagen
                        var fileName = file.FileName;
                        var fileExtension = Path.GetExtension(fileName);
                        if (fileExtension != ".pdf" && fileExtension != ".docx")
                        {
                            return BadRequest("El tipo de archivo no es válido para foto de perfil");
                        }

                        #endregion

                        #region Comprobar si ya existe una imagen registrada al expediente, crearlo
                        var pathFolder = $@"Files/Documentos/{expediente.Empleados.NombreCompleto}";
                        if (!Directory.Exists(pathFolder))
                        {
                            Directory.CreateDirectory(pathFolder);
                        }
                        var path = Path.Combine(Directory.GetCurrentDirectory(), pathFolder, file.FileName);
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
                    }
                    return Ok("Documento registrado correctamente.");
                    #endregion
                }
                else
                {
                    return BadRequest("No se ingresó ningun documento");
                }
            }
            catch
            {
                return BadRequest("Ah ocurrido un error inesperado");
            }
        #endregion
        }
        [HttpDelete("Documentos/Eliminar/{ExpedienteDigitalId}/{ArchivoId}")]
        public async Task<IActionResult> DeleteDocuments(int ExpedienteDigitalId, int ArchivoId)
        {
            var Expediente = await _context.ExpedienteDigital
                .Include(i => i.Empleados)
                .FirstOrDefaultAsync(f => f.ExpedienteDigitalId == ExpedienteDigitalId);
            
            if (Expediente == null)
            {
                return NotFound($"No se encuentra expediente con el ID {ExpedienteDigitalId}");
            }
            var Archivo = await _context.Archivos
                .FirstOrDefaultAsync(f => f.ArchivoId == ArchivoId);

            if (Archivo == null)
            {
                return NotFound($"No se encuentra el documento con el ID {ArchivoId}");
            }
            var path = $@"Files/Documentos/{Expediente.Empleados.NombreCompleto}/{Archivo.Nombre}";
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                //System.IO.File.Delete(path);
            Archivo.Archivado = true;
            _context.Archivos.Update(Archivo);
            await _context.SaveChangesAsync();
            return Ok("Documento archivado correctamente.");
            }
            return BadRequest("Error inesperado, no se encuentra el documento");
        }
    }
}
