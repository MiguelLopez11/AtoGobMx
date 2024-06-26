﻿using AtoGobMx.Context;
using AtoGobMx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SERMED_CitaController : ControllerBase
    {
        private readonly AtoGobMxContext _context;

        public SERMED_CitaController(AtoGobMxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SERMED_Cita>>> GetCitas()
        {
            var Citas = await _context.Cita
                .Where(w => !w.Archivado)
                .Include(i => i.Empleados.Departamentos)
                .ToListAsync();
            return Ok(Citas);
        }

        [HttpGet("{CitaId}")]
        public async Task<ActionResult<SERMED_Cita>> GetCitaById(int CitaId)
        {
            var Cita = await _context.Cita
                .Include(i => i.Empleados)
                .Where(w => !w.Archivado)
                .FirstOrDefaultAsync(f => f.CitaId == CitaId);

            if (Cita == null)
            {
                return NotFound();
            }

            return Ok(Cita);
        }
        [HttpGet("Empleado/{EmpleadoId}")]
        public async Task<ActionResult<SERMED_Cita>> GetCitaByEmpleadoId(int EmpleadoId)
        {
            var Cita = await _context.Cita
                .Include(i => i.Empleados)
                .Where(w => !w.Archivado)
                .Where(w => w.EmpleadoId == EmpleadoId)
                .ToListAsync();
            return Ok(Cita);
        }
        [HttpPost]
        public async Task<ActionResult<SERMED_Cita>> PostInventario(SERMED_Cita cita)
        {
            _context.Cita.Add(cita);
            await _context.SaveChangesAsync();
            return StatusCode(200, "Cita Se ha credo exitosamente");
        }
        [HttpPut("{CitaId}")]
        public async Task<IActionResult> PutCita(int CitaId, SERMED_Cita cita)
        {
            if (cita.CitaId != CitaId)
            {
                return Ok("Los ID ingresados no coinciden");
            }
            var Cita = await _context.Cita.FindAsync(CitaId);
            if (Cita == null)
            {
                return NotFound();
            }
            Cita.CitaId = cita.CitaId;
            Cita.Motivo = cita.Motivo;
            Cita.FechaDesde = cita.FechaDesde;
            Cita.FechaHasta = cita.FechaHasta;
            Cita.Descripcion = cita.Descripcion;
            Cita.Archivado = cita.Archivado;

            _context.Cita.Update(Cita);
            await _context.SaveChangesAsync();
            return Ok("Cita actualizada correctamente");
        }
        [HttpDelete("{CitaId}")]
        public async Task<IActionResult> DeleteCita(int CitaId)
        {
            var cita = await _context.Cita
                .FirstOrDefaultAsync(f => f.CitaId == CitaId);
            if (cita == null)
            {
                return NotFound();
            }
            cita.Archivado = true;
            _context.Cita.Update(cita);
            await _context.SaveChangesAsync();
            return Ok("Cita archivada");
        }
    }
}
