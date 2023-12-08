using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using UzayProjectAPI.Models;
using UzayProjectAPI.Model;

namespace UzayProjectAPI.Controllers
{
	[ApiController]
	//[Route("api/v1/uzaylars/{gezegenID}/gezegen")]
	[Route("api/v1/uzay/{uzayID}/gezegen")]
	public class GezegenlerController : ControllerBase
	{

        private readonly ProjeContext _context;

        public GezegenlerController(ProjeContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gezegenler>>> GetGezegenler(
            [FromQuery] int? page = 1,
            [FromQuery] int? pageSize = 10,
            [FromQuery] string name = null,
            [FromQuery] string sortField = "GezegenAdi",
            [FromQuery] string sortOrder = "asc")
        {
            try
            {
                var query = _context.Gezegenler.AsQueryable();

                // Filtreleme
                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(g => g.GezegenAdi.Contains(name));
                }

                // Sıralama
                if (!string.IsNullOrEmpty(sortField))
                {
                    switch (sortOrder.ToLower())
                    {
                        case "asc":
                            query = query.OrderBy(g => EF.Property<object>(g, sortField));
                            break;
                        case "desc":
                            query = query.OrderByDescending(g => EF.Property<object>(g, sortField));
                            break;
                    }
                }

                // Sayfalama
                if (page.HasValue && pageSize.HasValue)
                {
                    query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                var gezegenler = await query.ToListAsync();
                return Ok(gezegenler);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


       
        [HttpGet("{id}")]
        public async Task<ActionResult<Gezegenler>> GetGezegen(int id)
        {
            var gezegen = await _context.Gezegenler.FindAsync(id);

            if (gezegen == null)
            {
                return NotFound("Gezegen bulunamadı.");
            }

            return Ok(gezegen);
        }



        [HttpPost]
        public async Task<ActionResult<Gezegenler>> PostGezegen(Gezegenler gezegen)
        {
            try
            {
                _context.Gezegenler.Add(gezegen);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetGezegen), new { id = gezegen.ID }, gezegen);
            }
            catch (Exception ex)
            {
                return BadRequest($"Bad Request: {ex.Message}");
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutGezegen(int id, Gezegenler gezegen)
        {
            if (id != gezegen.ID)
            {
                return BadRequest("Bad Request: Gezegen kimliği eşleşmiyor.");
            }

            _context.Entry(gezegen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GezegenExists(id))
                {
                    return NotFound("Gezegen bulunamadı.");
                }
                else
                {
                    return StatusCode(500, "Internal Server Error");
                }
            }
        }



        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchGezegen(int id, [FromBody] JsonPatchDocument<Gezegenler> patchDocument)
        {
            var gezegen = await _context.Gezegenler.FindAsync(id);

            if (gezegen == null)
            {
                return NotFound("Gezegen bulunamadı.");
            }

            patchDocument.ApplyTo(gezegen, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGezegen(int id)
        {
            var gezegen = await _context.Gezegenler.FindAsync(id);
            if (gezegen == null)
            {
                return NotFound("Gezegen bulunamadı.");
            }

            _context.Gezegenler.Remove(gezegen);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }



        private bool GezegenExists(int id)
        {
            return _context.Gezegenler.Any(e => e.ID == id);
        }

        
    }
}


