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
    public class RoleController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        public RoleController(AtoGobMxContext Context, IMapper mapper)
        {
            _context = Context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var empleados = await _context.Roles
                .Include(i => i.Usuarios)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<Role>(s))
                .ToListAsync();
            return Ok(empleados);
        }

        [HttpGet("{RoleId}")]
        public async Task<ActionResult<Role>> GetRoleById(int RoleId)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(f => f.RoleId == RoleId);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{RoleId}")]
        public async Task<IActionResult> PutRole(int RoleId, Role role)
        {
            if (role.RoleId != RoleId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var Role = await _context.Roles.FindAsync(RoleId);
            if (Role == null)
            {
                return NotFound();
            }
            Role.RoleId = role.RoleId;
            Role.Nombre = role.Nombre;
            Role.Descripcion = role.Descripcion;
            Role.Archivado = role.Archivado;

            _context.Roles.Update(Role);
            await _context.SaveChangesAsync();
            return Ok("Role actualizado correctamente");
        }
        [HttpDelete("{RoleId}")]
        public async Task<IActionResult> DeleteRole(int RoleId)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(f => f.RoleId == RoleId);
            if (role == null)
            {
                return NotFound();
            }
            role.Archivado = true;
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return Ok("Role archivado");
        }
    }
}
