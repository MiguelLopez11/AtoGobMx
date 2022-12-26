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
    public class ObrasPublicasController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public ObrasPublicasController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<OP_Obras>> GetObrasPublicas()
        {
            var obraspublicas = await _context.ObrasPublicas
                .Include(i => i.OP_EstatusObras)
                .OrderBy(o => o.ObraId)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<OP_Obras>(s))
                .ToArrayAsync();
            return Ok(obraspublicas);
        }

        [HttpGet("{ObraId}")]
        public async Task<ActionResult> GetObrasPublicasById(int ObraId)
        {
            var obraspublicas = await _context.ObrasPublicas
                .Include(i => i.OP_EstatusObras)
                .FirstOrDefaultAsync(f => f.ObraId == ObraId);
            if (obraspublicas == null)
            {
                return NotFound();
            }
            return Ok(obraspublicas);
        }

        [HttpPost]
        public async Task<ActionResult<OP_Obras>> PostObrasPublicas(OP_Obras op_ObrasPublicas)
        {
            object value = _context.ObrasPublicas.Add(op_ObrasPublicas);
            await _context.SaveChangesAsync();
            return Ok("Tipo de obra creada correctamente");
        }

        [HttpPut("{ObraId}")]
        public async Task<ActionResult> PutObrasPublicas(int ObraId, OP_Obras obrasPublicas)
        {
            if (obrasPublicas.ObraId != ObraId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var obrapublic = await _context.ObrasPublicas.FirstOrDefaultAsync(f => f.ObraId == ObraId);
            if (obrapublic == null)
            {
                return BadRequest("El Registro de la obra publica no existe");
            }

            obrapublic.ObraId = ObraId;
            obrapublic.Nombre = obrasPublicas.Nombre;
            obrapublic.Latitud = obrasPublicas.Latitud;
            obrapublic.Longitud = obrasPublicas.Longitud;
            obrapublic.Descripcion = obrasPublicas.Descripcion;
            obrapublic.EstatusObraId = obrasPublicas.EstatusObraId;
            obrapublic.Archivado = obrasPublicas.Archivado;

            _context.ObrasPublicas.Update(obrapublic);
            await _context.SaveChangesAsync();
            return Ok("Obras publicas actualizado correctamente");
        }

        [HttpDelete("{ObraId}")]
        public async Task<IActionResult> DeleteObrasPublicas(int ObraId)
        {
            var obraspublicas = _context.ObrasPublicas
                .FirstOrDefault(f => f.ObraId == ObraId);
            if (obraspublicas == null)
            {
                return NotFound();
            }

            obraspublicas.Archivado = true;
            _context.ObrasPublicas.Update(obraspublicas);
            await _context.SaveChangesAsync();
            return Ok("Estatus Archivado");
        }
    }
}
