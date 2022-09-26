using AtoGobMx.Context;
using AtoGobMx.Migrations;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteFallaController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public ExpedienteFallaController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ExpedienteFallas>> GetExpedienteFalla()
        {
            var expefalla = await _context.ExpedienteFalla
                .Where(w => !w.Archivado)
                .OrderBy(o => o.ExpedienteFallaId)
                .ToListAsync();
            return Ok(expefalla);
        }

        [HttpGet("ExpedienteFallaId")]
        public async Task<ActionResult> GetExpedienteFallaById(int ExpedienteFallaId)
        {
            var expedienteFalla = await _context.ExpedienteFalla
                .Include(i => i.FallasAlumbradoPublico)
                .FirstOrDefaultAsync(f => f.ExpedienteFallaId == ExpedienteFallaId);
            if (expedienteFalla == null)
            {
                return NotFound();
            }
            return Ok(expedienteFalla);
        }

        [HttpPost("{FallaId}")]
        public async Task<ActionResult<ExpedienteFalla>> PostExpedienteFalla(ExpedienteFallas expedienteFalla, int FallaId)
        {
            var alumbrado = await _context.FallasAlumbradoPublico.FirstOrDefaultAsync(f => f.FallaId == FallaId);
            _context.ExpedienteFalla.Add(expedienteFalla);
            var expedientefa = CreatedAtAction("GetExpedienteFallaById", new { ExpedienteFallaId = expedienteFalla.ExpedienteFallaId }, expedienteFalla);
            alumbrado.expedienteFallaId = expedienteFalla.ExpedienteFallaId;
            await _context.SaveChangesAsync();
            return Ok(expedientefa);

        }

        [HttpPut("{ExpedienteFallaId}")]
        public async Task<ActionResult> PutExpedientFalla(int ExpedienteFallaId, ExpedienteFallas expedienteFalla)
        {
            if (expedienteFalla.ExpedienteFallaId != ExpedienteFallaId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var expedientfalla = _context.ExpedienteFalla.Find(ExpedienteFallaId);
            if (expedientfalla == null)
            {
                return BadRequest("El Registro de alumbrado no existe");
            }

            expedientfalla.ExpedienteFallaId = expedienteFalla.ExpedienteFallaId;
            expedientfalla.TipoFalla = expedienteFalla.TipoFalla;
            expedientfalla.TipoOrden = expedienteFalla.TipoOrden;
            expedientfalla.Domicilio = expedienteFalla.Domicilio;
            expedientfalla.MarcaIluminaria = expedienteFalla.MarcaIluminaria;
            expedientfalla.Archivado = expedienteFalla.Archivado;

            _context.ExpedienteFalla.Update(expedientfalla);
            await _context.SaveChangesAsync();
            return Ok("Expediente actualizado correctamente");
        }

        [HttpDelete("{ExpedienteFallaId}")]
        public async Task<IActionResult> DeleteExpedienteFalla(int ExpedienteFallaId)
        {
            var expedientfallas = _context.ExpedienteFalla
                .FirstOrDefault(f => f.ExpedienteFallaId == ExpedienteFallaId);
            if (expedientfallas == null)
            {
                return NotFound();
            }

            expedientfallas.Archivado = true;
            _context.ExpedienteFalla.Update(expedientfallas);
            await _context.SaveChangesAsync();
            return Ok("ExpedienteFalla Archivado");
        }

    }
}
