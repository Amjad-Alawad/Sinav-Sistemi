using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinavSistemi.API.Data;
using SinavSistemi.API.Models;

namespace SinavSistemi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HocalarController : ControllerBase
{
    private readonly AppDbContext _db;
    public HocalarController(AppDbContext db) { _db = db; }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var liste = await _db.Hocalar
            .Select(h => new {
                h.HocaId,
                h.Ad,
                h.Soyad,
                DersSayisi = h.Dersler.Count
            }).ToListAsync();
        return Ok(liste);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var hoca = await _db.Hocalar.FindAsync(id);
        if (hoca == null) return NotFound();
        return Ok(hoca);
    }

    [HttpPost]
    public async Task<IActionResult> Ekle([FromBody] Hoca hoca)
    {
        _db.Hocalar.Add(hoca);
        await _db.SaveChangesAsync();
        return Ok(hoca);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Guncelle(int id, [FromBody] Hoca gelen)
    {
        var hoca = await _db.Hocalar.FindAsync(id);
        if (hoca == null) return NotFound();
        hoca.Ad = gelen.Ad;
        hoca.Soyad = gelen.Soyad;
        await _db.SaveChangesAsync();
        return Ok(hoca);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Sil(int id)
    {
        var hoca = await _db.Hocalar.FindAsync(id);
        if (hoca == null) return NotFound();
        _db.Hocalar.Remove(hoca);
        await _db.SaveChangesAsync();
        return Ok();
    }

}