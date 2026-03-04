using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelApi.Data;          // AppDbContext burada
using PersonelApi.DTOs.Birim;    // DTO'lar burada olmalı
using PersonelApi.Models;

namespace PersonelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BirimController : ControllerBase
{
    private readonly AppDbContext _context;

    public BirimController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Birim
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BirimListDto>>> GetAll()
    {
        var list = await _context.Birimler
            .AsNoTracking()
            .OrderBy(x => x.BirimAd)
            .Select(x => new BirimListDto
            {
                BirimId = x.BirimId,
                BirimAd = x.BirimAd
            })
            .ToListAsync();

        return Ok(list);
    }

    // GET: api/Birim/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BirimDto>> GetById(int id)
    {
        var entity = await _context.Birimler
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BirimId == id);

        if (entity is null)
            return NotFound();

        var dto = new BirimDto
        {
            BirimId = entity.BirimId,
            BirimAd = entity.BirimAd
        };

        return Ok(dto);
    }

    // POST: api/Birim
    [HttpPost]
    public async Task<ActionResult<BirimDto>> Create([FromBody] BirimCreateDto createDto)
    {
        // Not: [ApiController] olduğu için model doğrulama otomatik BadRequest döner.
        // Yine de açık görmek istersen aşağıdaki satır kalabilir.
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = new Birim
        {
            BirimAd = createDto.BirimAd
        };

        _context.Birimler.Add(entity);
        await _context.SaveChangesAsync();

        var dto = new BirimDto
        {
            BirimId = entity.BirimId,
            BirimAd = entity.BirimAd
        };

        return CreatedAtAction(nameof(GetById), new { id = entity.BirimId }, dto);
    }

    // PUT: api/Birim/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] BirimUpdateDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = await _context.Birimler
            .FirstOrDefaultAsync(x => x.BirimId == id);

        if (entity is null)
            return NotFound();

        entity.BirimAd = updateDto.BirimAd;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Birim/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Birimler
            .FirstOrDefaultAsync(x => x.BirimId == id);

        if (entity is null)
            return NotFound();

        // Bu birime bağlı personel varsa silme (istersen kaldırabilirsin)
        var hasPersonel = await _context.Personeller
            .AnyAsync(p => p.BirimId == id);

        if (hasPersonel)
            return Conflict("Bu birime bağlı personel(ler) var. Önce personelleri taşıyın/silin.");

        _context.Birimler.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}