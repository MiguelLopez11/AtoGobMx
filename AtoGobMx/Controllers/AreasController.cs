﻿using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly AtoGobMxContext _context;
        public AreasController(AtoGobMxContext Context)
        {
            _context = Context;
        }
        [HttpGet]
        public async Task<ActionResult> GetAreas()
        {
            var areas = await _context.Area.ToListAsync();
            return Ok(areas);
        }
        [HttpGet("{AreaId}")]
        public async Task<ActionResult> GetAreasById(int AreaId)
        {
            var Area = await _context.Area.FindAsync(AreaId);
            if (Area == null)
            {
                Ok($"No se encuentra el area con el ID: {AreaId}");
            }
            return Ok(Area);
        }
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(Area areas)
        {
            _context.Area.Add(areas);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Se ha creado area correctamente");
        }
    }
}