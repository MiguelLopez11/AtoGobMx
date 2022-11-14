using AtoGobMx.Context;
using AtoGobMx.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PROV_ProveedorController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public PROV_ProveedorController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PROV_Proveedor>> GetProveedor()
        {
            var proveedor = await _context.PROV_Proveedor
                .OrderBy(o => o.ProveedorId)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<PROV_Proveedor>(s))
                .ToArrayAsync();
            return Ok(proveedor);
        }

        [HttpGet("{ProveedorId}")]
        public async Task<ActionResult> GetProveedorById(int ProveedorId)
        {
            var proveedor = await _context.PROV_Proveedor
                .FirstOrDefaultAsync(f => f.ProveedorId == ProveedorId);
            if (proveedor == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok(proveedor);
        }

        [HttpPost]
        public async Task<ActionResult<PROV_Proveedor>> PostProveedor(PROV_Proveedor proveedor)
        {
            object value = _context.PROV_Proveedor.Add(proveedor);
            await _context.SaveChangesAsync();
            return Ok("Proveedor creado correctamente");
        }

        [HttpPut("{ProveedorId}")]
        public async Task<ActionResult> PutProveedor(int ProveedorId, PROV_Proveedor Proveedor)
        {
            if (Proveedor.ProveedorId != ProveedorId)
            {
                return Ok("Los ID no ingresados no coinciden");
            }

            var proveedor = await _context.PROV_Proveedor.FirstOrDefaultAsync(f => f.ProveedorId == ProveedorId);
            if (proveedor == null)
            {
                return BadRequest("El Registro del proveedor no existe");
            }

            proveedor.ProveedorId = ProveedorId;
            proveedor.Nombre = Proveedor.Nombre;
            proveedor.RepresentanteLegal = Proveedor.RepresentanteLegal;
            proveedor.Direccion = Proveedor.Direccion;
            proveedor.Telefono = Proveedor.Telefono;
            proveedor.Email = Proveedor.Email;
            proveedor.Archivado = Proveedor.Archivado;

            _context.PROV_Proveedor.Update(proveedor);
            await _context.SaveChangesAsync();
            return Ok("Proveedor actualizado correctamente");
        }

        [HttpDelete("{ProveedorId}")]
        public async Task<IActionResult> DeleteProveedor(int ProveedorId)
        {
            var proveedor = _context.PROV_Proveedor
                .FirstOrDefault(f => f.ProveedorId == ProveedorId);
            if (proveedor == null)
            {
                return NotFound();
            }

            proveedor.Archivado = true;
            _context.PROV_Proveedor.Update(proveedor);
            await _context.SaveChangesAsync();
            return Ok("Proveedor Archivado");
        }
    }
}
