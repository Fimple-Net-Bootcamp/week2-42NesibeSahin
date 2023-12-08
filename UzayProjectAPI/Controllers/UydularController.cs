using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics;
using UzayProjectAPI.Model;
using UzayProjectAPI.Models;

namespace UzayProjectAPI.Controllers
{
    [ApiController]
    //[Route("api/v1/gezegenlers/{uyduID}/uydu")]
    //[Route("api/v1/uzaylars/{gezegenID}/gezegen/{uyduID}/uydulars")]

    [Route("api/v1/uzay/{uzayID}/gezegen/{gezegenID}/uydu")]

	public class UydularController : ControllerBase
{
    private readonly ProjeContext _context;

    public UydularController(ProjeContext context)
    {
        _context = context;
    }



    [HttpGet]
    public async Task<ActionResult<IEnumerable<Uydular>>> GetUydular(
        [FromQuery] int? page = 1,
        [FromQuery] int? pageSize = 10,
        [FromQuery] string status = null,
        [FromQuery] string sortField = "UyduAdi",
        [FromQuery] string sortOrder = "asc")
    {
        try
        {
            var query = _context.Uydular.AsQueryable();

            // Filtreleme
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(u => u.Status == status);
            }

            // Sıralama
            if (!string.IsNullOrEmpty(sortField))
            {
                switch (sortOrder.ToLower())
                {
                    case "asc":
                        query = query.OrderBy(u => EF.Property<object>(u, sortField));
                        break;
                    case "desc":
                        query = query.OrderByDescending(u => EF.Property<object>(u, sortField));
                        break;
                }
            }

            // Sayfalama
            if (page.HasValue && pageSize.HasValue)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            var uydular = await query.ToListAsync();
            return Ok(uydular);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<Uydular>> GetUydu(int id)
    {
        var uydu = await _context.Uydular.FindAsync(id);

        if (uydu == null)
        {
            return NotFound();
        }

        return uydu;
    }



    [HttpPost]
    //[Route("UyduEkle")]
    public async Task<ActionResult<Uydular>> PostUydu(Uydular uydu)
    {
        try
        {
            _context.Uydular.Add(uydu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUydu), new { id = uydu.ID }, uydu);
        }
        catch (Exception ex)
        {
            return BadRequest($"Bad Request: {ex.Message}");
        }
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> PutUydu(int id, Uydular uydu)
    {
        if (id != uydu.ID)
        {
            return BadRequest("Bad Request: Uydu kimliği eşleşmiyor.");
        }

        _context.Entry(uydu).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UyduExists(id))
            {
                return NotFound("Uydu bulunamadı.");
            }
            else
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        return NoContent();
    }



    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUydu(int id, [FromBody] JsonPatchDocument<Uydular> patchDocument)
    {
        var uydu = await _context.Uydular.FindAsync(id);

        if (uydu == null)
        {
            return NotFound("Uydu bulunamadı.");
        }

        patchDocument.ApplyTo(uydu, ModelState);

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
    public async Task<IActionResult> DeleteUydu(int id)
    {
        var uydu = await _context.Uydular.FindAsync(id);
        if (uydu == null)
        {
            return NotFound("Uydu bulunamadı.");
        }

        _context.Uydular.Remove(uydu);

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



    private bool UyduExists(int id)
    {
        return _context.Uydular.Any(e => e.ID == id);
    }


}
}


