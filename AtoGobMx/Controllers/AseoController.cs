using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AtoGobMx.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AseoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public AseoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Aseo>> GetAseo()
        {
            var ase = await _context.Aseo
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<Aseo>(s))
                .ToArrayAsync();
            return Ok(ase);
        }

        [HttpGet("{AseoId}")]
        public async Task<ActionResult> GetAseoById(int AseoId)
        {
            var aseo = await _context.Aseo
                //.Include(i => i.TareaTipoAlumbrado)
                //.Include(i => i.Estatus)
                .FirstOrDefaultAsync(f => f.AseoId == AseoId);
            if (aseo == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(aseo);
        }

        [HttpPost]
        public async Task<ActionResult<Aseo>> PostAseo(Aseo aseo)
        {
            //object value = _context.Aseo.Add(aseo);
            _context.Aseo.Add(aseo);
            aseo.Fecha = DateTime.Now;
            var aseopublico = aseo.Nombre.ToString();
            var host = "ftp://digital.atogobmx.com/Files/ServiciosPublicos/AseoPublico/";
            WebRequest request = WebRequest.Create(host + aseopublico);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                request.Abort();
                resp.Close();
            }
            CreateDocument(host + aseopublico);
            _context.Aseo.Add(aseo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAseoById", new { AseoId = aseo.AseoId }, aseo);
            //return Ok("Aseo creado correctamente");
        }

        [HttpPut("{AseoId}")]
        public async Task<ActionResult> PutAseo(int AseoId, Aseo aseo)
        {
            if (aseo.AseoId != AseoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var aseopublico = await _context.Aseo.FirstOrDefaultAsync(f => f.AseoId == AseoId);
            if (aseopublico == null)
            {
                return BadRequest("El Registro del aseo no existe");
            }
            if (!aseo.Nombre.Equals(aseopublico.Nombre))
            {
                try
                {
                    string serverUri = $"ftp://digital.atogobmx.com/Files/ServiciosPublicos/AseoPublico/{aseopublico.Nombre}";
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                    request.Method = WebRequestMethods.Ftp.Rename;
                    request.Proxy = null;
                    request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
                    request.RenameTo = $"{aseo.Nombre}";
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    Stream respStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(respStream);
                    string streamContent = reader.ReadToEnd();
                    respStream.Close();
                    response.Close();

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            aseopublico.AseoId = AseoId;
            aseopublico.Nombre = aseo.Nombre;
            aseopublico.Fecha = aseo.Fecha;
            aseopublico.Problema  = aseo.Problema;
            aseopublico.Domicilio = aseo.Domicilio;
            aseopublico.Descripcion = aseo.Descripcion;
            aseopublico.Archivado = aseo.Archivado;

            _context.Aseo.Update(aseopublico);
            await _context.SaveChangesAsync();
            return Ok("Servicio publico aseo actualizado correctamente");
        }

        [HttpDelete("{AseoId}")]
        public async Task<IActionResult> DeleteAseo(int AseoId)
        {
            var aseopublico = _context.Aseo
                .FirstOrDefault(f => f.AseoId == AseoId);
            if (aseopublico == null)
            {
                return NotFound();
            }

            aseopublico.Archivado = true;
            _context.Aseo.Update(aseopublico);
            await _context.SaveChangesAsync();
            return Ok("Servicio publico aseo Archivado");
        }
        private static bool CreateDocument(string url)
        {
            try
            {
                var pathDocument = "/Documentos";
                WebRequest request = WebRequest.Create(url + pathDocument);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    request.Abort();
                    resp.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
