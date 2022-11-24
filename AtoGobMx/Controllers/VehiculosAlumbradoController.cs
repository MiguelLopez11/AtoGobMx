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
    public class VehiculosAlumbradoController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public VehiculosAlumbradoController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<VehiculosAlumbrado>> GetVehiculoAlumbrado()
        {
            var vehiculos = await _context.VehiculosAlumbrado
                .OrderBy(o => o.VehiculoAlumbradoId)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<VehiculosAlumbrado>(s))
                .ToArrayAsync();
            return Ok(vehiculos);
        }

        [HttpGet("{VehiculoAlumbradoId}")]
        public async Task<ActionResult> GetVehiculoAlumbradoById(int VehiculoAlumbradoId)
        {
            var vehiculo = await _context.VehiculosAlumbrado
                .FirstOrDefaultAsync(f => f.VehiculoAlumbradoId == VehiculoAlumbradoId);
            if (vehiculo == null)
            {
                //Ok($"No se encuentra la falla con el ID: {FallasId}");
                return NotFound();
            }
            return Ok();
        }
    }
}
