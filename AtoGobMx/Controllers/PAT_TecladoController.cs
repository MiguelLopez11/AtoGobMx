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
    public class PAT_TecladoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public PAT_TecladoController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PAT_Teclado>>> GetTeclados()
        {
            var Mouses = await _context.Teclado
                .Include(i => i.EquipoComputo)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Mouses);
        }

        [HttpGet("{TecladoId}")]
        public async Task<ActionResult<PAT_Teclado>> GetTecladoById(int TecladoId)
        {
            var teclado = await _context.Teclado.FirstOrDefaultAsync(f => f.TecladoId == TecladoId);

            if (teclado == null)
            {
                return NotFound();
            }

            return Ok(teclado);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_Teclado>> PostTeclado(PAT_Teclado teclado)
        {
            _context.Teclado.Add(teclado);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{TecladoId}")]
        public async Task<IActionResult> PutTeclado(int TecladoId, PAT_Teclado teclado)
        {
            if (teclado.TecladoId != TecladoId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var PAT_Teclado = await _context.Teclado.FindAsync(TecladoId);
            if (PAT_Teclado == null)
            {
                return NotFound();
            }
            PAT_Teclado.TecladoId = teclado.TecladoId;
            PAT_Teclado.Marca = teclado.Marca;
            PAT_Teclado.TipoConexion = teclado.TipoConexion;
            PAT_Teclado.Archivado = teclado.Archivado;

            _context.Teclado.Update(PAT_Teclado);
            await _context.SaveChangesAsync();
            return Ok("Teclado actualizado correctamente");
        }
        [HttpDelete("{TecladoId}")]
        public async Task<IActionResult> DeleteTeclado(int TecladoId)
        {
            var teclado = await _context.Teclado
                .FirstOrDefaultAsync(f => f.TecladoId == TecladoId);
            if (teclado == null)
            {
                return NotFound();
            }
            teclado.Archivado = true;
            _context.Teclado.Update(teclado);
            await _context.SaveChangesAsync();
            return Ok("Producto archivado");
        }
    }
}
