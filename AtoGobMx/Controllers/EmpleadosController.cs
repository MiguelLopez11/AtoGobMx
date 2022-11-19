using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                .Include(i => i.Area)
                .Include(i => i.Departamentos)
                .Include(i => i.PuestoTrabaj
                .OrderBy(o => o.EmpleadoId)
                .ToListAsync();
            return Ok(empleados);
        }
        [HttpGet("SinArchivar")]
        public async Task<ActionResult<Empleado>> GetEmpleadosSinArchivar()
        {
            var empleados = await _context.Empleados
                .Include(i => i.Area)
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
                .Include(i => i.Area)
                .Include(i => i.Departamentos)
                .Include(i => i.PuestoTrabajo)
                .Where(w => w.TieneExpediente == false)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(empleados);
        }
        //[HttpGet("AlumbradoPublico/{ExpedienteAlumbradoPublicoId}")]
        //public async Task<ActionResult<Empleado>> GetEmpleadosAlumbradoPublico(int ExpedienteAlumbradoPublicoId)
        //{
        //    List<Empleado> empleados = new List<Empleado>();
        //    //Empleados Registrados en alumbrado 
        //    var empleadosAlumbrado = await _context.EmpleadosAlumbrado
        //        .Where(w => !w.Archivado)
        //        .Where(w => w.ExpedienteAlumbradoId == ExpedienteAlumbradoPublicoId)
        //        .ToListAsync();
        //    //todos los empleados
        //    var emp = await _context.Empleados
        //            .Include(i => i.Area)
        //            .Include(i => i.Departamentos)
        //            .Include(i => i.PuestoTrabajo)
        //            .Where(w => !w.Archivado)
        //            .ToListAsync();
            //si esta vacio, retorna todos los empleados
            //for (int i = 0; i <= emp.Count; i++)
            //{
            //    for (int j =0; j <= empleadosAlumbrado.Count; j++)
            //    {
            //        if (emp[i].EmpleadoId != empleadosAlumbrado[j].EmpleadoId)
            //        {
            //            empleados.Add(emp[i]);
            //        }
            //    }
            //}
                //return Ok(empleados);
            
        //}

        [HttpGet("{EmpleadoId}")]
        public async Task<ActionResult> GetEmpleadosById(int EmpleadoId)
        {
            var Empleado = await _context.Empleados
                .Include(i => i.Area)
                .Include(i => i.Departamentos)
                .FirstOrDefaultAsync(f => f.EmpleadoId == EmpleadoId);
            if (Empleado == null)
            {
                return NotFound();
            }
            return Ok(Empleado);
        }
        //[HttpGet("EmpleadoExpedientes")]
        //public async Task<ActionResult> GetEmpleadoExpedientes()
        //{
        //    var Empleado = await _context.Empleados
        //        .Include(i => i.Area)
        //        .Include(i => i.Departamentos)
        //        .Where(w => w.expedienteDigitalId != null)
        //        .ToListAsync();
        //    if (Empleado == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(Empleado);
        //}
        //[HttpGet("EmpleadosSinExpedientes")]
        //public async Task<ActionResult> GetEmpleadosSinExpediente()
        //{
        //    var Empleado = await _context.Empleados
        //        .Include(i => i.Area)
        //        .Where(w => w.expedienteDigitalId == null)
        //        .ToListAsync();
        //    if (Empleado == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(Empleado);
        //}
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleados(Empleado Empleado)
        {
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
            emp.AreaId = empleado.AreaId;
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
                _context.Empleados.Update(empleado);
                await _context.SaveChangesAsync();
                return Ok("Empleado archivado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
