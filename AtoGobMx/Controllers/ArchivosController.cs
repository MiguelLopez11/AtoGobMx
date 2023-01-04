using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
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
        #region Expedientes Digitales
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
                var perfil = System.IO.File.OpenRead($"Files/user.png");
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
                var perfil = System.IO.File.OpenRead($"Files/user.png");
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
                Console.WriteLine(e.Message);
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
            if (expediente == null)
            {
                return NotFound();
            }
            var UrlHost = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/", "digital.atogobmx.com", "Files", "RecursosHumanos", "Empleados", expediente.Empleados.NombreCompleto);
            var result = GetListFiles(UrlHost + "Documentos");
            foreach (string line in result)
            {
                copyFile(UrlHost, line);
            }
            if (!Directory.Exists("Files/Documentos"))
            {
                Directory.CreateDirectory("Files/Documentos");
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
            var dir = new DirectoryInfo("Files/Documentos");
            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
            dir.Delete(true);
            //Directory.Delete("Files/Documentos");
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
            var Url = $"{serverPath + empleado + "/" + "Documentos/" + filePath}";
            var result = DeleteFile(Url);
            if (result)
            {
                Archivo.Archivado = true;
                _context.Archivos.Update(Archivo);
                await _context.SaveChangesAsync();
                return Ok("Documento archivado correctamente.");
            }
            return BadRequest("Error");

        }
        #endregion
        #region EquiposDeComputo

        //EquiposComputo 
        [HttpGet("DocumentosEquiposComputo/{EquipoComputoId}")]
        public async Task<IActionResult> GetDocumentosEquiposComputo(int EquipoComputoId)
        {
            var Documentos = await _context.ArchivosEquipoComputos
                .Include(i => i.EquipoComputo)
                .Where(w => w.EquipoComputoId == EquipoComputoId)
                .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Documentos == null)
            {
                return BadRequest("No se encuentran documentos registrados, ");
            }
            return Ok(Documentos);
        }
        [HttpGet("DocumentosEquiposComputo/Descargar/{EquipoComputoId}/{ArchivoComputoId}")]
        public async Task<IActionResult> DownloadFileEquiposComputo(int EquipoComputoId, int ArchivoComputoId)
        {
            try
            {

                var equipoComputo = await _context.EquipoComputo
                    .FirstOrDefaultAsync(f => f.EquipoComputoId == EquipoComputoId);
                if (equipoComputo == null)
                {
                    return NotFound();
                }
                var documento = await _context.ArchivosEquipoComputos
                    .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                    .FirstOrDefaultAsync(f => f.ArchivoEquipoId == ArchivoComputoId);
                if (documento == null)
                {
                    return NotFound("No se encuentra Archivo");
                }
                var serverPath = "ftp://digital.atogobmx.com/Files/Patrimonio/EquiposComputacion/";
                var nomenclaturaEquipo = equipoComputo.CodigoInventario.ToString();
                var filePath = documento.Nombre.ToString();
                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(serverPath + nomenclaturaEquipo + "/Documentos/" + filePath);
                var url = serverPath + nomenclaturaEquipo + "/Documentos/" + filePath;
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
                Console.WriteLine(e.Message);
                return NoContent();
            }

        }
        [HttpGet("DocumentosEquiposComputo/{EquipoComputoId}/Zip")]
        public async Task<IActionResult> DownloadFilesEquiposComputoZip(int EquipoComputoId)
        {
            var equipoComputo = await _context.EquipoComputo
                 .Where(w => !w.Archivado)
                 .FirstOrDefaultAsync(f => f.EquipoComputoId == EquipoComputoId);
            var UrlHost = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "Patrimonio", "EquiposComputacion", equipoComputo.CodigoInventario, "Documentos");
            var result = GetListFiles(UrlHost);
            foreach (string line in result)
            {
                copyFile(String.Format("ftp://{0}/{1}/{2}/{3}/{4}", "digital.atogobmx.com", "Files", "Patrimonio", "EquiposComputacion", equipoComputo.CodigoInventario) + "/" , line);
            }
            if (!Directory.Exists("Files/Documentos/"))
            {
                Directory.CreateDirectory("Files/Documentos/");
            }
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Files/Documentos/");
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
            var dir = new DirectoryInfo("Files/Documentos/");
            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
            dir.Delete(true);
            return File(zipFileMemoryStream, "application/octet-stream", $"Documentos_{DateOnly.FromDateTime(DateTime.Now)}_.zip");
        }
        [HttpPost("DocumentosEquiposComputo/{EquipoComputoId}/")]
        public async Task<IActionResult> UploadDocumentsEquipocomputo(List<IFormFile> Files, int EquipoComputoId)
        {
            #region Cargar Archivos
            try
            {

                #region Comprobar si el expediente existe
                var equipo = await _context.EquipoComputo
                    .FirstOrDefaultAsync(f => f.EquipoComputoId == EquipoComputoId);
                if (equipo == null)
                {
                    return NotFound("No se encuentra el expediente digital");
                }

                #endregion
                string serverPath = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "Patrimonio", "EquiposComputacion", equipo.CodigoInventario, "Documentos");
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

                        #region crear documento al servidor
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
                        var archivo = new ArchivosEquipoComputo()
                        {
                            ArchivoEquipoId = 0,
                            Nombre = file.FileName,
                            TipoArchivo = fileExtension,
                            EquipoComputoId = EquipoComputoId
                        };
                        _context.ArchivosEquipoComputos.Add(archivo);
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

        [HttpDelete("DocumentosEquiposComputo/Eliminar/{EquipoComputoId}/{ArchivoId}")]
        public async Task<IActionResult> DeleteDocumentsEquiposComputo(int EquipoComputoId, int ArchivoId)
        {
            var equipo = await _context.EquipoComputo
                .FirstOrDefaultAsync(f => f.EquipoComputoId == EquipoComputoId);

            if (equipo == null)
            {
                return NotFound();
            }
            var Archivo = await _context.ArchivosEquipoComputos
                .FirstOrDefaultAsync(f => f.ArchivoEquipoId == ArchivoId);

            if (Archivo == null)
            {
                return NotFound();
            }
            var serverPath = "ftp://digital.atogobmx.com/Files/Patrimonio/EquiposComputacion/";
            var Nomenclatura = equipo.CodigoInventario.ToString();
            var filePath = Archivo.Nombre.ToString();
            var Url = $"{serverPath + Nomenclatura + "/" + "Documentos/" + filePath}";
            var result = DeleteFile(Url);
            if (result)
            {
                Archivo.Archivado = true;
                _context.ArchivosEquipoComputos.Update(Archivo);
                await _context.SaveChangesAsync();
                return Ok("Documento archivado correctamente.");
            }
            return BadRequest("Error");

        }
        #endregion
        #region Vehiculos
        //Vehiculos
        [HttpGet("DocumentosVehiculo/{VehiculoId}")]
        public async Task<IActionResult> GetDocumentosVehiculos(int VehiculoId)
        {
            var Documentos = await _context.archivosVehiculos
                .Include(i => i.Vehiculo)
                .Where(w => w.VehiculoId == VehiculoId)
                .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Documentos == null)
            {
                return BadRequest("No se encuentran documentos registrados, ");
            }
            return Ok(Documentos);
        }
        [HttpGet("DocumentosVehiculo/Descargar/{VehiculoId}/{ArchivoVehiculoId}")]
        public async Task<IActionResult> DownloadFileVehiculos(int VehiculoId, int ArchivoVehiculoId)
        {
            try
            {

                var vehiculo = await _context.Vehiculo
                    .FirstOrDefaultAsync(f => f.VehiculoId == VehiculoId);
                if (vehiculo == null)
                {
                    return NotFound();
                }
                var documento = await _context.archivosVehiculos
                    .Include(i => i.Vehiculo)
                    .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                    .FirstOrDefaultAsync(f => f.ArchivoVehiculoId == ArchivoVehiculoId);
                if (documento == null)
                {
                    return NotFound("No se encuentra Archivo");
                }
                var serverPath = "ftp://digital.atogobmx.com/Files/Patrimonio/ParqueVehicular/";
                var nomenclaturaEquipo = vehiculo.Nomenclatura.ToString();
                var filePath = documento.Nombre.ToString();
                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(serverPath + nomenclaturaEquipo + "/Documentos/" + filePath);
                var url = serverPath + nomenclaturaEquipo + "/Documentos/" + filePath;
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
                Console.WriteLine(e.Message);
                return NoContent();
            }

        }
        [HttpGet("DocumentosVehiculo/{VehiculoId}/Zip")]
        public async Task<IActionResult> DownloadFilesVehiculosZip(int VehiculoId)
        {
            var vehiculo = await _context.Vehiculo
                 .Where(w => !w.Archivado)
                 .FirstOrDefaultAsync(f => f.VehiculoId == VehiculoId);
            var UrlHost = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "Patrimonio", "ParqueVehicular", vehiculo.Nomenclatura, "Documentos");
            var result = GetListFiles(UrlHost);
            foreach (string line in result)
            {
                copyFile(String.Format("ftp://{0}/{1}/{2}/{3}/{4}", "digital.atogobmx.com", "Files", "Patrimonio", "ParqueVehicular", vehiculo.Nomenclatura) + "/", line);
            }
            if (!Directory.Exists("Files/Documentos/"))
            {
                Directory.CreateDirectory("Files/Documentos/");
            }
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Files/Documentos/");
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
            var dir = new DirectoryInfo("Files/Documentos/");
            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
            dir.Delete(true);
            return File(zipFileMemoryStream, "application/octet-stream", $"Documentos_{DateOnly.FromDateTime(DateTime.Now)}_.zip");
        }
        [HttpPost("DocumentosVehiculo/{VehiculoId}/")]
        public async Task<IActionResult> UploadDocumentsVehiculo(List<IFormFile> Files, int VehiculoId)
        {
            #region Cargar Archivos
            try
            {

                #region Comprobar si el expediente existe
                var vehiculo = await _context.Vehiculo
                    .FirstOrDefaultAsync(f => f.VehiculoId == VehiculoId);
                if (vehiculo == null)
                {
                    return NotFound("No se encuentra el expediente digital");
                }

                #endregion
                string serverPath = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "Patrimonio", "ParqueVehicular", vehiculo.Nomenclatura, "Documentos");
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

                        #region crear documento al servidor
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
                        var archivo = new ArchivosVehiculos()
                        {
                            ArchivoVehiculoId = 0,
                            Nombre = file.FileName,
                            TipoArchivo = fileExtension,
                            VehiculoId = VehiculoId
                        };
                        _context.archivosVehiculos.Add(archivo);
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

        [HttpDelete("DocumentosVehiculo/Eliminar/{VehiculoId}/{ArchivoId}")]
        public async Task<IActionResult> DeleteDocumentsVehiculos(int VehiculoId, int ArchivoId)
        {
            var vehiculo = await _context.Vehiculo
                .FirstOrDefaultAsync(f => f.VehiculoId == VehiculoId);

            if (vehiculo == null)
            {
                return NotFound();
            }
            var Archivo = await _context.archivosVehiculos
                .FirstOrDefaultAsync(f => f.ArchivoVehiculoId == ArchivoId);

            if (Archivo == null)
            {
                return NotFound();
            }
            var serverPath = "ftp://digital.atogobmx.com/Files/Patrimonio/ParqueVehicular/";
            var Nomenclatura = vehiculo.Nomenclatura.ToString();
            var filePath = Archivo.Nombre.ToString();
            var Url = $"{serverPath + Nomenclatura + "/" + "Documentos/" + filePath}";
            var result = DeleteFile(Url);
            if (result)
            {
                Archivo.Archivado = true;
                _context.archivosVehiculos.Update(Archivo);
                await _context.SaveChangesAsync();
                return Ok("Documento archivado correctamente.");
            }
            return BadRequest("Error");

        }
        #endregion
        #region Mobiliario
        //Mobiliario
        [HttpGet("DocumentosMobiliario/{MobiliarioId}")]
        public async Task<IActionResult> GetDocumentosMobiliario(int MobiliarioId)
        {
            var Documentos = await _context.ArchivosMobiliarios
                .Include(i => i.Mobiliario)
                .Where(w => w.MobiliarioId == MobiliarioId)
                .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Documentos == null)
            {
                return BadRequest("No se encuentran documentos registrados, ");
            }
            return Ok(Documentos);
        }
        [HttpGet("DocumentosMobiliario/Descargar/{MobiliarioId}/{ArchivoMobiliarioId}")]
        public async Task<IActionResult> DownloadFileMobiliarios(int MobiliarioId, int ArchivoMobiliarioId)
        {
            try
            {

                var Mobiliario = await _context.Mobiliario
                    .FirstOrDefaultAsync(f => f.MobiliarioId == MobiliarioId);
                if (Mobiliario == null)
                {
                    return NotFound();
                }
                var documento = await _context.ArchivosMobiliarios
                    .Include(i => i.Mobiliario)
                    .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                    .FirstOrDefaultAsync(f => f.ArchivoMobiliarioId == ArchivoMobiliarioId);
                if (documento == null)
                {
                    return NotFound("No se encuentra Archivo");
                }
                var serverPath = "ftp://digital.atogobmx.com/Files/Patrimonio/Mobiliario/";
                var nomenclaturaEquipo = Mobiliario.CodigoInventario.ToString();
                var filePath = documento.Nombre.ToString();
                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(serverPath + nomenclaturaEquipo + "/Documentos/" + filePath);
                var url = serverPath + nomenclaturaEquipo + "/Documentos/" + filePath;
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
                Console.WriteLine(e.Message);
                return NoContent();
            }

        }
        [HttpGet("DocumentosMobiliario/{MobiliarioId}/Zip")]
        public async Task<IActionResult> DownloadFilesMobiliarioZip(int MobiliarioId)
        {
            var Mobiliario = await _context.Mobiliario
                 .Where(w => !w.Archivado)
                 .FirstOrDefaultAsync(f => f.MobiliarioId == MobiliarioId);
            var UrlHost = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "Patrimonio", "Mobiliario", Mobiliario.CodigoInventario, "Documentos");
            var result = GetListFiles(UrlHost);
            foreach (string line in result)
            {
                copyFile(String.Format("ftp://{0}/{1}/{2}/{3}/{4}", "digital.atogobmx.com", "Files", "Patrimonio", "Mobiliario", Mobiliario.CodigoInventario) + "/", line);
            }
            if (!Directory.Exists("Files/Documentos/"))
            {
                Directory.CreateDirectory("Files/Documentos/");
            }
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Files/Documentos/");
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
            var dir = new DirectoryInfo("Files/Documentos/");
            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
            dir.Delete(true);
            return File(zipFileMemoryStream, "application/octet-stream", $"Documentos_{DateOnly.FromDateTime(DateTime.Now)}_.zip");
        }
        [HttpPost("DocumentosMobiliario/{MobiliarioId}/")]
        public async Task<IActionResult> UploadDocumentsMobiliario(List<IFormFile> Files, int MobiliarioId)
        {
            #region Cargar Archivos
            try
            {

                #region Comprobar si el expediente existe
                var mobiliario = await _context.Mobiliario
                    .FirstOrDefaultAsync(f => f.MobiliarioId == MobiliarioId);
                if (mobiliario == null)
                {
                    return NotFound("No se encuentra el expediente digital");
                }

                #endregion
                string serverPath = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "Patrimonio", "Mobiliario", mobiliario.CodigoInventario, "Documentos");
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

                        #region crear documento al servidor
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
                        var archivo = new ArchivosMobiliario()
                        {
                            ArchivoMobiliarioId = 0,
                            Nombre = file.FileName,
                            TipoArchivo = fileExtension,
                            MobiliarioId = MobiliarioId
                        };
                        _context.ArchivosMobiliarios.Add(archivo);
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

        [HttpDelete("DocumentsMobiliario/Eliminar/{MobiliarioId}/{ArchivoId}")]
        public async Task<IActionResult> DeleteDocumentsMobiliario(int MobiliarioId, int ArchivoId)
        {
            var mobiliario = await _context.Mobiliario
                .FirstOrDefaultAsync(f => f.MobiliarioId == MobiliarioId);

            if (mobiliario == null)
            {
                return NotFound();
            }
            var Archivo = await _context.ArchivosMobiliarios
                .FirstOrDefaultAsync(f => f.ArchivoMobiliarioId == ArchivoId);

            if (Archivo == null)
            {
                return NotFound();
            }
            var serverPath = "ftp://digital.atogobmx.com/Files/Patrimonio/Mobiliario/";
            var Nomenclatura = mobiliario.CodigoInventario.ToString();
            var filePath = Archivo.Nombre.ToString();
            var Url = $"{serverPath + Nomenclatura + "/" + "Documentos/" + filePath}";
            var result = DeleteFile(Url);
            if (result)
            {
                Archivo.Archivado = true;
                _context.ArchivosMobiliarios.Update(Archivo);
                await _context.SaveChangesAsync();
                return Ok("Documento archivado correctamente.");
            }
            return BadRequest("Error");

        }

        #endregion
        #region Alumbrado publico (Falta DELETE)
        //--------------Documentos Alumbrado--------------------//
        [HttpGet("DocumentosAlumbradoPublico/{AlumbradoId}")]
        public async Task<IActionResult> GetDocumentosAlumbrado(int AlumbradoId)
        {
            var Documentos = await _context.ArchivosAlumbrado
                .Include(i => i.Alumbrado)
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

        [HttpGet("DocumentosAlumbradoPublico/Dowload/{AlumbradoId}/{ArchivoAlumbradoId}")]
        public async Task<IActionResult> DownloadFileAlumbrado(int AlumbradoId, int ArchivoAlumbradoId)
        {
            try
            {
                var alumbrado = await _context.Alumbrado
                    .FirstOrDefaultAsync(f => f.AlumbradoId == AlumbradoId);
                if (alumbrado == null)
                {
                    return NotFound("");
                }
                var documento = await _context.ArchivosAlumbrado
                    .Include(i =>  i.Alumbrado)
                    .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                    .FirstOrDefaultAsync(f => f.ArchivoAlumbradoId == ArchivoAlumbradoId);
                if (documento == null)
                {
                    return NotFound("No se encuentra Archivo");
                }
                var serverPath = "ftp://digital.atogobmx.com/Files/ServiciosPublicos/AlumbradoPublico/";
                var NombreObra = alumbrado.NombreObra.ToString();
                var filePath = documento.Nombre.ToString();
                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(serverPath + NombreObra + "/Documentos/" + filePath);
                var url = serverPath + NombreObra + "/Documentos/" + filePath;
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
                Console.WriteLine(e.Message);
                return NoContent();
            }

        }

        [HttpGet("DocumentosAlumbradoPublico/{AlumbradoId}/Zip")]
        public async Task<IActionResult> DownloadFilesAlumbradoZip(int AlumbradoId)
        {
            var alumbrado = await _context.Alumbrado
                 .Include(i => i.ArchivosAlumbrado)
                 .Where(w => !w.Archivado)
                 .FirstOrDefaultAsync(f => f.AlumbradoId == AlumbradoId);
            var UrlHost = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "ServiciosPublicos", "AlumbradoPublico", alumbrado.NombreObra, "Documentos");
            var result = GetListFiles(UrlHost);
            foreach (string line in result)
            {
                copyFile(String.Format("ftp://{0}/{1}/{2}/{3}/{4}", "digital.atogobmx.com", "Files", "ServiciosPublicos", "AlumbradoPublico", alumbrado.NombreObra) + "/", line);
            }
            if (!Directory.Exists("Files/Documentos/"))
            {
                Directory.CreateDirectory("Files/Documentos/");
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
            var dir = new DirectoryInfo("Files/Documentos");
            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
            dir.Delete(true);
            //Directory.Delete("Files/Documentos");
            return File(zipFileMemoryStream, "application/octet-stream", $"Documentos_{DateOnly.FromDateTime(DateTime.Now)}_.zip");
        }
        [HttpPost("DocumentosAlumbradoPublico/{AlumbradoId}/")]
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
                    return NotFound("No se encuentra el expediente digital");
                }
                #endregion
                string serverPath = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "ServiciosPublicos", "AlumbradoPublico", alumbrado.NombreObra, "Documentos");
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

        [HttpDelete("DocumentosAlumbradoPublico/Eliminar/{AlumbradoId}/{ArchivoId}")]
        public async Task<IActionResult> DeleteDocumentsAlumbrado(int AlumbradoId, int ArchivoId)
        {
            var alumbrado = await _context.Alumbrado
                .FirstOrDefaultAsync(f => f.AlumbradoId == AlumbradoId);

            if (alumbrado == null)
            {
                return NotFound();
            }
            var Archivo = await _context.ArchivosAlumbrado
                .FirstOrDefaultAsync(f => f.ArchivoAlumbradoId == ArchivoId);

            if (Archivo == null)
            {
                return NotFound();
            }
            var serverPath = "ftp://digital.atogobmx.com/Files/ServiciosPublicos/AlumbradoPublico/";
            var NombreServicio = alumbrado.NombreObra.ToString();
            var filePath = Archivo.Nombre.ToString();
            var Url = $"{serverPath + NombreServicio + "/" + "Documentos" + "/" + filePath}";
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
        #endregion
        #region Cementerios (Falta LISTA Nombres archivos y descarga individual)
        [HttpGet("DocumentosCementerioPublico/{CementerioId}")]
        public async Task<IActionResult> GetDocumentoCementerio(int CementerioId)
        {
            var Documentos = await _context.ArchivosCementerios
                .Include(i => i.Cementerio)
                .Where(w => w.CementerioId == CementerioId)
                .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Documentos == null)
            {
                return BadRequest("No se encuentran documentos registrados, ");
            }
            return Ok(Documentos);
        }

        [HttpGet("DocumentosCementerioPublico/Dowload/{CementerioId}/{ArchivosCementeriosId}")]
        public async Task<IActionResult> DownloadFileCementerio(int CementerioId, int ArchivosCementeriosId)
        {
            try
            {
                var aseo = await _context.Cementerio
                    //.Include(i => i.ArchivosAseo)
                    .FirstOrDefaultAsync(f => f.CementerioId == CementerioId);
                if (aseo == null)
                {
                    return NotFound("El ID del alumbrado no existe");
                }
                var documento = await _context.ArchivosCementerios
                    .Include(i => i.Cementerio)
                    .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                    .FirstOrDefaultAsync(f => f.ArchivosCementerioId == ArchivosCementeriosId);
                if (documento == null)
                {
                    return NotFound("No se encuentra Archivo");
                }
                var serverPath = "ftp://digital.atogobmx.com/Files/ServiciosPublicos/CementerioPublico/";
                var NombreServicio = aseo.NombreCementerio.ToString();
                var filePath = documento.Nombre.ToString();
                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(serverPath + NombreServicio + "/Documentos/" + filePath);
                var url = serverPath + NombreServicio + "/Documentos/" + filePath;
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
                Console.WriteLine(e.Message);
                return NoContent();
            }

        }

        [HttpGet("DocumentosCementerioPublico/{CementerioId}/Zip")]
        public async Task<IActionResult> DownloadCementeryZip(int CementerioId)
        {
            var vehiculo = await _context.Cementerio
                 .Where(w => !w.Archivado)
                 .FirstOrDefaultAsync(f => f.CementerioId == CementerioId);
            var UrlHost = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "ServiciosPublicos", "CementerioPublico", vehiculo.NombreCementerio, "Documentos");
            var result = GetListFiles(UrlHost);
            foreach (string line in result)
            {
                copyFile(String.Format("ftp://{0}/{1}/{2}/{3}/{4}", "digital.atogobmx.com", "Files", "ServiciosPublicos", "CementerioPublico", vehiculo.NombreCementerio) + "/", line);
            }
            if (!Directory.Exists("Files/Documentos/"))
            {
                Directory.CreateDirectory("Files/Documentos/");
            }
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Files/Documentos/");
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
            var dir = new DirectoryInfo("Files/Documentos");
            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
            dir.Delete(true);
            return File(zipFileMemoryStream, "application/octet-stream", $"Documentos_{DateOnly.FromDateTime(DateTime.Now)}_.zip");
        }

        [HttpPost("DocumentosCementerioPublico/{CementerioId}/")]
        public async Task<IActionResult> UploadDocumentsCementerio(List<IFormFile> Files, int CementerioId)
        {
            #region Cargar Archivos
            try
            {

                #region Comprobar si el expediente existe
                var direccioncementerio = await _context.Cementerio
                    //.Include(i => i.TareaTipoAlumbrado)
                    .FirstOrDefaultAsync(f => f.CementerioId == CementerioId);

                if (direccioncementerio == null)
                {
                    return NotFound("No se encuentra el expediente digital");
                }

                string serverPath = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "ServiciosPublicos", "CementerioPublico", direccioncementerio.NombreCementerio, "Documentos");
                 

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
                        var archivo = new ArchivosCementerios()
                        {
                            ArchivosCementerioId = 0,
                            Nombre = file.FileName,
                            TipoArchivo = fileExtension,
                            CementerioId = CementerioId
                        };
                        _context.ArchivosCementerios.Add(archivo);
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

        [HttpDelete("DocumentosCementerio/Eliminar/{CementerioId}/{ArchivoId}")]
        public async Task<IActionResult> DeleteDocumentsCementerio(int CementerioId, int ArchivoId)
        {
            var cementerio = await _context.Cementerio
                .FirstOrDefaultAsync(f => f.CementerioId == CementerioId);

            if (cementerio == null)
            {
                return NotFound();
            }
            var Archivo = await _context.ArchivosCementerios
                .FirstOrDefaultAsync(f => f.ArchivosCementerioId == ArchivoId);

            if (Archivo == null)
            {
                return NotFound();
            }
            var serverPath = "ftp://digital.atogobmx.com/Files/ServiciosPublicos/CementerioPublico/";
            var NombreCementerio = cementerio.NombreCementerio.ToString();
            var filePath = Archivo.Nombre.ToString();
            var Url = $"{serverPath + NombreCementerio + "/" + "Documentos" + "/" + filePath}";
            var result = DeleteFile(Url);
            if (result)
            {
                Archivo.Archivado = true;
                _context.ArchivosCementerios.Update(Archivo);
                await _context.SaveChangesAsync();
                return Ok("Documento archivado correctamente.");
            }
            return BadRequest("Error");

        }
        #endregion
        #region Aseo Publico
        [HttpGet("DocumentosAseoPublico/{AseoId}")]
        public async Task<IActionResult> GetDocumentosAseo(int AseoId)
        {
            var Documentos = await _context.ArchivosAseo
                .Include(i => i.Aseo)
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

        [HttpGet("DocumentosAseoPublico/Dowload/{AseoId}/{ArchivosAseoId}")]
        public async Task<IActionResult> DownloadFileAseo(int AseoId, int ArchivosAseoId)
        {
            try
            {
                var aseo = await _context.Aseo
                    //.Include(i => i.ArchivosAseo)
                    .FirstOrDefaultAsync(f => f.AseoId == AseoId);
                if (aseo == null)
                {
                    return NotFound("El ID del alumbrado no existe");
                }
                var documento = await _context.ArchivosAseo
                    .Include(i => i.Aseo)
                    .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                    .FirstOrDefaultAsync(f => f.ArchivosAseoId == ArchivosAseoId);
                if (documento == null)
                {
                    return NotFound("No se encuentra Archivo");
                }
                var serverPath = "ftp://digital.atogobmx.com/Files/ServiciosPublicos/AseoPublico/";
                var NombreServicio = aseo.Nombre.ToString();
                var filePath = documento.Nombre.ToString();
                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(serverPath + NombreServicio + "/Documentos/" + filePath);
                var url = serverPath + NombreServicio + "/Documentos/" + filePath;
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
                Console.WriteLine(e.Message);
                return NoContent();
            }

        }

        [HttpGet("DocumentosAseoPublico/{AseoId}/Zip")]
        public async Task<IActionResult> DownloadFilesAseo(int AseoId)
        {
            var aseo = await _context.Aseo
                 .Include(i => i.ArchivosAseo)
                 .Where(w => !w.Archivado)
                 .FirstOrDefaultAsync(f => f.AseoId == AseoId);
            var UrlHost = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "ServiciosPublicos", "AseoPublico", aseo.Nombre, "Documentos");

            var result = GetListFiles(UrlHost);
            foreach (string line in result)
            {
                copyFile(String.Format("ftp://{0}/{1}/{2}/{3}/{4}", "digital.atogobmx.com", "Files", "ServiciosPublicos", "AseoPublico", aseo.Nombre) + "/", line);
            }
            if (!Directory.Exists("Files/Documentos/"))
            {
                Directory.CreateDirectory("Files/Documentos/");
            }
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Files/Documentos/");
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
            var dir = new DirectoryInfo("Files/Documentos");
            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
            dir.Delete(true);
            //Directory.Delete("Files/Documentos");
            return File(zipFileMemoryStream, "application/octet-stream", $"Documentos_{DateOnly.FromDateTime(DateTime.Now)}_.zip");
        }

        [HttpPost("DocumentosAseoPublico/{AseoId}/")]
        public async Task<IActionResult> UploadDocumentsAseo(List<IFormFile> Files, int AseoId)
        {
            #region Cargar Archivos
            try
            {

                #region Comprobar si el expediente existe
                var aseo = await _context.Aseo
                    //.Include(i => i.TareaTipoAlumbrado)
                    .FirstOrDefaultAsync(f => f.AseoId == AseoId);

                if (aseo == null)
                {
                    return NotFound("No se encuentra el expediente digital");
                }

                string serverPath = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "ServiciosPublicos", "AseoPublico", aseo.Nombre, "Documentos");
                 

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
                        var archivo = new ArchivosAseo()
                        {
                            ArchivosAseoId = 0,
                            Nombre = file.FileName,
                            TipoArchivo = fileExtension,
                            AseoId = AseoId
                        };
                        _context.ArchivosAseo.Add(archivo);
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

        [HttpDelete("DocumentosAseoPublico/Eliminar/{AseoId}/{ArchivoId}")]
        public async Task<IActionResult> DeleteDocumentsAseo(int AseoId, int ArchivoId)
        {
            var aseo = await _context.Aseo
                .FirstOrDefaultAsync(f => f.AseoId == AseoId);

            if (aseo == null)
            {
                return NotFound();
            }
            var Archivo = await _context.ArchivosAseo
                .FirstOrDefaultAsync(f => f.ArchivosAseoId == ArchivoId);

            if (Archivo == null)
            {
                return NotFound();
            }
            var serverPath = "ftp://digital.atogobmx.com/Files/ServiciosPublicos/AseoPublico/";
            var NombreServicio = aseo.Nombre.ToString();
            var filePath = Archivo.Nombre.ToString();
            var Url = $"{serverPath + NombreServicio + "/" + "Documentos" + "/" + filePath}";
            var result = DeleteFile(Url);
            if (result)
            {
                Archivo.Archivado = true;
                _context.ArchivosAseo.Update(Archivo);
                await _context.SaveChangesAsync();
                return Ok("Documento archivado correctamente.");
            }
            return BadRequest("Error");

        }
        #endregion
        #region Obras Publicas
        [HttpGet("DocumentosObrasPublicas/{ObraId}")]
        public async Task<IActionResult> GetDocumentosObras(int ObraId)
        {
            var Documentos = await _context.ArchivosObras
                .Include(i => i.OP_Obras)
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

        [HttpGet("DocumentosObrasPublicas/Dowload/{ObraId}/{ArchivosObrasId}")]
        public async Task<IActionResult> DownloadFileObrasPublicas(int ObraId, int ArchivosObrasId)
        {
            try
            {
                var ObraPublica = await _context.ObrasPublicas
                    .FirstOrDefaultAsync(f => f.ObraId == ObraId);
                if (ObraPublica == null)
                {
                    return NotFound("El ID del alumbrado no existe");
                }
                var documento = await _context.ArchivosObras
                    .Include(i => i.OP_Obras)
                    .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                    .FirstOrDefaultAsync(f => f.ArchivosObrasId == ArchivosObrasId);
                if (documento == null)
                {
                    return NotFound("No se encuentra Archivo");
                }
                var serverPath = "ftp://digital.atogobmx.com/Files/ObrasPublicas/";
                var Nombre = ObraPublica.Nombre.ToString();
                var filePath = documento.Nombre.ToString();
                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(serverPath + Nombre + "/Documentos/" + filePath);
                var url = serverPath + Nombre + "/Documentos/" + filePath;
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
                Console.WriteLine(e.Message);
                return NoContent();
            }

        }

        [HttpGet("DocumentosObrasPublicas/{ObraId}/Zip")]
        public async Task<IActionResult> DownloadFilesObrasPublicas(int ObraId)
        {
            var obrapublica = await _context.ObrasPublicas
                 .Include(i => i.ArchivosObras)
                 .Where(w => !w.Archivado)
                 .FirstOrDefaultAsync(f => f.ObraId == ObraId);
            var UrlHost = String.Format("ftp://{0}/{1}/{2}/{3}/{4}", "digital.atogobmx.com", "Files", "ObrasPublicas", obrapublica.Nombre, "Documentos");
            
            var result = GetListFiles(UrlHost);
            foreach (string line in result)
            {
                copyFile(String.Format("ftp://{0}/{1}/{2}/{3}", "digital.atogobmx.com", "Files", "ObrasPublicas", obrapublica.Nombre) + "/", line);
            }
            if (!Directory.Exists("Files/Documentos/"))
            {
                Directory.CreateDirectory("Files/Documentos/");
            }
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Files/Documentos/");
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
            var dir = new DirectoryInfo("Files/Documentos");
            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
            dir.Delete(true);
            //Directory.Delete("Files/Documentos");
            return File(zipFileMemoryStream, "application/octet-stream", $"Documentos_{DateOnly.FromDateTime(DateTime.Now)}_.zip");
        }

        [HttpPost("DocumentosObrasPublicas/{ObraId}/")]
        public async Task<IActionResult> UploadDocumentsObrasPublicas(List<IFormFile> Files, int ObraId)
        {
            #region Cargar Archivos
            try
            {

                #region Comprobar si el expediente existe
                var obraspublicas = await _context.ObrasPublicas
                    .FirstOrDefaultAsync(f => f.ObraId == ObraId);

                if (obraspublicas == null)
                {
                    return NotFound("No se encuentra el expediente digital");
                }
                 
                string serverPath = String.Format("ftp://{0}/{1}/{2}/{3}/{4}", "digital.atogobmx.com", "Files", "ObrasPublicas", obraspublicas.Nombre, "Documentos");

                

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
                        var archivo = new ArchivosObras()
                        {
                            ArchivosObrasId = 0,
                            Nombre = file.FileName,
                            TipoArchivo = fileExtension,
                            ObraId = ObraId
                        };
                        _context.ArchivosObras.Add(archivo);
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

        [HttpDelete("DocumentosObrasPublicas/Eliminar/{ObraId}/{ArchivoId}")]
        public async Task<IActionResult> DeleteDocumentsObrasPublicas(int ObraId, int ArchivoId)
        {
            var obraspublicas = await _context.ObrasPublicas
                .FirstOrDefaultAsync(f => f.ObraId == ObraId);

            if (obraspublicas == null)
            {
                return NotFound();
            }
            var Archivo = await _context.ArchivosObras
                .FirstOrDefaultAsync(f => f.ArchivosObrasId == ArchivoId);

            if (Archivo == null)
            {
                return NotFound();
            }
            var serverPath = "ftp://digital.atogobmx.com/Files/ObrasPublicas/";
            var Nombre = obraspublicas.Nombre.ToString();
            var filePath = Archivo.Nombre.ToString();
            var Url = $"{serverPath + Nombre + "/" + "Documentos" + "/" + filePath}";
            var result = DeleteFile(Url);
            if (result)
            {
                Archivo.Archivado = true;
                _context.ArchivosObras.Update(Archivo);
                await _context.SaveChangesAsync();
                return Ok("Documento archivado correctamente.");
            }
            return BadRequest("Error");

        }
        #endregion
        #region metodos servidor
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
                Console.Write(ex.Message);
                return false;
            }
        }
        private static bool copyFile(string url,string line)
        {
            try
            {
                FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(url + line);
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
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
        private static List<String> GetListFiles(string Url)
        {
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
        #endregion
        #region Armeria
        //Armeria
        [HttpGet("DocumentosArmeria/{ArmaId}")]
        public async Task<IActionResult> GetDocumentosArmeria(int ArmaId)
        {
            var Documentos = await _context.ArchivosArmeria
                .Include(i => i.Armeria)
                .Where(w => w.ArmaId == ArmaId)
                .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                .Where(w => !w.Archivado)
                .ToListAsync();
            if (Documentos == null)
            {
                return BadRequest("No se encuentran documentos registrados, ");
            }
            return Ok(Documentos);
        }
        [HttpGet("DocumentosArmeria/Descargar/{ArmaId}/{ArchivoArmeriaId}")]
        public async Task<IActionResult> DownloadFileArmeria(int ArmaId, int ArchivoArmeriaId)
        {
            try
            {

                var armeria = await _context.Armeria
                    .FirstOrDefaultAsync(f => f.ArmaId == ArmaId);
                if (armeria == null)
                {
                    return NotFound();
                }
                var documento = await _context.ArchivosArmeria
                    .Include(i => i.Armeria)
                    .Where(w => w.TipoArchivo == ".pdf" || w.TipoArchivo == ".docx")
                    .FirstOrDefaultAsync(f => f.ArchivoArmeriaId == ArchivoArmeriaId);
                if (documento == null)
                {
                    return NotFound("No se encuentra Archivo");
                }
                var serverPath = "ftp://digital.atogobmx.com/Files/Patrimonio/Armeria/";
                var nomenclaturaEquipo = armeria.Nomenclatura.ToString();
                var filePath = documento.Nombre.ToString();
                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(serverPath + nomenclaturaEquipo + "/Documentos/" + filePath);
                var url = serverPath + nomenclaturaEquipo + "/Documentos/" + filePath;
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
                Console.WriteLine(e.Message);
                return NoContent();
            }

        }
        [HttpGet("DocumentosArmeria/{ArmaId}/Zip")]
        public async Task<IActionResult> DownloadFilesArmeriaZip(int ArmaId)
        {
            var armeria = await _context.Armeria
                 .Where(w => !w.Archivado)
                 .FirstOrDefaultAsync(f => f.ArmaId == ArmaId);
            var UrlHost = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "Patrimonio", "Armeria", armeria.Nomenclatura, "Documentos");
            var result = GetListFiles(UrlHost);
            foreach (string line in result)
            {
                copyFile(String.Format("ftp://{0}/{1}/{2}/{3}/{4}", "digital.atogobmx.com", "Files", "Patrimonio", "Mobiliario", armeria.Nomenclatura) + "/", line);
            }
            if (!Directory.Exists("Files/Documentos"))
            {
                Directory.CreateDirectory("Files/Documentos");
            }
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Files/Documentos/");
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
            var dir = new DirectoryInfo("Files/Documentos/");
            dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
            dir.Delete(true);
            return File(zipFileMemoryStream, "application/octet-stream", $"Documentos_{DateOnly.FromDateTime(DateTime.Now)}_.zip");
        }
        [HttpPost("DocumentosArmeria/{ArmaId}/")]
        public async Task<IActionResult> UploadDocumentsArmeria(List<IFormFile> Files, int ArmaId)
        {
            #region Cargar Archivos
            try
            {

                #region Comprobar si el expediente existe
                var armeria = await _context.Armeria
                    .FirstOrDefaultAsync(f => f.ArmaId == ArmaId);
                if (armeria == null)
                {
                    return NotFound("No se encuentra el expediente digital");
                }

                #endregion
                string serverPath = String.Format("ftp://{0}/{1}/{2}/{3}/{4}/{5}", "digital.atogobmx.com", "Files", "Patrimonio", "Armeria", armeria.Nomenclatura, "Documentos");
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

                        #region crear documento al servidor
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
                        var archivo = new ArchivosArmeria()
                        {
                            ArchivoArmeriaId = 0,
                            Nombre = file.FileName,
                            TipoArchivo = fileExtension,
                            ArmaId = ArmaId
                        };
                        _context.ArchivosArmeria.Add(archivo);
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

        [HttpDelete("DocumentosArmeria/Eliminar/{ArmaId}/{ArchivoId}")]
        public async Task<IActionResult> DeleteDocumentsArmeria(int ArmaId, int ArchivoId)
        {
            var armeria = await _context.Armeria
                .FirstOrDefaultAsync(f => f.ArmaId == ArmaId);

            if (armeria == null)
            {
                return NotFound();
            }
            var Archivo = await _context.ArchivosArmeria
                .FirstOrDefaultAsync(f => f.ArchivoArmeriaId == ArchivoId);

            if (Archivo == null)
            {
                return NotFound();
            }
            var serverPath = "ftp://digital.atogobmx.com/Files/Patrimonio/Mobiliario/";
            var Nomenclatura = armeria.Nomenclatura.ToString();
            var filePath = Archivo.Nombre.ToString();
            var Url = $"{serverPath + Nomenclatura + "/" + "Documentos/" + filePath}";
            var result = DeleteFile(Url);
            if (result)
            {
                Archivo.Archivado = true;
                _context.ArchivosArmeria.Update(Archivo);
                await _context.SaveChangesAsync();
                return Ok("Documento archivado correctamente.");
            }
            return BadRequest("Error");

        }
        #endregion

    }
}
