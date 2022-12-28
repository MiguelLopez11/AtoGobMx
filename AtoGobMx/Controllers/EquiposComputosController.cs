using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposComputosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public EquiposComputosController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PAT_EquipoComputo>>> GetEquipos()
        {
            var Equipos = await _context.EquipoComputo
                .Include(i => i.EstatusEquipo)
                //.Include(i => i.Area)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Equipos);
        }

        [HttpGet("{EquipoComputoId}")]
        public async Task<ActionResult<PAT_EquipoComputo>> GetEquipoById(int EquipoComputoId)
        {
            var equipo = await _context.EquipoComputo
                .Include(i => i.EstatusEquipo)
                //.Include(i => i.Area)
                .FirstOrDefaultAsync(f => f.EquipoComputoId == EquipoComputoId);

            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_EquipoComputo>> PostEquipoComputo(PAT_EquipoComputo equipoComputo)
        {
            _context.EquipoComputo.Add(equipoComputo);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{EquipoComputoId}")]
        public async Task<IActionResult> PutEquipoComputo(int EquipoComputoId, PAT_EquipoComputo equipoComputo)
        {
            if (equipoComputo.EquipoComputoId != EquipoComputoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var equipo = await _context.EquipoComputo.FindAsync(EquipoComputoId);
            if (equipo == null)
            {
                return NotFound();
            }
            equipo.EquipoComputoId = equipoComputo.EquipoComputoId;
            equipo.CodigoInventario = equipoComputo.CodigoInventario;
            equipo.Caracteristicas = equipoComputo.Caracteristicas;
            equipo.fechaAdquisicion = equipoComputo.fechaAdquisicion;
            equipo.Costo = equipoComputo.Costo;
            equipo.NumeroSerie = equipoComputo.NumeroSerie;
            equipo.EstatusEquipoId = equipoComputo.EstatusEquipoId;
            //equipo.AreaId = equipoComputo.AreaId;
            equipo.Archivado = equipoComputo.Archivado;

            _context.EquipoComputo.Update(equipo);
            await _context.SaveChangesAsync();
            return Ok("Equipo de computo actualizado correctamente");
        }
        [HttpDelete("{EquipoComputoId}")]
        public async Task<IActionResult> DeleteEquipoComputo(int EquipoComputoId)
        {
            try
            {
                var equipo = await _context.EquipoComputo
                .FirstOrDefaultAsync(f => f.EquipoComputoId == EquipoComputoId);
                if (equipo == null)
                {
                    return NotFound();
                }
                var estatus = await _context.InventarioEstatus
                    .FirstOrDefaultAsync(f => f.Nombre.Equals("Dado de baja"));
                equipo.EstatusEquipoId = estatus.EstatusEquipoId;
                _context.EquipoComputo.Update(equipo);
                await _context.SaveChangesAsync();
                return Ok("Producto archivado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
