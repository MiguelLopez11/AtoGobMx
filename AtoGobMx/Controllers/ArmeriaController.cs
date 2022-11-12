using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmeriaController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public ArmeriaController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PAT_Armeria>>> GetArmeria()
        {
            var Estatus = await _context.Armeria
                .Include(i => i.Empleado)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Estatus);
        }

        [HttpGet("{ArmaId}")]
        public async Task<ActionResult<PAT_Armeria>> GetArmaById(int ArmaId)
        {
            var armeria = await _context.Armeria
                .Include(i => i.Empleado)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.ArmaId == ArmaId);

            if (armeria == null)
            {
                return NotFound();
            }

            return Ok(armeria);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_Armeria>> PostArmeria(PAT_Armeria armeria)
        {
            _context.Armeria.Add(armeria);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Arma registrada exitosamente");
        }
        [HttpPut("{ArmaId}")]
        public async Task<IActionResult> PutArmeria(int ArmaId, PAT_Armeria armeria)
        {
            if (armeria.ArmaId != ArmaId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var arma = await _context.Armeria.FindAsync(ArmaId);
            if (arma == null)
            {
                return NotFound();
            }
            arma.ArmaId = armeria.ArmaId;
            arma.Nomenclatura = armeria.Nomenclatura;
            arma.Marca = armeria.Marca;
            arma.TipoArma = armeria.TipoArma;
            arma.Calibre = armeria.Calibre;
            arma.EmpleadoId = armeria.EmpleadoId;
            arma.Archivado = armeria.Archivado;

            _context.Armeria.Update(arma);
            await _context.SaveChangesAsync();
            return Ok("Armeria actualizado correctamente");
        }
        [HttpDelete("{ArmaId}")]
        public async Task<IActionResult> DeleteArma(int ArmaId)
        {
            var arma = await _context.Armeria
                .FirstOrDefaultAsync(f => f.ArmaId == ArmaId);
            if (arma == null)
            {
                return NotFound();
            }
            arma.Archivado = true;
            _context.Armeria.Update(arma);
            await _context.SaveChangesAsync();
            return Ok("Arma archivado");
        }
    }
}
