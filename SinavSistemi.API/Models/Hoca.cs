namespace SinavSistemi.API.Models;

public class Hoca
{
    public int HocaId { get; set; }
    public string Ad { get; set; } = "";
    public string Soyad { get; set; } = "";
    public ICollection<Ders> Dersler { get; set; } = new List<Ders>();
}