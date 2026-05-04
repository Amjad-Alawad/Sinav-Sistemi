using SinavSistemi.API.Models;

public class OgrenciSinavi
{
    public int Id { get; set; }

    public int OgrenciId { get; set; }
    public Ogrenci Ogrenci { get; set; } = null!;

    public int SinavId { get; set; }
    public Sinav Sinav { get; set; } = null!;

    public decimal? Notu { get; set; }
}