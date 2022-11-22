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
    public class OP_EstatusObrasController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public OP_EstatusObrasController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<OP_EstatusObras>> GetEstatusObras()
        {
            var estatusobra = await _context.OP_EstatusObras
                .OrderBy(o => o.EstatusObraId)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<OP_EstatusObras>(s))
                .ToArrayAsync();
            return Ok(estatusobra);
        }

        [HttpGet("{EstatusObraId}")]
        public async Task<ActionResult> GetEstatusObrasById(int EstatusObraId)
        {
            var estatusobra = await _context.OP_EstatusObras
                .FirstOrDefaultAsync(f => f.EstatusObraId == EstatusObraId);
            if (estatusobra == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(estatusobra);
        }

        [HttpPost]
        public async Task<ActionResult<OP_EstatusObras>> PostEstatusObras(OP_EstatusObras op_Estatusobras)
        {
            object value = _context.OP_EstatusObras.Add(op_Estatusobras);
            await _context.SaveChangesAsync();
            return Ok("Estaus obras publicas creado correctamente");
        }

        [HttpPut("{EstusObraId}")]
        public async Task<ActionResult> PutEstatusObras(int EstatusObraId, OP_EstatusObras op_EstatusObras)
        {
            if (op_EstatusObras.EstatusObraId != EstatusObraId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var estatusobra = await _context.OP_EstatusObras.FirstOrDefaultAsync(f => f.EstatusObraId == EstatusObraId);
            if (estatusobra == null)
            {
                return BadRequest("El Registro del estatus de la obra no existe");
            }

            estatusobra.EstatusObraId = EstatusObraId;
            estatusobra.Nombre = op_EstatusObras.Nombre;
            estatusobra.Descripcion = op_EstatusObras.Descripcion;
            estatusobra.Archivado = op_EstatusObras.Archivado;

            _context.OP_EstatusObras.Update(estatusobra);
            await _context.SaveChangesAsync();
            return Ok("Estatus de obras actualizado correctamente");
        }

        [HttpDelete("{EstatusObraId}")]
        public async Task<IActionResult> DeleteEstatusObras(int EstatusObraId)
        {
            var estatusobra = _context.OP_EstatusObras
                .FirstOrDefault(f => f.EstatusObraId == EstatusObraId);
            if (estatusobra == null)
            {
                return NotFound();
            }

            estatusobra.Archivado = true;
            _context.OP_EstatusObras.Update(estatusobra);
            await _context.SaveChangesAsync();
            return Ok("Estatus obras Archivado");
        }
    }
}
