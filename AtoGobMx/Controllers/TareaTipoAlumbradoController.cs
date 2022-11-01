using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaTipoAlumbradoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public TareaTipoAlumbradoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TareaTipoAlumbrado>> GetTareaTipoAlumbrado()
        {
            var tareatipo = await _context.TareaTipoAlumbrado
                .Where(w => !w.Archivado)
                .OrderBy(o => o.TareaTipoId)
                .ToListAsync();
            return Ok(tareatipo);
        }

        [HttpGet("{TareaTipoId}")]
        public async Task<ActionResult> GetTareaTipoAlumbradoById(int TareaTipoId)
        {
            var tareaTipo = await _context.TareaTipoAlumbrado
                .FirstOrDefaultAsync(f => f.TareaTipoId == TareaTipoId);
            if (tareaTipo == null)
            {
                return NotFound();
            }
            return Ok(tareaTipo);
        }

        [HttpPost]
        public async Task<ActionResult<TareaTipoAlumbrado>> PostTareaTipoAlumbrado(TareaTipoAlumbrado tareaTipoAlumbrado)
        {
            _context.TareaTipoAlumbrado.Add(tareaTipoAlumbrado);
            await _context.SaveChangesAsync();
            return Ok("Tipo tarea creada correctamente");
        }

        [HttpPut("{TareaTipoId}")]
        public async Task<ActionResult> PutTareaTipoAlumbrado(int TareaTipoId, TareaTipoAlumbrado tareaTipoAlumbrado)
        {
            if (tareaTipoAlumbrado.TareaTipoId != TareaTipoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var tareatipo = _context.TareaTipoAlumbrado.Find(TareaTipoId);
            if (tareatipo == null)
            {
                return BadRequest("El Registro del tipo de tarea alumbrado no existe");
            }

            tareatipo.TareaTipoId = TareaTipoId;
            tareatipo.NombreTarea = tareaTipoAlumbrado.NombreTarea;
            tareatipo.Descripcion = tareaTipoAlumbrado.Descripcion;
            tareatipo.Archivado = tareaTipoAlumbrado.Archivado;

            _context.TareaTipoAlumbrado.Update(tareatipo);
            await _context.SaveChangesAsync();
            return Ok("Tipo de tarea alumbrado actualizado correctamente");
        }
        [HttpDelete("{TareaTipoId}")]
        public async Task<ActionResult> DeleteTareaTipoAlumbrado(int TareaTipoId)
        {
            var tareatipo = _context.TareaTipoAlumbrado
                .FirstOrDefault(f => f.TareaTipoId == TareaTipoId);
            if (tareatipo == null)
            {
                return NotFound();
            }

            tareatipo.Archivado = true;
            _context.TareaTipoAlumbrado.Update(tareatipo);
            await _context.SaveChangesAsync();
            return Ok("Tipo de tarea alumbrado Archivado");
        }
    }
}
