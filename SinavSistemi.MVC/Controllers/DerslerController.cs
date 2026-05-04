using Microsoft.AspNetCore.Mvc;
using SinavSistemi.MVC.Models;
using SinavSistemi.MVC.Services;

namespace SinavSistemi.MVC.Controllers;

public class DerslerController : Controller
{
    private readonly ApiService _api;

    public DerslerController(ApiService api)
    {
        _api = api;
    }

    // 📌 LİSTE
    public async Task<IActionResult> Index()
    {
        var liste = await _api.Get<List<DersVM>>("dersler") ?? new();
        return View(liste);
    }

    // 📌 EKLE (GET)
    public async Task<IActionResult> Ekle()
    {
        var model = new DersFormVM
        {
            Hocalar = await _api.Get<List<HocaVM>>("hocalar") ?? new()
        };

        return View(model);
    }

    // 📌 EKLE (POST)
    [HttpPost]
    public async Task<IActionResult> Ekle(string DersAdi, int HocaId)
    {
        var (basarili, mesaj) = await _api.Post("dersler", new
        {
            DersAdi,
            HocaId
        });

        if (basarili)
            return RedirectToAction("Index");

        ViewBag.Hata = mesaj;
        ViewBag.Hocalar = await _api.Get<List<HocaVM>>("hocalar") ?? new();

        return View();
    }

    // 📌 DÜZENLE (GET)
    public async Task<IActionResult> Duzenle(int id)
    {
        var ders = await _api.Get<DersVM>($"dersler/{id}");
        if (ders == null)
            return RedirectToAction("Index");

        ViewBag.Hocalar = await _api.Get<List<HocaVM>>("hocalar") ?? new();

        return View(ders);
    }

    // 📌 DÜZENLE (POST)
    [HttpPost]
    public async Task<IActionResult> Duzenle(DersVM model)
    {
        var (basarili, mesaj) = await _api.Put($"dersler/{model.DersId}", new
        {
            model.DersAdi,
            model.HocaId
        });

        if (basarili)
            return RedirectToAction("Index");

        ViewBag.Hata = mesaj;
        ViewBag.Hocalar = await _api.Get<List<HocaVM>>("hocalar") ?? new();

        return View(model);
    }

    // 📌 SİL
    [HttpPost]
    public async Task<IActionResult> Sil(int id)
    {
        await _api.Delete($"dersler/{id}");
        return RedirectToAction("Index");
    }
}