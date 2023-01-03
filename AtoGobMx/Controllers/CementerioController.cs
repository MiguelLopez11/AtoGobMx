using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Cementerio = AtoGobMx.Models.Cementerio;

namespace AtoGobMx.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CementerioController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public CementerioController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Cementerio>> GetCementerios()
        {
            var cementerios = await _context.Cementerio
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<Cementerio>(s))
                .ToArrayAsync();
            return Ok(cementerios);
        }

        [HttpGet("{CementerioId}")]
        public async Task<ActionResult> GetCementeriosById(int CementerioId)
        {
            var cementerios = await _context.Cementerio
                .FirstOrDefaultAsync(f => f.CementerioId == CementerioId);
            if (cementerios == null)
            {
                return NotFound();
            }
            return Ok(cementerios);
        }

        [HttpPost]
        public async Task<ActionResult> PostCementerios(Cementerio cementerio)
        {
            var direccioncementerios = cementerio.NombreCementerio.ToString();
            var host = "ftp://digital.atogobmx.com/Files/ServiciosPublicos/CementerioPublico/";
            WebRequest request = WebRequest.Create(host + direccioncementerios);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                request.Abort();
                resp.Close();
            }
            CreateDocument(host+direccioncementerios);
            _context.Cementerio.Add(cementerio);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCementeriosById", new { CementerioId = cementerio.CementerioId }, cementerio);
        }

        [HttpPut("{CementerioId}")]
        public async Task<ActionResult> PutCementerios(int CementerioId, Cementerio cementerio)
        {
            if (cementerio.CementerioId != CementerioId)
            {
                return Ok("Lo ID no ingresados no coinciden");
            }

            var cementerios = await _context.Cementerio.FirstOrDefaultAsync(f => f.CementerioId == CementerioId);
            if (cementerios == null)
            {
                return BadRequest("El registro del cementerio no existe");
            }

            cementerios.CementerioId = CementerioId;
            cementerios.NombreCementerio = cementerio.NombreCementerio;
            cementerios.Comunidad = cementerio.Comunidad;
            cementerios.Localidad = cementerio.Localidad;
            cementerios.Calle = cementerio.Calle;
            cementerios.Latitud = cementerio.Latitud;
            cementerios.Longitud = cementerio.Longitud;
            cementerios.Archivado = cementerio.Archivado;

            _context.Cementerio.Update(cementerios);
            await _context.SaveChangesAsync();
            return Ok("Direccion cementerio se actualizo correcta mente");
        }

        [HttpDelete("{CementerioId}")]
        public async Task<ActionResult> DeleteDireccionCementerios(int CementerioId)
        {
            var cementerio = _context.Cementerio
                .FirstOrDefault(f => f.CementerioId == CementerioId);
            if (cementerio == null)
            {
                return NotFound();
            }

            cementerio.Archivado = true;
            _context.Cementerio.Update(cementerio);
            await _context.SaveChangesAsync();
            return Ok("Cementerio archivado");
        }
        private static bool CreateDocument(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url + "/Documentos");
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
