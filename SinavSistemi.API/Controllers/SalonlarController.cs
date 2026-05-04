using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinavSistemi.API.Data;
using SinavSistemi.API.Models;

namespace SinavSistemi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalonlarController : ControllerBase
{
    private readonly AppDbContext _db;
    public SalonlarController(AppDbContext db) { _db = db; }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var liste = await _db.Salonlar.ToListAsync();
        return Ok(liste);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var salon = await _db.Salonlar.FindAsync(id);
        if (salon == null) return NotFound();
        return Ok(salon);
    }

    [HttpPost]
    public async Task<IActionResult> Ekle([FromBody] Salon salon)
    {
        _db.Salonlar.Add(salon);
        await _db.SaveChangesAsync();
        return Ok(salon);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Guncelle(int id, [FromBody] Salon gelen)
    {
        var salon = await _db.Salonlar.FindAsync(id);
        if (salon == null) return NotFound();
        salon.SalonAdi = gelen.SalonAdi;
        salon.Kapasite = gelen.Kapasite;
        await _db.SaveChangesAsync();
        return Ok(salon);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Sil(int id)
    {
        var salon = await _db.Salonlar.FindAsync(id);
        if (salon == null) return NotFound();
        _db.Salonlar.Remove(salon);
        await _db.SaveChangesAsync();
        return Ok();
    }

}