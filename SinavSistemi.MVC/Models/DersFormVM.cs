namespace SinavSistemi.MVC.Models;

public class DersFormVM
{
    public string DersAdi { get; set; } = "";
    public int HocaId { get; set; }

    public List<HocaVM> Hocalar { get; set; } = new();
}