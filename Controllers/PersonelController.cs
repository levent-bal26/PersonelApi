using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelApi.Data;
using PersonelApi.DTOs.Personel;
using PersonelApi.Models;

namespace PersonelApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonelController : ControllerBase
{
    private readonly AppDbContext _context;

    public PersonelController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Personel
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonelListDto>>> GetAll()
    {
        var list = await _context.Personeller
            .AsNoTracking()
            .Include(p => p.Birim)
            .OrderBy(p => p.Ad)
            .ThenBy(p => p.Soyad)
            .Select(p => new PersonelListDto
            {
                PersonelId = p.PersonelId,
                Ad = p.Ad,
                Soyad = p.Soyad,
                BirimAd = p.Birim != null ? p.Birim.BirimAd : string.Empty
            })
            .ToListAsync();

        return Ok(list);
    }

    // GET: api/Personel/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PersonelDto>> GetById(int id)
    {
        var p = await _context.Personeller
            .AsNoTracking()
            .Include(x => x.Birim)
            .Include(x => x.Unvan)
            .FirstOrDefaultAsync(x => x.PersonelId == id);

        if (p is null)
            return NotFound();

        var dto = new PersonelDto
        {
            PersonelId = p.PersonelId,
            Ad = p.Ad,
            Soyad = p.Soyad,
            BirimAd = p.Birim?.BirimAd ?? string.Empty,
            UnvanAd = p.Unvan?.UnvanAd ?? string.Empty,
            Maas = p.Maas,
            Prim = p.Prim
        };

        return Ok(dto);
    }

    // POST: api/Personel
    [HttpPost]
    public async Task<ActionResult<PersonelDto>> Create([FromBody] PersonelCreateDto createDto)
    {
        // [ApiController] olduğu için geçersiz model otomatik BadRequest döner,
        // yine de açık olsun diye bırakıyorum:
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // FK kontrol (Birim/Unvan)
        var birimExists = await _context.Birimler.AnyAsync(b => b.BirimId == createDto.BirimId);
        if (!birimExists) return BadRequest("Geçersiz BirimId.");

        var unvanExists = await _context.Unvanlar.AnyAsync(u => u.UnvanId == createDto.UnvanId);
        if (!unvanExists) return BadRequest("Geçersiz UnvanId.");

        var entity = new Personel
        {
            Ad = createDto.Ad,
            Soyad = createDto.Soyad,
            DogumTarihi = createDto.DogumTarihi,

            BirimId = createDto.BirimId,
            UnvanId = createDto.UnvanId,
            IlId = createDto.IlId,
            IlceId = createDto.IlceId,

            Maas = createDto.Maas,
            Prim = createDto.Prim,
            CalismaSaati = createDto.CalismaSaati
        };

        _context.Personeller.Add(entity);
        await _context.SaveChangesAsync();

        // Detay DTO döndürmek için tekrar include ile çekelim
        var created = await _context.Personeller
            .AsNoTracking()
            .Include(x => x.Birim)
            .Include(x => x.Unvan)
            .FirstAsync(x => x.PersonelId == entity.PersonelId);

        var dto = new PersonelDto
        {
            PersonelId = created.PersonelId,
            Ad = created.Ad,
            Soyad = created.Soyad,
            BirimAd = created.Birim?.BirimAd ?? string.Empty,
            UnvanAd = created.Unvan?.UnvanAd ?? string.Empty,
            Maas = created.Maas,
            Prim = created.Prim
        };

        return CreatedAtAction(nameof(GetById), new { id = created.PersonelId }, dto);
    }

    // PUT: api/Personel/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PersonelUpdateDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = await _context.Personeller
            .FirstOrDefaultAsync(x => x.PersonelId == id);

        if (entity is null)
            return NotFound();

        var birimExists = await _context.Birimler.AnyAsync(b => b.BirimId == updateDto.BirimId);
        if (!birimExists) return BadRequest("Geçersiz BirimId.");

        var unvanExists = await _context.Unvanlar.AnyAsync(u => u.UnvanId == updateDto.UnvanId);
        if (!unvanExists) return BadRequest("Geçersiz UnvanId.");

        entity.Ad = updateDto.Ad;
        entity.Soyad = updateDto.Soyad;
        entity.DogumTarihi = updateDto.DogumTarihi;

        entity.BirimId = updateDto.BirimId;
        entity.UnvanId = updateDto.UnvanId;

        entity.Maas = updateDto.Maas;
        entity.Prim = updateDto.Prim;
        entity.CalismaSaati = updateDto.CalismaSaati;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Personel/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Personeller
            .FirstOrDefaultAsync(x => x.PersonelId == id);

        if (entity is null)
            return NotFound();

        _context.Personeller.Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}