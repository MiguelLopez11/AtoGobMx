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
    public class UsuariosController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public UsuariosController(AtoGobMxContext Context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = Context;
            _mapper = mapper;
            _env = env;
        }
        #region CRUD
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var Usuarios = await _context.Usuarios
                .Include(i => i.Role)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<Usuario>(s))
                .ToListAsync();
            return Ok(Usuarios);
        }

        [HttpGet("{UsuarioId}")]
        public async Task<ActionResult<Usuario>> GetUsuarioById(int UsuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(UsuarioId);

            if (usuario == null)
            {
                return NotFound("No se encuentra el usuario");
            }

            return usuario;
        }
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha credo exitosamente");
        }
        [HttpPut("{UsuarioId}")]
        public async Task<IActionResult> PutUsuario(int UsuarioId, Usuario usuario)
        {
            if (usuario.UsuarioId != UsuarioId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var Usuario = await _context.Usuarios.FindAsync(UsuarioId);
            if (Usuario == null)
            {
                return NotFound();
            }
            Usuario.UsuarioId = usuario.UsuarioId;
            Usuario.NombreUsuario = usuario.NombreUsuario;
            Usuario.Contraseña = usuario.Contraseña;
            Usuario.ConfirmarContraseña = usuario.ConfirmarContraseña;
            Usuario.Archivado = usuario.Archivado;
            Usuario.RoleId = usuario.RoleId;
            Usuario.EmpleadoId = usuario.EmpleadoId;


            _context.Usuarios.Update(Usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuario actualizado correctamente");
        }
        [HttpDelete("{UsuarioId}")]
        public async Task<IActionResult> DeleteUsuario(int UsuarioId)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(f => f.UsuarioId == UsuarioId);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.Archivado = true;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuario archivado");
        }
        #endregion
        #region UploadImages


        #endregion
    }
}