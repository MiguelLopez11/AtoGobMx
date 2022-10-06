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
    public class DireccionCementerioController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        private readonly IMapper _mapper;

        public DireccionCementerioController(AtoGobMxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<DireccionCementerio>> GetDireccionCementerio()
        {
            var direccion = await _context.DireccionCementerio
                .OrderBy(o => o.NombreCementerio)
                .Where(w => !w.Archivado)
                .Select(s => _mapper.Map<DireccionCementerio>(s))
                .ToArrayAsync();
            return Ok(direccion);
        }
    }
}
