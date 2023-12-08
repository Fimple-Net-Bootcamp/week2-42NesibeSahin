using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UzayProjectAPI.Dtos;
using UzayProjectAPI.Model;
using UzayProjectAPI.Models;

namespace UzayProjectAPI.Controllers
{
	[Route("api/uzay")]
	[ApiController]
	public class UzayController : ControllerBase
	{
		private readonly ProjeContext _context;

		public UzayController(ProjeContext context)
		{
			_context = context;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<Uzaylar>>> GetUzay(
		   [FromQuery] int? page = 1,
		   [FromQuery] int? pageSize = 10,
		   [FromQuery] string name = null,
		   [FromQuery] string sortField = "UzayAdi",
		   [FromQuery] string sortOrder = "asc")
		{
			try
			{
				var query = _context.Uzaylar.AsQueryable();

				// Filtreleme
				if (!string.IsNullOrEmpty(name))
				{
					query = query.Where(u => u.UzayAdi.Contains(name));
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

				var uzaylar = await query.ToListAsync();
				return Ok(uzaylar);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal Server Error: {ex.Message}");
			}
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<Uzaylar>> GetUzay(int id)
		{
			var uzay = await _context.Uzaylar.Include(g => g.Gezegenler).ThenInclude(u => u.Uydular).Where(x => x.ID == id).Select(x =>
			new
			{
				UzayId = x.ID,
				UzayAdi = x.UzayAdi,				
			}).FirstOrDefaultAsync();
			if (uzay == null)
			{
				return NotFound("Uzay bulunamadı.");
			}

			return Ok(uzay);
		}

		[HttpPost]
		public async Task<ActionResult<Uzaylar>> AddUzay(string uzayAdi)
		{
			try
			{
				Uzaylar uzay = new Uzaylar(uzayAdi);
				_context.Uzaylar.Add(uzay);
				await _context.SaveChangesAsync();

				return CreatedAtAction(nameof(GetUzay), new { id = uzay.ID }, uzay);
			}
			catch (Exception ex)
			{
				return BadRequest($"Bad Request: {ex.Message}");
			}
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> PutUzay(int id, UzayDto uzayDto)
		{
			

			if (uzayDto.ID !=null && id != uzayDto.ID)
			{
				return BadRequest("Bad Request: Uzay kimliği eşleşmiyor.");
			}

			Uzaylar uzay = new Uzaylar()
			{
				
				UzayAdi = uzayDto.UzayAdi
			};

			_context.Entry(uzay).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
				return NoContent();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UzayExists(id))
				{
					return NotFound("Uzay bulunamadı.");
				}
				else
				{
					return StatusCode(500, "Internal Server Error");
				}
			}
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> PatchUzay(int id, [FromBody] JsonPatchDocument<UzayDto> patchDocument)
		{

			var uzay = await _context.Uzaylar.FindAsync(id);

			if (uzay == null)
			{
				return NotFound("Uzay bulunamadı.");
			}
			UzayDto uzayDto = new UzayDto()
			{
				UzayAdi = uzay.UzayAdi,
			};

			patchDocument.ApplyTo(uzayDto, ModelState);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				uzay.UzayAdi = uzayDto.UzayAdi;
				await _context.SaveChangesAsync();
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal Server Error: {ex.Message}");
			}
		}



		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUzay(int id)
		{
			var uzay = await _context.Uzaylar.FindAsync(id);
			if (uzay == null)
			{
				return NotFound("Uzay bulunamadı.");
			}

			_context.Uzaylar.Remove(uzay);

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

		private bool UzayExists(int id)
		{
			return _context.Uzaylar.Any(e => e.ID == id);
		}
	}
}



