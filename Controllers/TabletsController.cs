using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo03.Models;

namespace Demo03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabletsController : ControllerBase
    {
        private readonly TabletContext _context;

        public TabletsController(TabletContext context)
        {
            _context = context;
        }

        // GET: api/Tablets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tablet>>> GetTablets()
        {
            return await _context.Tablets.ToListAsync();
        }

        // GET: api/Tablets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tablet>> GetTablet(int id)
        {
            var tablet = await _context.Tablets.FindAsync(id);

            if (tablet == null)
            {
                return NotFound();
            }

            return tablet;
        }

        // PUT: api/Tablets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTablet(int id, Tablet tablet)
        {
            if (id != tablet.Id)
            {
                return BadRequest();
            }

            _context.Entry(tablet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TabletExists(id))
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

        // POST: api/Tablets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tablet>> PostTablet(Tablet tablet)
        {
            _context.Tablets.Add(tablet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTablet", new { id = tablet.Id }, tablet);
        }

        // DELETE: api/Tablets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTablet(int id)
        {
            var tablet = await _context.Tablets.FindAsync(id);
            if (tablet == null)
            {
                return NotFound();
            }

            _context.Tablets.Remove(tablet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TabletExists(int id)
        {
            return _context.Tablets.Any(e => e.Id == id);
        }
    }
}
