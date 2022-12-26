using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using FluentFTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
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

                FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(UrlHost + line);
                downloadRequest.UsePassive = true;
                downloadRequest.UseBinary = true;
                downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                downloadRequest.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
                downloadRequest.UseBinary = true;
                downloadRequest.UsePassive = true;
                downloadRequest.KeepAlive = true;
                var ftpResponse = (FtpWebResponse)downloadRequest.GetResponse();
                var ftpStream = ftpResponse.GetResponseStream();
                var fileName = @"Files/" + line;
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                FileStream file = System.IO.File.Create(fileName);
                byte[] buffer = new byte[1024];
                int read;
                while ((read = ftpStream.Read(buffer, 0, buffer.Length)) > 0) { file.Write(buffer, 0, read); }
                file.Close();
                ftpStream.Close();
                ftpResponse.Close();
            }
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Files/Documentos");
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
            //Directory.Delete("Files/Documentos");
            return File(zipFileMemoryStream, "application/octet-stream", $"Documentos_{DateOnly.FromDateTime(DateTime.Now)}_{expediente.Empleados.NombreCompleto}.zip");

            //    using (ZipArchive archive = new ZipArchive(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
            //    {
            //        ftpResponse.op
            //        await ftpStream.CopyToAsync(zipFileMemoryStream);
            //    }
            //    zipFileMemoryStream.Seek(0, SeekOrigin.Begin);
            //return File(zipFileMemoryStream, "application/octet-stream", $"Documentos_{DateOnly.FromDateTime(DateTime.Now)}_{expediente.Empleados.NombreCompleto}.zip");

            //}
            //return Ok("Directorio copiado");
            //var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Files/Documentos/{expediente.Empleados.NombreCompleto}");
            //var FilePaths = Directory.GetFiles(FolderPath);
            //var zipFileMemoryStream = new MemoryStream();
            //using (ZipArchive archive = new ZipArchive(zipFileMemoryStream, ZipArchiveMode.Update, leaveOpen: true))
            //{
            //    foreach (var FilePath in reader.ReadToEnd())
            //    {
            //        var FileName = Path.GetFileName(FilePath.ToString());
            //        var entry = archive.CreateEntry(FileName);
            //        using (var entryStream = entry.Open())
            //        using (var fileStream = System.IO.File.OpenRead(FilePath.ToString()))
            //        {
            //            await fileStream.CopyToAsync(entryStream);
            //        }
            //    }
            //}



        }

        //Metodos creados en mis espacios
        [HttpGet("Documents/AlumbradoPublico/{AlumbradoId}")]
        public async Task<IActionResult> GetDocumentosAlumbrado(int AlumbradoId)
        {
            var Documentos = await _context.ArchivosAlumbrado
                .Where(w => w.AlumbradoId == AlumbradoId)
                .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Documentos == null)
            {
                return BadRequest("No se encuentran documentos registrados, ");
            }
            return Ok(Documentos);
        }

        [HttpGet("Documents/DireccionCementerio/{DireccionCementerioId}")]
        public async Task<IActionResult> GetDocumentosCementerios(int DireccionCementerioId)
        {
            var Documentos = await _context.ArchivosCementerios
                .Where(w => w.DireccionId == DireccionCementerioId)
                .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Documentos == null)
            {
                return BadRequest("No se encuentran documentos registrados, ");
            }
            return Ok(Documentos);
        }

        [HttpGet("Documents/Aseo/{AseoId}")]
        public async Task<IActionResult> GetDocumentosAseo(int AseoId)
        {
            var Documentos = await _context.ArchivosAseo
                .Where(w => w.AseoId == AseoId)
                .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Documentos == null)
            {
                return BadRequest("No se encuentran documentos registrados, ");
            }
            return Ok(Documentos);
        }

        [HttpGet("Documents/ObrasPublicas/{ObraId}")]
        public async Task<IActionResult> GetDocumentosObras(int ObraId)
        {
            var Documentos = await _context.ArchivosObras
                .Where(w => w.ObraId == ObraId)
                .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Documentos == null)
            {
                return BadRequest("No se encuentran documentos registrados, ");
            }
            return Ok(Documentos);
        }
        //-------Descarga de uno en uno decada documento cargado al sistema---------//
        [HttpGet("Documents/Dowload/{AlumbradoId}/{ArchivoAlumbradoId}")]
        public async Task<IActionResult> DownloadFileAlumbrado(int AlumbradoId, int ArchivoAlumbradoId)
        {
            try
            {
                var alumbrado = await _context.Alumbrado
                    .Include(i => i.ArchivosAlumbrado)
                    .FirstOrDefaultAsync(f => f.AlumbradoId == AlumbradoId);
                if (alumbrado == null)
                {
                    return NotFound();
                }
                var documento = await _context.ArchivosAlumbrado
                    .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                    .FirstOrDefaultAsync(f => f.ArchivoAlumbradoId == ArchivoAlumbradoId);
                if (documento == null)
                {
                    return NotFound("No se encuentra Archivo");
                }
                var serverPath = "ftp://digital.atogobmx.com/Files/ServiciosPublicos/AlumbradoPublico/";
                var NombreObra = alumbrado.NombreObra.ToString();
                var filePath = documento.Nombre.ToString();
                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(serverPath + NombreObra + "/" + filePath);
                var url = serverPath + NombreObra + "/" + filePath;
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
        [HttpPost("Documents/AlumbradoPublico/{AlumbradoId}/")]
        public async Task<IActionResult> UploadDocumentsAlumbrado(List<IFormFile> Files, int AlumbradoId)
        {
            #region Cargar Archivos
            try
            {

                #region Comprobar si el expediente existe
                var alumbrado = await _context.Alumbrado
                    .Include(i => i.TareaTipoAlumbrado)
                    .FirstOrDefaultAsync(f => f.AlumbradoId == AlumbradoId);

                if (alumbrado == null)
                {
                    return NotFound();
                }
                string serverPath = String.Format("ftp://{0}/{1}/{2}/{3}/{4}", "digital.atogobmx.com", "Files", "ServiciosPublicos", "AlumbradoPublico", alumbrado.NombreObra);
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
                        var archivo = new ArchivosAlumbrado()
                        {
                            ArchivoAlumbradoId = 0,
                            Nombre = file.FileName,
                            TipoArchivo = fileExtension,
                            AlumbradoId = AlumbradoId
                        };
                        _context.ArchivosAlumbrado.Add(archivo);
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
        [HttpDelete("Documents/AlumbradoPublico/Eliminar/{AlumbradoId}/{ArchivoId}")]
        public async Task<IActionResult> DeleteDocumentsAlumbrado(int AlumbradoId, int ArchivoId)
        {
            var alumbrado = await _context.Alumbrado
                .FirstOrDefaultAsync(f => f.AlumbradoId == AlumbradoId);

            if (alumbrado == null)
            {
                return NotFound($"No se encuentra expediente con el ID {AlumbradoId}");
            }
            var Archivo = await _context.ArchivosAlumbrado
                .FirstOrDefaultAsync(f => f.ArchivoAlumbradoId == ArchivoId);

            if (Archivo == null)
            {
                return NotFound($"No se encuentra el documento con el ID {ArchivoId}");
            }
            var serverPath = "ftp://digital.atogobmx.com/Files/ServiciosPublicos/AlumbradoPublico/";
            var NombreObra = alumbrado.NombreObra.ToString();
            var filePath = Archivo.Nombre.ToString();
            var Url = $"{serverPath + NombreObra + "/" + filePath}";
            var result = DeleteFile(Url);
            if (result)
            {
                Archivo.Archivado = true;
                _context.ArchivosAlumbrado.Update(Archivo);
                await _context.SaveChangesAsync();
                return Ok("Documento archivado correctamente.");
            }
            return BadRequest("Error");

        }
        //-------------------------------------------------------------------------//

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
            Archivo.Archivado = true;
            _context.Archivos.Update(Archivo);
            await _context.SaveChangesAsync();
            return Ok("Documento archivado correctamente.");
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
        private static List<String> GetListFiles(string name)
        {
            string Url = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "RecursosHumanos", "Empleados", name, "Documentos");

            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://www.contoso.com/");
            //request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            //// This example assumes the FTP site uses anonymous logon.
            //request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");

            //FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            //Stream responseStream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(responseStream);
            //Console.WriteLine(reader.ReadToEnd());

            //Console.WriteLine($"Directory List Complete, status {response.StatusDescription}");


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Url);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            List<string> result = new List<string>();
            using (StreamReader streamReader = new StreamReader(responseStream))
            {
                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    result.Add(line);
                    line = streamReader.ReadLine();
                }
                result.Remove("Documentos/.");
                result.Remove("Documentos/..");
                streamReader.Close();
                response.Close();
            }

            return result;
        }

    }
}
