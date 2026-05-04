public class SinavCreateDto
{
    public int DersId { get; set; }
    public int SalonId { get; set; }
    public DateOnly SinavTarihi { get; set; }
    public TimeOnly BaslangicSaati { get; set; }
    public TimeOnly BitisSaati { get; set; }
}