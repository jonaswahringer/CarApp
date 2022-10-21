using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarAPI.Database;
using CarAPI.Models;

namespace CarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetryController : ControllerBase
    {
        private readonly CarContext _context;

        public TelemetryController(CarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telemetry>>> GetTelemetries()
        {
          if (_context.Telemetries == null)
          {
              return NotFound();
          }
            return await _context.Telemetries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Telemetry>> GetTelemetry(int id)
        {
          if (_context.Telemetries == null)
          {
              return NotFound();
          }
            var telemetry = await _context.Telemetries.FindAsync(id);

            if (telemetry == null)
            {
                return NotFound();
            }

            return telemetry;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelemetry(int id, Telemetry telemetry)
        {
            if (id != telemetry.IdTelemetry)
            {
                return BadRequest();
            }

            _context.Entry(telemetry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelemetryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Telemetry>> PostTelemetry(Telemetry telemetry)
        {
          if (_context.Telemetries == null)
          {
              return Problem("Entity set 'CarContext.Telemetries'  is null.");
          }
            _context.Telemetries.Add(telemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTelemetry", new { id = telemetry.IdTelemetry }, telemetry);
        }

        // DELETE: api/Telemetry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelemetry(int id)
        {
            if (_context.Telemetries == null)
            {
                return NotFound();
            }
            var telemetry = await _context.Telemetries.FindAsync(id);
            if (telemetry == null)
            {
                return NotFound();
            }

            _context.Telemetries.Remove(telemetry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TelemetryExists(int id)
        {
            return (_context.Telemetries?.Any(e => e.IdTelemetry == id)).GetValueOrDefault();
        }
    }
}
