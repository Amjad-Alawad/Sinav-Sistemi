using SinavSistemi.API.Models;

public class Sinav
{
    public int SinavId { get; set; }
    public int DersId { get; set; }
    public int SalonId { get; set; }

    public DateOnly SinavTarihi { get; set; }
    public TimeOnly BaslangicSaati { get; set; }
    public TimeOnly BitisSaati { get; set; }

    public Ders Ders { get; set; } = null!;
    public Salon Salon { get; set; } = null!;
    public ICollection<OgrenciSinavi> OgrenciSinavlari { get; set; } = new List<OgrenciSinavi>();
}