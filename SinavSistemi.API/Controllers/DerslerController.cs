using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinavSistemi.API.Data;
using SinavSistemi.API.Models;

namespace SinavSistemi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DerslerController : ControllerBase
{
    private readonly AppDbContext _db;

    public DerslerController(AppDbContext db)
    {
        _db = db;
    }

    // 📌 GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var liste = await _db.Dersler
            .Select(d => new
            {
                d.DersId,
                d.DersAdi,
                d.HocaId,
                HocaAd = d.Hoca.Ad + " " + d.Hoca.Soyad,
                OgrenciSayisi = d.OgrenciDersleri.Count
            })
            .ToListAsync();

        return Ok(liste);
    }

    // 📌 GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ders = await _db.Dersler
            .Where(d => d.DersId == id)
            .Select(d => new
            {
                d.DersId,
                d.DersAdi,
                d.HocaId,
                HocaAd = d.Hoca.Ad + " " + d.Hoca.Soyad
            })
            .FirstOrDefaultAsync();

        if (ders == null)
            return NotFound();

        return Ok(ders);
    }

    // 📌 CREATE
    [HttpPost]
    public async Task<IActionResult> Ekle([FromBody] DersCreateDto dto)
    {
        var hocaVar = await _db.Hocalar.AnyAsync(h => h.HocaId == dto.HocaId);
        if (!hocaVar)
            return BadRequest(new { mesaj = "Hoca bulunamadı." });

        var ders = new Ders
        {
            DersAdi = dto.DersAdi,
            HocaId = dto.HocaId
        };

        _db.Dersler.Add(ders);
        await _db.SaveChangesAsync();

        return Ok(ders);
    }

    // 📌 UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Guncelle(int id, [FromBody] DersCreateDto dto)
    {
        var ders = await _db.Dersler.FindAsync(id);
        if (ders == null)
            return NotFound();

        var hocaVar = await _db.Hocalar.AnyAsync(h => h.HocaId == dto.HocaId);
        if (!hocaVar)
            return BadRequest(new { mesaj = "Hoca bulunamadı." });

        ders.DersAdi = dto.DersAdi;
        ders.HocaId = dto.HocaId;

        await _db.SaveChangesAsync();

        return Ok(ders);
    }

    // 📌 DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Sil(int id)
    {
        var ders = await _db.Dersler.FindAsync(id);
        if (ders == null)
            return NotFound();

        _db.Dersler.Remove(ders);
        await _db.SaveChangesAsync();

        return Ok();
    }

    // 📌 ÖĞRENCİ EKLE
    [HttpPost("ogrenciekle")]
    public async Task<IActionResult> OgrenciEkle([FromBody] OgrenciDersi kayit)
    {
        bool zaten = await _db.OgrenciDersleri
            .AnyAsync(x => x.OgrenciId == kayit.OgrenciId && x.DersId == kayit.DersId);

        if (zaten)
            return BadRequest(new { mesaj = "Öğrenci bu derse zaten kayıtlı." });

        _db.OgrenciDersleri.Add(kayit);
        await _db.SaveChangesAsync();

        return Ok();
    }

}