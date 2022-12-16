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
    public class PAT_MonitorController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public PAT_MonitorController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PAT_Monitor>>> GetMonitores()
        {
            var monitores = await _context.Monitor
                .Include(i => i.EquipoComputo)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(monitores);
        }

        [HttpGet("{MonitorId}")]
        public async Task<ActionResult<PAT_Monitor>> GetMonitorById(int MonitorId)
        {
            var monitor = await _context.Monitor.FirstOrDefaultAsync(f => f.MonitorId == MonitorId);

            if (monitor == null)
            {
                return NotFound();
            }

            return Ok(monitor);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_Monitor>> PostMonitor(PAT_Monitor monitor)
        {
            _context.Monitor.Add(monitor);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{MonitorId}")]
        public async Task<IActionResult> PutMonitor(int MonitorId, PAT_Monitor monitor)
        {
            if (monitor.MonitorId != MonitorId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var PAT_Monitor = await _context.Monitor.FindAsync(MonitorId);
            if (PAT_Monitor == null)
            {
                return NotFound();
            }
            PAT_Monitor.MonitorId = monitor.MonitorId;
            PAT_Monitor.Marca = monitor.Marca;
            PAT_Monitor.Pulgadas = monitor.Pulgadas;
            PAT_Monitor.Archivado = monitor.Archivado;

            _context.Monitor.Update(PAT_Monitor);
            await _context.SaveChangesAsync();
            return Ok("Monitor actualizado correctamente");
        }
        [HttpDelete("{MonitorId}")]
        public async Task<IActionResult> DeleteMonitor(int MonitorId)
        {
            var monitor = await _context.Monitor
                .FirstOrDefaultAsync(f => f.MonitorId == MonitorId);
            if (monitor == null)
            {
                return NotFound();
            }
            monitor.Archivado = true;
            _context.Monitor.Update(monitor);
            await _context.SaveChangesAsync();
            return Ok("Producto archivado");
        }
    }
}
