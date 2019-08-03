using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkShopRestfullApi.Model;

namespace WorkShopRestfullApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabanasController : ControllerBase
    {
        private readonly CabanasContext _context;

        public CabanasController(CabanasContext context)
        {
            _context = context;
        }

        // GET: api/Cabanas
        [ProducesResponseType(200, Type = typeof(List<Cabana>))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cabana>>> GetCabanas()
        {
            return await _context.Cabanas.ToListAsync();
        }

        // GET: api/Cabanas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cabana>> GetCabana(int id)
        {
            var cabana = await _context.Cabanas.FindAsync(id);

            if (cabana == null)
            {
                return NotFound();
            }

            return cabana;
        }

        // PUT: api/Cabanas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCabana(int id, Cabana cabana)
        {
            if (id != cabana.Id)
            {
                return BadRequest();
            }

            _context.Entry(cabana).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CabanaExists(id))
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

        // POST: api/Cabanas
       // [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails)]
        [HttpPost]
        public async Task<ActionResult<Cabana>> PostCabana(Cabana cabana)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Cabanas.Add(cabana);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCabana", new { id = cabana.Id }, cabana);
        }

        // DELETE: api/Cabanas/5
       //[ProducesResponseType((int)HttpStatusCode.NotFound]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cabana>> DeleteCabana(int id)
        {
            var cabana = await _context.Cabanas.FindAsync(id);
            if (cabana == null)
            {
                return NotFound();
            }

            _context.Cabanas.Remove(cabana);
            await _context.SaveChangesAsync();

            return cabana;
        }

        private bool CabanaExists(int id)
        {
            return _context.Cabanas.Any(e => e.Id == id);
        }
    }
}
