using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Net;

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
        //Traer imagen de empleado 
        [HttpGet("FotoPerfil/{expedienteDigitalId}")]
        public async Task<IActionResult> GetImagenPerfil(int expedienteDigitalId)
        {
            var expediente = await _context.ExpedienteDigital
                .Include(i => i.Empleados)
                .FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);
            var fotoPerfil = await _context.Archivos.FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);
            if (fotoPerfil == null || expediente == null)
            {
                var perfil = System.IO.File.OpenRead($"Files/images/blank-profile.jpg");
                return File(perfil, "image/jpeg");
            }
            var serverPath = "ftp://digital.atogobmx.com/Files/RecursosHumanos/Empleados/";
            var empleado = expediente.Empleados.NombreCompleto.ToString();
            var filePath = fotoPerfil.Nombre.ToString();
            var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(serverPath + empleado + "/" + filePath);
            ftpRequest.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            var ftpStream = ftpResponse.GetResponseStream();
            return File(ftpStream, "image/jpeg");

        }
        [HttpGet("FotoPerfil/Empleado/{EmpleadoId}")]
        public async Task<IActionResult> GetImagenPerfilByEmployee(int EmpleadoId)
        {
            var expediente = await _context.ExpedienteDigital
                .Include(i => i.Empleados)
                .FirstOrDefaultAsync(f => f.EmpleadoId == EmpleadoId);
            var fotoPerfil = await _context.Archivos.FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expediente.ExpedienteDigitalId);
            if (fotoPerfil == null || expediente == null)
            {
                var perfil = System.IO.File.OpenRead($"Files/images/blank-profile.jpg");
                return File(perfil, "image/jpeg");
            }
            var serverPath = "ftp://digital.atogobmx.com/Files/RecursosHumanos/Empleados/";
            var empleado = expediente.Empleados.NombreCompleto.ToString();
            var filePath = fotoPerfil.Nombre.ToString();
            var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(serverPath + empleado + "/" + filePath);
            ftpRequest.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            var ftpStream = ftpResponse.GetResponseStream();
            return File(ftpStream, "image/jpeg");

        }
        [HttpGet("Documentos/Descargar/{ExpedienteDigitalId}/{ArchivoId}")]
        public async Task<IActionResult> DownloadFile(int ExpedienteDigitalId, int ArchivoId)
        {
            try
            {

                var expediente = await _context.ExpedienteDigital
                    .Include(i => i.Empleados)
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
                var serverPath = "ftp://digital.atogobmx.com/Files/RecursosHumanos/Empleados/";
                var empleado = expediente.Empleados.NombreCompleto.ToString();
                var filePath = documento.Nombre.ToString();
                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(serverPath + empleado + "/Documentos/" + filePath);
                var url = serverPath + empleado + "/Documentos/" + filePath;
                ftpRequest.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                var ftpStream = ftpResponse.GetResponseStream();
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(url, out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                return File(ftpStream, contentType, Path.GetFileName(url));

            }
            catch (Exception e)
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
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.ExpedienteDigitalId == ExpedienteDigitalId);
            var employeeName = expediente.Empleados.NombreCompleto.ToString();
            var host = "ftp://digital.atogobmx.com/Files/RecursosHumanos/Empleados/";

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
                    .Include(i => i.Empleados)
                    .FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);

                string uploadUrl = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "RecursosHumanos", "Empleados", expediente.Empleados.NombreCompleto, file.FileName);
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
                    var request = (FtpWebRequest)WebRequest.Create(uploadUrl);
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
                    byte[] buffer = new byte[1024];
                    var stream = file.OpenReadStream();
                    byte[] fileContents;
                    using (var ms = new MemoryStream())
                    {
                        int read;
                        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        fileContents = ms.ToArray();
                    }
                    using (Stream requestStream = await request.GetRequestStreamAsync())
                    {
                        requestStream.Write(fileContents, 0, fileContents.Length);
                    }
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
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
                    string Url = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "RecursosHumanos", "Empleados", expediente.Empleados.NombreCompleto, archivoExpediente.Nombre);
                    //string uploadUrl = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "RecursosHumanos", "Empleados", expediente.Empleados.NombreCompleto, file.FileName);
                    var delete = DeleteFile(Url);
                    if (delete)
                    {
                        var request = (FtpWebRequest)WebRequest.Create(uploadUrl);
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
                        byte[] buffer = new byte[1024];
                        var stream = file.OpenReadStream();
                        byte[] fileContents;
                        using (var ms = new MemoryStream())
                        {
                            int read;
                            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, read);
                            }
                            fileContents = ms.ToArray();
                        }
                        using (Stream requestStream = await request.GetRequestStreamAsync())
                        {
                            requestStream.Write(fileContents, 0, fileContents.Length);
                        }
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        archivoExpediente.Nombre = file.FileName;
                        _context.Archivos.Update(archivoExpediente);
                        await _context.SaveChangesAsync();
                        return Ok("Foto de perfil registrada correctamente.");
                    }
                    else
                    {
                        return BadRequest();
                    }
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
                    .Include(i => i.Empleados)
                    .FirstOrDefaultAsync(f => f.ExpedienteDigitalId == expedienteDigitalId);

                string serverPath = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "RecursosHumanos", "Empleados", expediente.Empleados.NombreCompleto, "Documentos");
                if (expediente == null)
                {
                    return NotFound("No se encuentra el expediente digital");
                }
                #endregion
                if (Files.Count > 0)
                {
                    foreach (var file in Files)
                    {
                        #region Comprobar que el archivo sea un documento
                        var fileName = file.FileName;
                        var fileExtension = Path.GetExtension(fileName);
                        if (fileExtension != ".pdf" && fileExtension != ".docx")
                        {
                            return BadRequest("El tipo de archivo no es válido");
                        }

                        #endregion

                        #region Comprobar si ya existe una imagen registrada al expediente, crearlo
                        var request = (FtpWebRequest)WebRequest.Create(serverPath + "/" + file.FileName);
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
                        byte[] buffer = new byte[1024];
                        var stream = file.OpenReadStream();
                        byte[] fileContents;
                        using (var ms = new MemoryStream())
                        {
                            int read;
                            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, read);
                            }
                            fileContents = ms.ToArray();
                        }
                        using (Stream requestStream = await request.GetRequestStreamAsync())
                        {
                            requestStream.Write(fileContents, 0, fileContents.Length);
                        }
                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
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
            var serverPath = "ftp://digital.atogobmx.com/Files/RecursosHumanos/Empleados/";
            var empleado = Expediente.Empleados.NombreCompleto.ToString();
            var filePath = Archivo.Nombre.ToString();
            var Url = $"{serverPath + empleado + "/" + filePath}";
            var result = DeleteFile(Url);
            if (result)
            {
                Archivo.Archivado = true;
                _context.Archivos.Update(Archivo);
                await _context.SaveChangesAsync();
                return Ok("Documento archivado correctamente.");
            }
            return BadRequest("Error");
            //var path = $@"Files/Documentos/{Expediente.Empleados.NombreCompleto}/{Archivo.Nombre}";
            //FileInfo file = new FileInfo(path);
            //if (file.Exists)
            //{
            //    file.Delete();
            //    Archivo.Archivado = true;
            //    _context.Archivos.Update(Archivo);
            //    await _context.SaveChangesAsync();
            //    return Ok("Documento archivado correctamente.");
            //}
           
        }
        private static bool DeleteFile(string url)
        {
            try
            {

                WebRequest request = WebRequest.Create(url);
                request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                response.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
