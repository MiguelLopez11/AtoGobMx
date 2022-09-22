
using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FallasAlumbradoPublicoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public FallasAlumbradoPublicoController(AtoGobMxContext Context, IMapper mapper)
        {
            _context = Context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<FallasAlumbradoPublico>> GetFallasAlumbradoPublico()
        {
            var fallas = await _context.FallasAlumbradoPublico
                .OrderBy(o => o.TipoFalla)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<FallasAlumbradoPublico>(s))
                .ToArrayAsync();
            return Ok(fallas);
        }
        [HttpGet("{FallaId}")]
        public async Task<ActionResult> GetFallasAlumbradoPublicoById(int FallaId)
        {
            var Falla = await _context.FallasAlumbradoPublico
                .FirstOrDefaultAsync(f => f.FallaId == FallaId);
            if (Falla == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(Falla);
        }
        [HttpPost]
        public async Task<IActionResult> PostFallasAlumbradoPublico(FallasAlumbradoPublico Falla)
        {
            _context.FallasAlumbradoPublico.Add(Falla);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{FallaId}")]
        public async Task<ActionResult> PutFallasAlumbradoPublico(int FallaId, FallasAlumbradoPublico fallas)
        {
            if (fallas.FallaId != FallaId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var fall = _context.FallasAlumbradoPublico.Find(FallaId);
            if (fall == null)
            {
                return BadRequest("La falla no existe");
            }
            fall.FallaId = fallas.FallaId;
            fall.TipoFalla = fallas.TipoFalla;
            fall.DescripcionSolucion = fallas.DescripcionSolucion;
            fall.FechaAlta = fallas.FechaAlta;
            fall.FechaBaja = fallas.FechaBaja;
            fall.Domicilio = fallas.Domicilio;
            fall.DescripcionDomicilio = fallas.DescripcionDomicilio;
            //emp.RFC = empleado.RFC;
            //emp.CURP = empleado.CURP;
            //emp.Direccion = empleado.Direccion;
            fall.Archivado = fallas.Archivado;
            //emp.FechaAlta = empleado.FechaAlta;

            _context.FallasAlumbradoPublico.Update(fall);
            await _context.SaveChangesAsync();
            return Ok("Falla actualizada correctamente");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFallasAlumbradoPublico(int Id)
        {
            var fallas = _context.FallasAlumbradoPublico
                .FirstOrDefault(f => f.FallaId == Id);
            if (fallas == null)
            {
                return NotFound();
            }
            fallas.Archivado = true;
            _context.FallasAlumbradoPublico.Update(fallas);
            await _context.SaveChangesAsync();
            return Ok("Fallas archivadas");
        }
    }
}
