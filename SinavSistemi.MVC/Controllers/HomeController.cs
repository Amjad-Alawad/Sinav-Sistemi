using Microsoft.AspNetCore.Mvc;
using SinavSistemi.MVC.Models;
using SinavSistemi.MVC.Services;

namespace SinavSistemi.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ApiService _api;
    public HomeController(ApiService api) { _api = api; }

    public async Task<IActionResult> Index()
    {
        var hocalar = await _api.Get<List<HocaVM>>("hocalar") ?? new();
        var ogrenciler = await _api.Get<List<OgrenciVM>>("ogrenciler") ?? new();
        var dersler = await _api.Get<List<DersVM>>("dersler") ?? new();
        var sinavlar = await _api.Get<List<SinavVM>>("sinavlar") ?? new();

        ViewBag.HocaSayisi = hocalar.Count;
        ViewBag.OgrenciSayisi = ogrenciler.Count;
        ViewBag.DersSayisi = dersler.Count;
        ViewBag.SinavSayisi = sinavlar.Count;
        ViewBag.SonSinavlar = sinavlar.TakeLast(5).ToList();

        return View();
    }
}