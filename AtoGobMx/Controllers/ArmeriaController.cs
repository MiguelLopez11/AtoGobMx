using AtoGobMx.Context;
using AtoGobMx.Models;
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
    public class ArmeriaController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public ArmeriaController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PAT_Armeria>>> GetArmeria()
        {
            var Estatus = await _context.Armeria
                .Include(i => i.Empleado)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Estatus);
        }

        [HttpGet("{ArmaId}")]
        public async Task<ActionResult<PAT_Armeria>> GetArmaById(int ArmaId)
        {
            var armeria = await _context.Armeria
                .Include(i => i.Empleado)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.ArmaId == ArmaId);

            if (armeria == null)
            {
                return NotFound();
            }

            return Ok(armeria);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_Armeria>> PostArmeria(PAT_Armeria armeria)
        {
            var Cartucho = armeria.Nomenclatura.ToString();
            var host = "ftp://digital.atogobmx.com/Files/Patrimonio/Armeria/";
            #region Create directory employee
            WebRequest request = WebRequest.Create(host + Cartucho);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                request.Abort();
                resp.Close();
            }
            #endregion
            var documentPath = CreateDocument(host + Cartucho);
            _context.Armeria.Add(armeria);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Arma registrada exitosamente");
        }
        [HttpPut("{ArmaId}")]
        public async Task<IActionResult> PutArmeria(int ArmaId, PAT_Armeria armeria)
        {
            if (armeria.ArmaId != ArmaId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var arma = await _context.Armeria.FindAsync(ArmaId);
            if (arma == null)
            {
                return NotFound();
            }
            if (!armeria.Nomenclatura.Equals(arma.Nomenclatura))
            {
                try
                {
                    string serverUri = $"ftp://digital.atogobmx.com/Files/Patrimonio/Armeria/{arma.Nomenclatura}";
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                    request.Method = WebRequestMethods.Ftp.Rename;
                    request.Proxy = null;
                    request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
                    request.RenameTo = $"{armeria.Nomenclatura}";
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
            arma.ArmaId = armeria.ArmaId;
            arma.Nomenclatura = armeria.Nomenclatura;
            arma.Marca = armeria.Marca;
            arma.TipoArma = armeria.TipoArma;
            arma.Calibre = armeria.Calibre;
            arma.EmpleadoId = armeria.EmpleadoId;
            arma.FechaAdquisicion = armeria.FechaAdquisicion;
            arma.Costo = armeria.Costo;
            arma.Archivado = armeria.Archivado;

            _context.Armeria.Update(arma);
            await _context.SaveChangesAsync();
            return Ok("Armeria actualizado correctamente");
        }
        [HttpDelete("{ArmaId}")]
        public async Task<IActionResult> DeleteArma(int ArmaId)
        {
            var arma = await _context.Armeria
                .FirstOrDefaultAsync(f => f.ArmaId == ArmaId);
            if (arma == null)
            {
                return NotFound();
            }
            arma.Archivado = true;
            _context.Armeria.Update(arma);
            await _context.SaveChangesAsync();
            return Ok("Arma archivado");
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
