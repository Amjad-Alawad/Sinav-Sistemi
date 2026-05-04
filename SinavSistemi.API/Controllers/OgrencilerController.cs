using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinavSistemi.API.Data;
using SinavSistemi.API.Models;

namespace SinavSistemi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OgrencilerController : ControllerBase
{
    private readonly AppDbContext _db;
    public OgrencilerController(AppDbContext db) { _db = db; }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var liste = await _db.Ogrenciler
            .Select(x => new OgrenciDto
            {
                OgrenciId = x.OgrenciId,
                OgrenciNo = x.OgrenciNo,
                Ad = x.Ad,
                Soyad = x.Soyad
            })
            .ToListAsync();

        return Ok(liste);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ogrenci = await _db.Ogrenciler.FindAsync(id);
        if (ogrenci == null) return NotFound();
        return Ok(ogrenci);
    }

    [HttpPost]
    public async Task<IActionResult> Ekle([FromBody] Ogrenci ogrenci)
    {
        bool varMi = await _db.Ogrenciler.AnyAsync(o => o.OgrenciNo == ogrenci.OgrenciNo);
        if (varMi) return BadRequest(new { mesaj = "Bu öğrenci numarası zaten kayıtlı." });
        _db.Ogrenciler.Add(ogrenci);
        await _db.SaveChangesAsync();
        return Ok(ogrenci);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Guncelle(int id, [FromBody] Ogrenci gelen)
    {
        var ogrenci = await _db.Ogrenciler.FindAsync(id);
        if (ogrenci == null) return NotFound();
        ogrenci.Ad = gelen.Ad;
        ogrenci.Soyad = gelen.Soyad;
        ogrenci.OgrenciNo = gelen.OgrenciNo;
        await _db.SaveChangesAsync();
        return Ok(ogrenci);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Sil(int id)
    {
        var ogrenci = await _db.Ogrenciler.FindAsync(id);
        if (ogrenci == null) return NotFound();
        _db.Ogrenciler.Remove(ogrenci);
        await _db.SaveChangesAsync();
        return Ok();
    }


}