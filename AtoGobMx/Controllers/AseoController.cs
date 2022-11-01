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
    public class AseoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public AseoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Aseo>> GetAseo()
        {
            var ase = await _context.Aseo
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<Aseo>(s))
                .ToArrayAsync();
            return Ok(ase);
        }

        [HttpGet("{AseoId}")]
        public async Task<ActionResult> GetAseoById(int AseoId)
        {
            var aseo = await _context.Aseo
                //.Include(i => i.TareaTipoAlumbrado)
                //.Include(i => i.Estatus)
                .FirstOrDefaultAsync(f => f.AseoId == AseoId);
            if (aseo == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(aseo);
        }

        [HttpPost]
        public async Task<ActionResult<Aseo>> PostAseo(Aseo aseo)
        {
            object value = _context.Aseo.Add(aseo);
            await _context.SaveChangesAsync();
            return Ok("Aseo creado correctamente");
        }

        [HttpPut("{AseoId}")]
        public async Task<ActionResult> PutAseo(int AseoId, Aseo aseo)
        {
            if (aseo.AseoId != AseoId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var aseopublico = await _context.Aseo.FirstOrDefaultAsync(f => f.AseoId == AseoId);
            if (aseopublico == null)
            {
                return BadRequest("El Registro del aseo no existe");
            }

            aseopublico.AseoId = AseoId;
            aseopublico.NombreServicio = aseo.NombreServicio;
            aseopublico.EstablecimientoPublico = aseo.EstablecimientoPublico;
            aseopublico.Domicilio = aseo.Domicilio;
            aseopublico.Objetivo = aseo.Objetivo;
            aseopublico.Archivado = aseo.Archivado;

            _context.Aseo.Update(aseopublico);
            await _context.SaveChangesAsync();
            return Ok("Servicio publico aseo actualizado correctamente");
        }

        [HttpDelete("{AseoId}")]
        public async Task<IActionResult> DeleteAseo(int AseoId)
        {
            var aseopublico = _context.Aseo
                .FirstOrDefault(f => f.AseoId == AseoId);
            if (aseopublico == null)
            {
                return NotFound();
            }

            aseopublico.Archivado = true;
            _context.Aseo.Update(aseopublico);
            await _context.SaveChangesAsync();
            return Ok("Servicio publico aseo Archivado");
        }

    }
}
