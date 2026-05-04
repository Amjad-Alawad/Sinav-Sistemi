public class OgrenciSinaviDetayDto
{
    public int Id { get; set; }
    public int OgrenciId { get; set; }
    public string OgrenciNo { get; set; }
    public string OgrenciAdi { get; set; }
    public string DersAdi { get; set; }
    public string HocaAdi { get; set; }
    public string SalonAdi { get; set; }
    public DateOnly SinavTarihi { get; set; }
    public TimeOnly BaslangicSaati { get; set; }
    public TimeOnly BitisSaati { get; set; }
    public decimal? Notu { get; set; }
}