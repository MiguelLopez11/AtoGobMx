using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public EmpleadosController(AtoGobMxContext Context, IMapper mapper)
        {
            _context = Context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Empleado>> GetEmpleados()
        {
            var empleados = await _context.Empleados
                //.Include(i => i.Area)
                .Include(i => i.Departamentos)
                .Include(i => i.PuestoTrabajo)
                .OrderBy(o => o.EmpleadoId)
                .ToListAsync();
            return Ok(empleados);
        }
        [HttpGet("SinArchivar")]
        public async Task<ActionResult<Empleado>> GetEmpleadosSinArchivar()
        {
            var empleados = await _context.Empleados
                //.Include(i => i.Area)
                .Include(i => i.Departamentos)
                .Include(i => i.PuestoTrabajo)
                //.Include(i => i.usuario)
                .Where(w => !w.Archivado)
                .OrderBy(o => o.EmpleadoId)
                .ToListAsync();
            return Ok(empleados);
        }
        [HttpGet("SinExpedientes")]
        public async Task<ActionResult<Empleado>> GetEmpleadosSinExpedientes()
        {
            var empleados = await _context.Empleados
                //.Include(i => i.Area)
                .Include(i => i.Departamentos)
                .Include(i => i.PuestoTrabajo)
                .Where(w => w.TieneExpediente == false)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(empleados);
        }
        [HttpGet("SinExpedienteMedico")]
        public async Task<ActionResult<Empleado>> GetEmpleadosSinExpedienteMédico()
        {
            var empleados = await _context.Empleados
                //.Include(i => i.Area)
                .Include(i => i.Departamentos)
                .Include(i => i.PuestoTrabajo)
                .Where(w => w.TieneExpedienteMédico == false)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(empleados);
        }

        [HttpGet("{EmpleadoId}")]
        public async Task<ActionResult> GetEmpleadosById(int EmpleadoId)
        {
            var Empleado = await _context.Empleados
                //.Include(i => i.Area)
                .Include(i => i.Departamentos)
                .FirstOrDefaultAsync(f => f.EmpleadoId == EmpleadoId);
            if (Empleado == null)
            {
                return NotFound();
            }
            return Ok(Empleado);
        }
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleados(Empleado Empleado)
        {
            var employeeName = Empleado.NombreCompleto.ToString();
            var host = "ftp://digital.atogobmx.com/Files/RecursosHumanos/Empleados/";
            #region Create directory employee
            WebRequest request = WebRequest.Create(host + employeeName);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("atogobmxdigital@digital.atogobmx.com", "LosAhijados22@");
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                request.Abort();
                resp.Close();
            }
            #endregion
            var documentPath = CreateDocument(host + employeeName);
            Empleado.FechaAlta = DateTime.Today;
            _context.Empleados.Add(Empleado);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEmpleadosById", new { EmpleadoId = Empleado.EmpleadoId }, Empleado);
     
        }
        [HttpPut("{EmpleadoId}")]
        public async Task<ActionResult> PutEmpleado(int EmpleadoId, Empleado empleado)
        {
            if (empleado.EmpleadoId != EmpleadoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var emp = await _context.Empleados.FirstOrDefaultAsync(f => f.EmpleadoId == EmpleadoId);
            if (emp == null)
            {
                return BadRequest("El empledo no existe");
            }
            emp.EmpleadoId = empleado.EmpleadoId;
            emp.NombreCompleto = empleado.NombreCompleto;
            emp.FechaAlta = empleado.FechaAlta;
            emp.FechaBaja = empleado.FechaBaja;
            //emp.AreaId = empleado.AreaId;
            emp.DepartamentoId = empleado.DepartamentoId;
            emp.PuestoTrabajoId = empleado.PuestoTrabajoId;
            emp.Archivado = empleado.Archivado;

            _context.Empleados.Update(emp);
            await _context.SaveChangesAsync();
            return Ok("Empleado actualizado correctamente");
        }
        [HttpDelete("{empleadoId}")]
        public async Task<IActionResult> DeleteEmpleados(int empleadoId)
        {
            try
            {

                var empleado = _context.Empleados
                    .FirstOrDefault(f => f.EmpleadoId == empleadoId);
                if (empleado == null)
                {
                    return NotFound();
                }
                empleado.Archivado = true;
                empleado.TieneExpediente = false;
                empleado.FechaBaja = DateTime.Today;

                var expediente = await _context.ExpedienteDigital
                    .FirstOrDefaultAsync(f => f.EmpleadoId == empleadoId);

                expediente.Archivado = true;
                var expedienteMedico = await _context.ExpedienteMedico
                    .FirstOrDefaultAsync(f => f.EmpleadoId == empleadoId);

                expedienteMedico.Archivado = true;
                _context.ExpedienteMedico.Update(expedienteMedico);
                _context.ExpedienteDigital.Update(expediente);
                _context.Empleados.Update(empleado);
                await _context.SaveChangesAsync();
                return Ok("Empleado archivado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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
                return false;
            }
        }
    }
}
