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
    public class PAT_VehiculosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public PAT_VehiculosController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PAT_Vehiculo>>> GetVehiculos()
        {
            var vehiculos = await _context.Vehiculo
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(vehiculos);
        }

        [HttpGet("{VehiculoId}")]
        public async Task<ActionResult<PAT_Vehiculo>> GetVehiculo(int VehiculoId)
        {
            var vehiculo = await _context.Vehiculo.FirstOrDefaultAsync(f => f.VehiculoId == VehiculoId);

            if (vehiculo == null)
            {
                return NotFound();
            }

            return Ok(vehiculo);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_Vehiculo>> PostMonitor(PAT_Vehiculo vehiculo)
        {
            var Nomenclatura = vehiculo.Nomenclatura.ToString();
            var host = "ftp://digital.atogobmx.com/Files/Patrimonio/ParqueVehicular/";
            #region Create directory patrimonio
            WebRequest request = WebRequest.Create(host + Nomenclatura);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                request.Abort();
                resp.Close();
            }
            #endregion
            CreateDocument(host + Nomenclatura);
            _context.Vehiculo.Add(vehiculo);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{VehiculoId}")]
        public async Task<IActionResult> PutVehiculo(int VehiculoId, PAT_Vehiculo vehiculo)
        {
            if (vehiculo.VehiculoId != VehiculoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var Vehiculo = await _context.Vehiculo.FindAsync(VehiculoId);
            if (Vehiculo == null)
            {
                return NotFound();
            }
            if (!vehiculo.Nomenclatura.Equals(Vehiculo.Nomenclatura))
            {
                try
                {
                    string serverUri = $"ftp://digital.atogobmx.com/Files/ServiciosPublicos/AseoPublico/{vehiculo.Nomenclatura}";
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                    request.Method = WebRequestMethods.Ftp.Rename;
                    request.Proxy = null;
                    request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
                    request.RenameTo = $"{vehiculo.Nomenclatura}";
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
            Vehiculo.VehiculoId = vehiculo.VehiculoId;
            Vehiculo.Nomenclatura = vehiculo.Nomenclatura;
            Vehiculo.ProveedorId = vehiculo.ProveedorId;
            Vehiculo.NumeroFactura = vehiculo.NumeroFactura;
            Vehiculo.Marca = vehiculo.Marca;
            Vehiculo.Modelo = vehiculo.Modelo;
            Vehiculo.Color = vehiculo.Color;
            Vehiculo.Placa = vehiculo.Placa;
            Vehiculo.NumeroSerie = vehiculo.NumeroSerie;
            Vehiculo.AñoLanzamiento = vehiculo.AñoLanzamiento;
            Vehiculo.Puertas = vehiculo.Puertas;
            Vehiculo.Transmisión = vehiculo.Transmisión;
            Vehiculo.Version = vehiculo.Version;
            Vehiculo.Cilindros = vehiculo.Cilindros;
            Vehiculo.Observaciones = vehiculo.Observaciones;
            Vehiculo.Archivado = vehiculo.Archivado;

            _context.Vehiculo.Update(Vehiculo);
            await _context.SaveChangesAsync();
            return Ok("Monitor actualizado correctamente");
        }
        [HttpDelete("{VehiculoId}")]
        public async Task<IActionResult> DeleteVehiculo(int VehiculoId)
        {
            var vehiculo = await _context.Vehiculo
                .FirstOrDefaultAsync(f => f.VehiculoId == VehiculoId);
            if (vehiculo == null)
            {
                return NotFound();
            }
            vehiculo.Archivado = true;
            _context.Vehiculo.Update(vehiculo);
            await _context.SaveChangesAsync();
            return Ok("Producto archivado");
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
