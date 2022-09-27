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
    public class EstatusController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public EstatusController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Estatus>> GetEstatus()
        {
            var estatus = await _context.Estatus
                .OrderBy(o => o.NombreEstatus)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<Estatus>(s))
                .ToArrayAsync();
            return Ok(estatus);
        }
        [HttpGet("{EstatusId}")]
        public async Task<ActionResult> GetEstatusById(int EstatusId)
        {
            var estatus = await _context.Estatus
                .FirstOrDefaultAsync(f => f.EstatusId == EstatusId);
            if (estatus == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(estatus);
        }
        [HttpPost("{AlumbradoId}")]
        public async Task<ActionResult<Estatus>> PostEstatus(Estatus estatus, int AlumbradoId)
        {
            var estatu = await _context.Alumbrado.FirstOrDefaultAsync(f => f.AlumbradoId == AlumbradoId);
            _context.Estatus.Add(estatus);
            var Estatus= CreatedAtAction("GetEstatusById", new { EstatusId = estatus.EstatusId }, estatus);
            //estatu.estatusId = estatus.EstatusId;
            //await _context.SaveChangesAsync();
            return Ok(Estatus);
        }
        [HttpPut("{}")]
    }
}
