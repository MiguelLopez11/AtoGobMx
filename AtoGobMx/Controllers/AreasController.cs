//using AtoGobMx.Context;
//using AtoGobMx.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace AtoGobMx.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AreasController : ControllerBase
//    {
//        private readonly AtoGobMxContext _context;
//        public AreasController(AtoGobMxContext Context)
//        {
//            _context = Context;
//        }
//        [HttpGet]
//        public async Task<ActionResult> GetAreas()
//        {
//            var areas = await _context.Area
//                .Where(w => !w.Archivado)
//                .ToListAsync();
//            return Ok(areas);
//        }
//        [HttpGet("{AreaId}")]
//        public async Task<ActionResult> GetAreasById(int AreaId)
//        {
//            var Area = await _context.Area.FindAsync(AreaId);
//            if (Area == null)
//            {
//                Ok($"No se encuentra el area con el ID: {AreaId}");
//            }
//            return Ok(Area);
//        }
//        [HttpPost]
//        public async Task<ActionResult<Area>> PostArea(Area areas)
//        {
//            _context.Area.Add(areas);
//            await _context.SaveChangesAsync();
//            return StatusCode(200, "Se ha creado area correctamente");
//        }
//        [HttpPut("{AreaId}")]
//        public async Task<ActionResult> PutEmpleado(int AreaId, Area area)
//        {
//            if (area.AreaId != AreaId)
//            {
//                return Ok("Los ID ingresados no coinciden");
//            }
//            var AreaValue = await _context.Area.FindAsync(AreaId);
//            if (AreaValue == null)
//            {
//                return BadRequest("El area no existe");
//            }
//            AreaValue.AreaId = area.AreaId;
//            AreaValue.Nombre = area.Nombre;
//            AreaValue.Descripcion = area.Descripcion;

//            _context.Area.Update(AreaValue);
//            await _context.SaveChangesAsync();
//            return Ok("EL area ha sido actualizada correctamente");
//        }
//        [HttpDelete("{areaId}")]
//        public async Task<IActionResult> DeleteEmpleados(int areaId)
//        {
//            var area = await _context.Area
//                .FirstOrDefaultAsync(f => f.AreaId == areaId);
//            if (area == null)
//            {
//                return NotFound();
//            }
//            area.Archivado = true;
//            _context.Area.Update(area);
//            await _context.SaveChangesAsync();
//            return Ok("El area ha sido archivada correctamente");
//        }
//    }
//}
