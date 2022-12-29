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
    public class PAT_AccesorioComputoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public PAT_AccesorioComputoController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PAT_AccesorioComputo>>> GetAccesories()
        {
            var Accesories = await _context.AccesorioComputo
                .Include(i => i.EquipoComputo)
                .Where(w => !w.Archivado)
                .ToListAsync();
            return Ok(Accesories);
        }

        [HttpGet("{AccesorioId}")]
        public async Task<ActionResult<PAT_AccesorioComputo>> GetAccesorieById(int AccesorioId)
        {
            var Accesorie = await _context.AccesorioComputo
                .Include(i => i.EquipoComputo)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.AccesorioId == AccesorioId);

            if (Accesorie == null)
            {
                return NotFound();
            }

            return Ok(Accesorie);
        }
        [HttpPost]
        public async Task<ActionResult<PAT_AccesorioComputo>> PostMouse(PAT_AccesorioComputo accesorioComputo)
        {
            _context.AccesorioComputo.Add(accesorioComputo);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{AccesorioId}")]
        public async Task<IActionResult> PutMouse(int AccesorioId, PAT_AccesorioComputo accesorioComputo)
        {
            if (accesorioComputo.AccesorioId != AccesorioId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var Accesorio = await _context.AccesorioComputo.FindAsync(AccesorioId);
            if (Accesorio == null)
            {
                return NotFound();
            }
            Accesorio.AccesorioId = accesorioComputo.AccesorioId;
            Accesorio.Nombre = accesorioComputo.Nombre;
            Accesorio.Descripcion = accesorioComputo.Descripcion;
            Accesorio.Costo = accesorioComputo.Costo;
            Accesorio.FechaAdquisicion = accesorioComputo.FechaAdquisicion;
            Accesorio.EquipoComputoId = accesorioComputo.EquipoComputoId;
            Accesorio.Archivado = accesorioComputo.Archivado;

            _context.AccesorioComputo.Update(Accesorio);
            await _context.SaveChangesAsync();
            return Ok("Equipo de computo actualizado correctamente");
        }
        [HttpDelete("{AccesorioId}")]
        public async Task<IActionResult> DeleteMouse(int AccesorioId)
        {
            var accesorioComputo = await _context.AccesorioComputo
                .FirstOrDefaultAsync(f => f.AccesorioId == AccesorioId);
            if (accesorioComputo == null)
            {
                return NotFound();
            }
            accesorioComputo.Archivado = true;
            _context.AccesorioComputo.Update(accesorioComputo);
            await _context.SaveChangesAsync();
            return Ok("Producto archivado");
        }
    }
}
