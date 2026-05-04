using Microsoft.AspNetCore.Mvc;
using SinavSistemi.MVC.Models;
using SinavSistemi.MVC.Services;

namespace SinavSistemi.MVC.Controllers;

public class SalonlarController : Controller
{
    private readonly ApiService _api;
    public SalonlarController(ApiService api) { _api = api; }

    public async Task<IActionResult> Index()
    {
        var liste = await _api.Get<List<SalonVM>>("salonlar") ?? new();
        return View(liste);
    }

    public IActionResult Ekle() => View(new SalonVM());

    [HttpPost]
    public async Task<IActionResult> Ekle(SalonVM model)
    {
        var (basarili, mesaj) = await _api.Post("salonlar", new { model.SalonAdi, model.Kapasite });
        if (basarili) return RedirectToAction("Index");
        ViewBag.Hata = "Kayıt başarısız: " + mesaj;
        return View(model);
    }

    public async Task<IActionResult> Duzenle(int id)
    {
        var salon = await _api.Get<SalonVM>($"salonlar/{id}");
        if (salon == null) return RedirectToAction("Index");
        return View(salon);
    }

    [HttpPost]
    public async Task<IActionResult> Duzenle(SalonVM model)
    {
        var (basarili, mesaj) = await _api.Put($"salonlar/{model.SalonId}", new { model.SalonAdi, model.Kapasite });
        if (basarili) return RedirectToAction("Index");
        ViewBag.Hata = "Güncelleme başarısız: " + mesaj;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Sil(int id)
    {
        await _api.Delete($"salonlar/{id}");
        return RedirectToAction("Index");
    }
}