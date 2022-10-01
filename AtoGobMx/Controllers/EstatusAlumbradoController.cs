using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatusAlumbradoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public EstatusAlumbradoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<EstatusAlumbrado>> GetEstatus()
        {
            var estatus = await _context.EstatusAlumbrado
                .OrderBy(o => o.NombreEstatus)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<EstatusAlumbrado>(s))
                .ToArrayAsync();
            return Ok(estatus);
        }

        [HttpGet("{EstatusId}")]
        public async Task<ActionResult> GetEstatusById(int EstatusId)
        {
            var estatus = await _context.EstatusAlumbrado
                .FirstOrDefaultAsync(f => f.EstatusId == EstatusId);
            if (estatus == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(estatus);
        }

        [HttpPost]
        public async Task<ActionResult<EstatusAlumbrado>> PostEstatus(EstatusAlumbrado estatus)
        {
            object value = _context.EstatusAlumbrado.Add(estatus);
            await _context.SaveChangesAsync();
            return Ok("Estatus creado correctamente");
        }

        [HttpPut("{EstatusId}")]
        public async Task<ActionResult> PutEstatus(int EstatusId, EstatusAlumbrado estatus)
        {
            if (estatus.EstatusId != EstatusId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var estatu = _context.EstatusAlumbrado.Find(EstatusId);
            if (estatu == null)
            {
                return BadRequest("El Registro del estatus no existe");
            }

            estatus.EstatusId = estatu.EstatusId;
            estatus.NombreEstatus = estatu.NombreEstatus;
            estatus.Descripcion = estatu.Descripcion;

            _context.EstatusAlumbrado.Update(estatus);
            await _context.SaveChangesAsync();
            return Ok("Estatus actualizado correctamente");
        }
        [HttpDelete("{EstatusId}")]
        public async Task<IActionResult> DeleteEstatus(int EstatusId)
        {
            var estatus = _context.EstatusAlumbrado
                .FirstOrDefault(f => f.EstatusId == EstatusId);
            if (estatus == null)
            {
                return NotFound();
            }

            estatus.Archivado = true;
            _context.EstatusAlumbrado.Update(estatus);
            await _context.SaveChangesAsync();
            return Ok("Estatus Archivado");
        }
    }
}
