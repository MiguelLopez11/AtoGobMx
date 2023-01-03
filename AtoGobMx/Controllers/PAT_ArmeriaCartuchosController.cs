using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AtoGobMx.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PAT_ArmeriaCartuchosController : ControllerBase
    {

        private readonly AtoGobMxContext _context;

        public PAT_ArmeriaCartuchosController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet("Arma/{ArmaId}")]
        public async Task<ActionResult<IEnumerable<PAT_ArmeriaCartucho>>> GetCartuchos(int ArmaId)
        {
            var Estatus = await _context.Cartuchos
                .Include(i => i.Armeria)
                .Where(w => w.ArmeriaId == ArmaId)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Estatus);
        }

        [HttpGet("{CartuchoId}")]
        public async Task<ActionResult<PAT_ArmeriaCartucho>> GetCartuchoById(int CartuchoId)
        {
            var cartucho = await _context.Cartuchos
                .Include(i => i.Armeria)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.CartuchoId == CartuchoId);

            if (cartucho == null)
            {
                return NotFound();
            }

            return Ok(cartucho);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_ArmeriaCartucho>> PostArmeria(PAT_ArmeriaCartucho cartucho)
        {
            _context.Cartuchos.Add(cartucho);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Arma registrada exitosamente");
        }
        [HttpPut("{CartuchoId}")]
        public async Task<IActionResult> PutArmeria(int CartuchoId, PAT_ArmeriaCartucho cartucho)
        {
            if (cartucho.CartuchoId != CartuchoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var Cartucho = await _context.Cartuchos.FindAsync(CartuchoId);
            if (Cartucho == null)
            {
                return NotFound();
            }
            Cartucho.CartuchoId = cartucho.CartuchoId;
            Cartucho.Nomenclatura = cartucho.Nomenclatura;
            Cartucho.Marca = cartucho.Marca;
            Cartucho.TipoArma = cartucho.TipoArma;
            Cartucho.Calibre = cartucho.Calibre;
            Cartucho.ArmeriaId = cartucho.ArmeriaId;
            Cartucho.Archivado = cartucho.Archivado;

            _context.Cartuchos.Update(Cartucho);
            await _context.SaveChangesAsync();
            return Ok("Armeria actualizado correctamente");
        }
        [HttpDelete("{CartuchoId}")]
        public async Task<IActionResult> DeleteArma(int CartuchoId)
        {
            var cartucho = await _context.Cartuchos
                .FirstOrDefaultAsync(f => f.CartuchoId == CartuchoId);
            if (cartucho == null)
            {
                return NotFound();
            }
            cartucho.Archivado = true;
            _context.Cartuchos.Update(cartucho);
            await _context.SaveChangesAsync();
            return Ok("Arma archivado");
        }
        
    }
}
