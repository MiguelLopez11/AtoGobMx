using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
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
                .Include(i => i.InventarioEstatus)
                .Include(i => i.Area)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Equipos);
        }

        [HttpGet("{EquipoComputoId}")]
        public async Task<ActionResult<PAT_EquipoComputo>> GetEquipoById(int EquipoComputoId)
        {
            var equipo = await _context.EquipoComputo
                .Include(i => i.InventarioEstatus)
                .Include(i => i.Area)
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
            //var Monitor = new PAT_Monitor()
            //{
            //    MonitorId = 0,
            //    Marca = null,
            //    Pulgadas = null,
            //    EquipoComputoId = equipoComputo.EquipoComputoId,
            //    Archivado = false
            //};
            //var Teclado = new PAT_Teclado()
            //{
            //    TecladoId = 0,
            //    Marca = null,
            //    TipoConexion = null,
            //    EquipoComputoId = equipoComputo.EquipoComputoId,
            //    Archivado = false
            //};
            //var Mouse = new PAT_Mouse()
            //{
            //    MouseId = 0,
            //    Marca = null,
            //    TipoConexion = null,
            //    EquipoComputoId = equipoComputo.EquipoComputoId,
            //    Archivado = false
            //};
            //_context.PAT_Monitor.Add(Monitor);
            //_context.PAT_Teclado.Add(Teclado);
            //_context.PAT_Mouse.Add(Mouse);
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
            equipo.Marca = equipoComputo.Marca;
            equipo.MemoriaRAM = equipoComputo.MemoriaRAM;
            equipo.Almacenamiento = equipoComputo.Almacenamiento;
            equipo.Procesador = equipoComputo.Procesador;
            equipo.EstatusId = equipoComputo.EstatusId;
            equipo.AreaId = equipoComputo.AreaId;
            equipo.Archivado = equipoComputo.Archivado;

            _context.EquipoComputo.Update(equipo);
            await _context.SaveChangesAsync();
            return Ok("Equipo de computo actualizado correctamente");
        }
        [HttpDelete("{EquipoComputoId}")]
        public async Task<IActionResult> DeleteEquipoComputo(int EquipoComputoId)
        {
            var equipo = await _context.EquipoComputo
                .FirstOrDefaultAsync(f => f.EquipoComputoId == EquipoComputoId);
            if (equipo == null)
            {
                return NotFound();
            }
            equipo.Archivado = true;
            _context.EquipoComputo.Update(equipo);
            await _context.SaveChangesAsync();
            return Ok("Producto archivado");
        }
    }
}
