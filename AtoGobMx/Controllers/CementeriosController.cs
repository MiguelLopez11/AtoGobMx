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
    public class CementeriosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public CementeriosController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Cementerios>> GetCementerios()
        {
            var cementerios = await _context.Cementerios
                .OrderBy(o => o.NombrePropietario)
                .Where(w => w.Archivado)
                .Select(s => _mapper.Map<Cementerios>(s))
                .ToArrayAsync();
            return Ok(cementerios);
        }

        [HttpGet("{CementeriosId}")]
        public async Task<ActionResult> GetCementeriosById(int CemeneteriosId)
        {
            var cementerio = await _context.Cementerios
                .FirstOrDefaultAsync(f => f.CementeriosId == CemeneteriosId);
            if (cementerio == null)
            {
                return NotFound();
            }
            return Ok(cementerio);
        }

        [HttpPost]
        public async Task<ActionResult<Cementerios>> PostCementerios(Cementerios cementerios)
        {
            Object value = _context.Cementerios.Add(cementerios);
            await _context.SaveChangesAsync();
            return Ok("Registro cementerio creado correctamente");
        }

        [HttpPut("{CementeriosId}")]
        public async Task<ActionResult> PutCementerios(int CementeriosId, Cementerios cementerios)
        {
            if (cementerios.CementeriosId != CementeriosId)
            {
                return Ok("Los ID ingresados no coinciden");
            }

            var cementeri = _context.Cementerios.Find(CementeriosId);
            if (cementeri == null)
            {
                return BadRequest("Elregistro de cementerios no existe");
            }

            cementeri.CementeriosId = CementeriosId;
            cementeri.NombrePropietario = cementerios.NombrePropietario;
            cementeri.NumeroEspasios = cementerios.NumeroEspasios;
            cementeri.MetrosCorrespondientes = cementerios.MetrosCorrespondientes;
            cementeri.EspaciosDisponibles = cementerios.EspaciosDisponibles;

            _context.Cementerios.Update(cementeri);
            await _context.SaveChangesAsync();
            return Ok("Tipo de cementerio actualizado correctamente");
            
        }

        [HttpDelete("{CementeriosId}")]
        public async Task<ActionResult> DeleteCementerios(int CementeriosId)
        {
            var cementeri = _context.Cementerios.FirstOrDefault(f => f.CementeriosId == CementeriosId);

            if (cementeri == null)
            {
                return NotFound();
            }

            cementeri.Archivado = true;
            _context.Cementerios.Update(cementeri);
            await _context.SaveChangesAsync();
            return Ok("El sementerio se archivo con exito");
        }
    }
}
