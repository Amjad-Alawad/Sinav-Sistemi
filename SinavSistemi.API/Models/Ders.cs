namespace SinavSistemi.API.Models;

public class Ders
{
    public int DersId { get; set; }
    public string DersAdi { get; set; } = "";

    public int HocaId { get; set; }

    // 🔥 BURASI DÜZELTİLDİ
    public Hoca? Hoca { get; set; }

    public ICollection<OgrenciDersi> OgrenciDersleri { get; set; } = new List<OgrenciDersi>();
    public ICollection<Sinav> Sinavlar { get; set; } = new List<Sinav>();
}