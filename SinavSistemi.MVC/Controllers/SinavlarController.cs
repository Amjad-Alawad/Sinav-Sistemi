using Microsoft.AspNetCore.Mvc;
using SinavSistemi.MVC.Models;
using SinavSistemi.MVC.Services;

namespace SinavSistemi.MVC.Controllers;

public class SinavlarController : Controller
{
    private readonly ApiService _api;
    public SinavlarController(ApiService api) { _api = api; }

    // 📌 LIST
    public async Task<IActionResult> Index()
    {
        var liste = await _api.Get<List<SinavVM>>("sinavlar") ?? new();
        return View(liste);
    }

    // 📌 EKLE SAYFASI
    public async Task<IActionResult> Ekle()
    {
        var model = new SinavFormVM
        {
            Dersler = await _api.Get<List<DersVM>>("dersler") ?? new(),
            Salonlar = await _api.Get<List<SalonVM>>("salonlar") ?? new()
        };

        return View(model);
    }

    // 📌 EKLE POST
    [HttpPost]
    public async Task<IActionResult> Ekle(SinavFormVM model)
    {
        var (basarili, mesaj) = await _api.Post("sinavlar", new
        {
            model.DersId,
            model.SalonId,
            SinavTarihi = DateOnly.Parse(model.SinavTarihi).ToString("yyyy-MM-dd"),
            BaslangicSaati = TimeOnly.Parse(model.BaslangicSaati).ToString("HH:mm:ss"),
            BitisSaati = TimeOnly.Parse(model.BitisSaati).ToString("HH:mm:ss")
        });

        if (basarili)
            return RedirectToAction("Index");

        ViewBag.Hata = mesaj;

        model.Dersler = await _api.Get<List<DersVM>>("dersler") ?? new();
        model.Salonlar = await _api.Get<List<SalonVM>>("salonlar") ?? new();

        return View(model);
    }

    // 📌 DÜZENLE SAYFASI (GET) ✅ FIX
    public async Task<IActionResult> Duzenle(int id)
    {
        var sinav = await _api.Get<SinavVM>($"sinavlar/{id}");
        if (sinav == null) return RedirectToAction("Index");

        var model = new SinavFormVM
        {
            SinavId = sinav.SinavId,
            DersId = sinav.DersId,
            SalonId = sinav.SalonId,
            SinavTarihi = sinav.SinavTarihi,
            BaslangicSaati = sinav.BaslangicSaati,
            BitisSaati = sinav.BitisSaati,

            Dersler = await _api.Get<List<DersVM>>("dersler") ?? new(),
            Salonlar = await _api.Get<List<SalonVM>>("salonlar") ?? new()
        };

        return View(model);
    }

    // 📌 DÜZENLE POST
    [HttpPost]
    public async Task<IActionResult> Duzenle(SinavFormVM model)
    {
        var (basarili, mesaj) = await _api.Put($"sinavlar/{model.SinavId}", new
        {
            model.DersId,
            model.SalonId,
            SinavTarihi = DateOnly.Parse(model.SinavTarihi).ToString("yyyy-MM-dd"),
            BaslangicSaati = TimeOnly.Parse(model.BaslangicSaati).ToString("HH:mm:ss"),
            BitisSaati = TimeOnly.Parse(model.BitisSaati).ToString("HH:mm:ss")
        });

        if (basarili)
            return RedirectToAction("Index");

        ViewBag.Hata = mesaj;

        model.Dersler = await _api.Get<List<DersVM>>("dersler") ?? new();
        model.Salonlar = await _api.Get<List<SalonVM>>("salonlar") ?? new();

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Sil(int id)
    {
        return Content("GELDİ: " + id);
    }
    // 📌 DETAY
    public async Task<IActionResult> Detay(int id)
    {
        var liste = await _api.Get<List<OgrenciSinaviVM>>($"sinavlar/detay/{id}") ?? new();
        var sinav = await _api.Get<SinavVM>($"sinavlar/{id}");

        ViewBag.SinavBilgi = $"{sinav?.DersAdi} — {sinav?.SinavTarihi}";
        ViewBag.SinavId = id;

        return View(liste);
    }

    // 📌 NOT GİR
    public async Task<IActionResult> NotGir(int id)
    {
        var liste = await _api.Get<List<OgrenciSinaviVM>>($"sinavlar/detay/{id}") ?? new();
        ViewBag.SinavId = id;

        return View(liste);
    }

    [HttpPost]
    public async Task<IActionResult> NotKaydet(int ogrenciId, int sinavId, decimal? notu)
    {
        await _api.Put("sinavlar/notgir", new
        {
            OgrenciId = ogrenciId,
            SinavId = sinavId,
            Notu = notu
        });

        return RedirectToAction("NotGir", new { id = sinavId });
    }

    // 📌 ÖĞRENCİ EKLE
    public async Task<IActionResult> OgrenciEkle(int id)
    {
        var tumOgrenciler = await _api.Get<List<OgrenciVM>>("ogrenciler") ?? new();
        var sinav = await _api.Get<SinavVM>($"sinavlar/{id}");

        ViewBag.SinavId = id;
        ViewBag.SinavBilgi = $"{sinav?.DersAdi} — {sinav?.SinavTarihi}";

        return View(tumOgrenciler);
    }

    [HttpPost]
    public async Task<IActionResult> OgrenciEkleKaydet(int sinavId, int ogrenciId)
    {
        if (sinavId <= 0 || ogrenciId <= 0)
        {
            TempData["Hata"] = "Geçersiz ID";
            return RedirectToAction("Detay", new { id = sinavId });
        }

        var (ok, msg) = await _api.Post("sinavlar/ogrenciekle", new
        {
            SinavId = sinavId,
            OgrenciId = ogrenciId
        });

        if (!ok)
        {
            TempData["Hata"] = msg;
        }

        return RedirectToAction("Detay", new { id = sinavId });
    }
}