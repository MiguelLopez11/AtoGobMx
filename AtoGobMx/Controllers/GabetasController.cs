using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gabetas = AtoGobMx.Models.Gabetas;

namespace AtoGobMx.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GabetasController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public GabetasController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Gabetas>> GetGabetas()
        {
            var Gabetas = await _context.Gabetas
                .Include(i => i.Cementerio)
                //.OrderBy(o => o.NombrePropietario)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<Gabetas>(s))
                .ToArrayAsync();
            return Ok(Gabetas);
        }

        [HttpGet("{GabetaId}")]
        public async Task<ActionResult> GetGabetasById(int GabetaId)
        {
            var gabeta = await _context.Gabetas
            .Include(i => i.Cementerio)
            //.Include(i => i.Estatus)
                .FirstOrDefaultAsync(f => f.GabetaId == GabetaId);
            if (gabeta == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(gabeta);
        }

        [HttpPost]
        public async Task<ActionResult<Gabetas>> PostGabeta(Gabetas gabetas)
        {
            Object value = _context.Gabetas.Add(gabetas);
            await _context.SaveChangesAsync();
            return Ok("Registro gabeta creado correctamente");
        }

        [HttpPut("{GabetaId}")]
        public async Task<ActionResult> PutGabetas(int GabetaId, Gabetas gabetas)
        {
            if (gabetas.GabetaId != GabetaId)
            {
                return Ok("Los ID ingresados no coinciden");
            }

            var gabeta = _context.Gabetas.Find(GabetaId);
            if (gabeta == null)
            {
                return BadRequest("Elregistro de gabeta no existe");
            }

            gabeta.GabetaId = GabetaId;
            gabeta.NombrePropietario = gabetas.NombrePropietario;
            gabeta.NumeroEspasios = gabetas.NumeroEspasios;
            gabeta.MetrosCorrespondientes = gabetas.MetrosCorrespondientes;
            gabeta.CementerioId = gabetas.CementerioId;
            gabeta.Latitud = gabetas.Latitud;
            gabeta.Longitud = gabetas.Longitud;
            gabeta.EspaciosDisponibles = gabetas.EspaciosDisponibles;

            _context.Gabetas.Update(gabeta);
            await _context.SaveChangesAsync();
            return Ok("Tipo de gabeta actualizado correctamente");

        }

        [HttpDelete("{GabetaId}")]
        public async Task<ActionResult> DeleteGabetaId(int GabetaId)
        {
            var gabetas = _context.Gabetas.FirstOrDefault(f => f.GabetaId == GabetaId);

            if (gabetas == null)
            {
                return NotFound();
            }
            //var expediente = await _context.ExpedienteAlumbrado
            //    .FirstOrDefaultAsync(f => f.AlumbradoId == Id);
            //if (expediente == null)
            //{
            //    cementeri.Archivado = true;
            //    _context.Cementerios.Update(cementeri);
            //    await _context.SaveChangesAsync();
            //    return Ok("No existe el expediente de la tarea, se archivo la tarea");
            //}
            //expediente.Archivado = true;
            gabetas.Archivado = true;
            _context.Gabetas.Update(gabetas);
            await _context.SaveChangesAsync();
            return Ok("La gabeta se archivo con exito");
        }
    }
}
