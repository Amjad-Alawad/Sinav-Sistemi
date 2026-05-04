namespace SinavSistemi.API.Models;

public class OgrenciDersi
{
    public int Id { get; set; }
    public int OgrenciId { get; set; }
    public int DersId { get; set; }
    public Ogrenci Ogrenci { get; set; } = null!;
    public Ders Ders { get; set; } = null!;
}