//using AtoGobMx.Context;
//using AtoGobMx.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace AtoGobMx.Controllers
//{
//    [Authorize]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PAT_MouseController : ControllerBase
//    {
//        private readonly AtoGobMxContext _context;

//        public PAT_MouseController(AtoGobMxContext context)
//        {
//            _context = context;
//        }
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<PAT_Mouse>>> GetMouses()
//        {
//            var Mouses = await _context.Mouse
//                .Include(i => i.EquipoComputo)
//                .Where(w => !w.Archivado)
//                .ToListAsync();
//            return Ok(Mouses);
//        }

//        [HttpGet("{MouseId}")]
//        public async Task<ActionResult<PAT_Mouse>> GetMouseById(int MouseId)
//        {
//            var Mouse = await _context.Mouse.FirstOrDefaultAsync(f => f.MouseId == MouseId);

//            if (Mouse == null)
//            {
//                return NotFound();
//            }

//            return Ok(Mouse);
//        }
//        [HttpPost]
//        public async Task<ActionResult<PAT_Mouse>> PostMouse(PAT_Mouse Mouse)
//        {
//            _context.Mouse.Add(Mouse);
//            await _context.SaveChangesAsync();
//            return StatusCode(200, "Se ha credo exitosamente");
//        }
//        [HttpPut("{MouseId}")]
//        public async Task<IActionResult> PutMouse(int MouseId, PAT_Mouse mouse)
//        {
//            if (mouse.MouseId != MouseId)
//            {
//                return Ok("Los ID ingresados no coinciden");
//            }
//            var PAT_Mouse = await _context.Mouse.FindAsync(MouseId);
//            if (PAT_Mouse == null)
//            {
//                return NotFound();
//            }
//            PAT_Mouse.MouseId = mouse.MouseId;
//            PAT_Mouse.Marca = mouse.Marca;
//            PAT_Mouse.TipoConexion = mouse.TipoConexion;
//            PAT_Mouse.Archivado = mouse.Archivado;

//            _context.Mouse.Update(PAT_Mouse);
//            await _context.SaveChangesAsync();
//            return Ok("Equipo de computo actualizado correctamente");
//        }
//        [HttpDelete("{MouseId}")]
//        public async Task<IActionResult> DeleteMouse(int MouseId)
//        {
//            var mouse = await _context.Mouse
//                .FirstOrDefaultAsync(f => f.MouseId == MouseId);
//            if (mouse == null)
//            {
//                return NotFound();
//            }
//            mouse.Archivado = true;
//            _context.Mouse.Update(mouse);
//            await _context.SaveChangesAsync();
//            return Ok("Producto archivado");
//        }
//    }
//}
