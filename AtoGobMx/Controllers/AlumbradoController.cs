using AtoGobMx.Auth;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AlumbradoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public AlumbradoController(AtoGobMxContext Context, IMapper mapper)
        {
            _context = Context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Alumbrado>> GetAlumbrado()
        {
            var alumbrado = await _context.Alumbrado
                .Include(i => i.TareaTipoAlumbrado)
                .Include(i => i.Estatus)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<Alumbrado>(s))
                .ToArrayAsync();
            return Ok(alumbrado);
        }

        [HttpGet("ServiciosSinExpediente")]
        public async Task<ActionResult> GetServiciosSinExpedientes()
        {
            var expedientes = await _context.Alumbrado
                .Include(i => i.TareaTipoAlumbrado)
                .Include(i => i.Estatus)
                .Where(w => !w.TieneExpediente)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(expedientes);
        }

        [HttpGet("{AlumbradoId}")]
        public async Task<ActionResult> GetAlumbradoById(int AlumbradoId)
        {
            var alumbrado = await _context.Alumbrado
                .Include(i => i.TareaTipoAlumbrado)
                .Include(i => i.Estatus)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.AlumbradoId == AlumbradoId);
            if (alumbrado == null)
            {
                return NotFound();
            }
            return Ok(alumbrado);
        }

        [HttpPost]
        public async Task<ActionResult<Alumbrado>> PostAlumbrado(Alumbrado alumbrado)
        {
            var obra = alumbrado.NombreObra.ToString();
            var host = "ftp://digital.atogobmx.com/Files/ServiciosPublicos/AlumbradoPublico/";
            WebRequest request = WebRequest.Create(host + obra);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                request.Abort();
                resp.Close();
            }
            _context.Alumbrado.Add(alumbrado);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAlumbradoById", new { AlumbradoId = alumbrado.AlumbradoId }, alumbrado);
        }

        [HttpPut("{AlumbradoId}")]
        public async Task<ActionResult> PutAlumbrado(int AlumbradoId, Alumbrado alumbrado)
        {
            if (alumbrado.AlumbradoId != AlumbradoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var alum = _context.Alumbrado.Find(AlumbradoId);
            if (alum == null)
            {
                return BadRequest("El alumbrado no existe");
            }

            alum.AlumbradoId = alumbrado.AlumbradoId;
            alum.DescripcionProblema = alumbrado.DescripcionProblema;
            alum.NombreObra = alumbrado.NombreObra;
            alum.Domicilio = alumbrado.Domicilio;
            alum.DescripcionDomicilio = alumbrado.DescripcionDomicilio;
            alum.EstatusId = alumbrado.EstatusId;
            alum.TareaTipoId = alumbrado.TareaTipoId;
            alum.Archivado = alumbrado.Archivado;




            _context.Alumbrado.Update(alum);
            await _context.SaveChangesAsync();
            return Ok("Alumbrado actualizada correctamente");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAlumbrado(int Id)
        {
            var alumbra = _context.Alumbrado
                .FirstOrDefault(f => f.AlumbradoId == Id);
            if (alumbra == null)
            {
                return NotFound();
            }
            var expediente = await _context.ExpedienteAlumbrado
                .FirstOrDefaultAsync(f => f.AlumbradoId == Id);
            if (expediente == null)
            {
                alumbra.TieneExpediente = false;
                alumbra.Archivado = true;
                _context.Alumbrado.Update(alumbra);
                await _context.SaveChangesAsync();
                return Ok("No existe el expediente de la tarea, se archivo la tarea");
            }
            alumbra.TieneExpediente = false;
            alumbra.Archivado = true;
            expediente.Archivado = true;
            _context.Alumbrado.Update(alumbra);
            await _context.SaveChangesAsync();
            return Ok("Alumbrado archivado");
        }
    }
}


//.OrderBy(o => o.Tarea)
//alum.Tarea = alumbrado.Tarea;
//alum.DescripcionSolucion = alumbrado.DescripcionSolucion;
//alum.FechaAlta = alumbrado.FechaAlta;
//alum.FechaBaja = alumbrado.FechaBaja;