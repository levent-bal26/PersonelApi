using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelApi.Data;
using PersonelApi.Models;

namespace PersonelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UnvanController : ControllerBase
{
    private readonly AppDbContext _context;

    public UnvanController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Unvan
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Unvan>>> GetAll()
    {
        var list = await _context.Unvanlar
            .AsNoTracking()
            .OrderBy(x => x.UnvanAd)
            .ToListAsync();

        return Ok(list);
    }

    // GET: api/Unvan/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Unvan>> GetById(int id)
    {
        var entity = await _context.Unvanlar
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UnvanId == id);

        if (entity is null)
            return NotFound();

        return Ok(entity);
    }

    // POST: api/Unvan
    [HttpPost]
    public async Task<ActionResult<Unvan>> Create([FromBody] Unvan model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Unvanlar.Add(model);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = model.UnvanId }, model);
    }

    // PUT: api/Unvan/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Unvan model)
    {
        if (id != model.UnvanId)
            return BadRequest("Id uyuşmuyor.");

        var entity = await _context.Unvanlar.FirstOrDefaultAsync(x => x.UnvanId == id);
        if (entity is null)
            return NotFound();

        entity.UnvanAd = model.UnvanAd;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Unvan/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Unvanlar.FirstOrDefaultAsync(x => x.UnvanId == id);
        if (entity is null)
            return NotFound();

        _context.Unvanlar.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}