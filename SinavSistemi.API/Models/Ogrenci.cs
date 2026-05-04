namespace SinavSistemi.API.Models;

public class Ogrenci
{
    public int OgrenciId { get; set; }
    public string OgrenciNo { get; set; } = "";
    public string Ad { get; set; } = "";
    public string Soyad { get; set; } = "";
    public ICollection<OgrenciDersi> OgrenciDersleri { get; set; } = new List<OgrenciDersi>();
    public ICollection<OgrenciSinavi> OgrenciSinavlari { get; set; } = new List<OgrenciSinavi>();
}