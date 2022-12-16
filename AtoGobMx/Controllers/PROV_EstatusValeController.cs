using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PROV_EstatusValeController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public PROV_EstatusValeController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PROV_EstatusVale>> GetEstatusVale()
        {
            var estatusvale = await _context.PROV_EstatusVale
                .OrderBy(o => o.EstatusValeId)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<PROV_EstatusVale>(s))
                .ToArrayAsync();
            return Ok(estatusvale);
        }

        [HttpGet("{EstatusValeId}")]
        public async Task<ActionResult> GetEstatusValeById(int EstatusValeId)
        {
            var estatusvale = await _context.PROV_EstatusVale
                .FirstOrDefaultAsync(f => f.EstatusValeId == EstatusValeId);
            if (estatusvale == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(estatusvale);
        }

        [HttpPost]
        public async Task<ActionResult<PROV_EstatusVale>> PostEstatusVale(PROV_EstatusVale estatusVale)
        {
            object value = _context.PROV_EstatusVale.Add(estatusVale);
            await _context.SaveChangesAsync();
            return Ok("Estatus vale creado correctamente");
        }

        [HttpPut("{EstatusValeId}")]
        public async Task<ActionResult> PutEstatusVale(int EstatusValeId, PROV_EstatusVale estatusVale)
        {
            if (estatusVale.EstatusValeId != EstatusValeId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var estatusvale = await _context.PROV_EstatusVale.FirstOrDefaultAsync(f => f.EstatusValeId == EstatusValeId);
            if (estatusvale == null)
            {
                return BadRequest("El Registro del estatus del vale no existe");
            }

            estatusvale.EstatusValeId = EstatusValeId;
            estatusvale.Nombre = estatusVale.Nombre;
            estatusvale.Descripcion = estatusVale.Descripcion;
            estatusvale.Archivado = estatusVale.Archivado;

            _context.PROV_EstatusVale.Update(estatusvale);
            await _context.SaveChangesAsync();
            return Ok("Estatus vale actualizado correctamente");
        }

        [HttpDelete("{EstatusValeId}")]
        public async Task<IActionResult> DeleteEstatusVale(int EstatusValeId)
        {
            var estatusvale = _context.PROV_EstatusVale
                .FirstOrDefault(f => f.EstatusValeId == EstatusValeId);
            if (estatusvale == null)
            {
                return NotFound();
            }

            estatusvale.Archivado = true;
            _context.PROV_EstatusVale.Update(estatusvale);
            await _context.SaveChangesAsync();
            return Ok("Estatus Vale Archivado");
        }
    }
}
