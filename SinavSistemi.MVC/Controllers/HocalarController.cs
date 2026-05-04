using Microsoft.AspNetCore.Mvc;
using SinavSistemi.MVC.Models;
using SinavSistemi.MVC.Services;

namespace SinavSistemi.MVC.Controllers;

public class HocalarController : Controller
{
    private readonly ApiService _api;
    public HocalarController(ApiService api) { _api = api; }

    public async Task<IActionResult> Index()
    {
        var liste = await _api.Get<List<HocaVM>>("hocalar") ?? new();
        return View(liste);
    }

    public IActionResult Ekle() => View(new HocaVM());

    [HttpPost]
    public async Task<IActionResult> Ekle(HocaVM model)
    {
        var (basarili, mesaj) = await _api.Post("hocalar", new { model.Ad, model.Soyad });
        if (basarili) return RedirectToAction("Index");
        ViewBag.Hata = "Kayıt başarısız: " + mesaj;
        return View(model);
    }

    public async Task<IActionResult> Duzenle(int id)
    {
        var hoca = await _api.Get<HocaVM>($"hocalar/{id}");
        if (hoca == null) return RedirectToAction("Index");
        return View(hoca);
    }

    [HttpPost]
    public async Task<IActionResult> Duzenle(HocaVM model)
    {
        var (basarili, mesaj) = await _api.Put($"hocalar/{model.HocaId}", new { model.Ad, model.Soyad });
        if (basarili) return RedirectToAction("Index");
        ViewBag.Hata = "Güncelleme başarısız: " + mesaj;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Sil(int id)
    {
        await _api.Delete($"hocalar/{id}");
        return RedirectToAction("Index");
    }
}