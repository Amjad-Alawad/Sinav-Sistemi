namespace SinavSistemi.API.Models;

public class Salon
{
    public int SalonId { get; set; }
    public string SalonAdi { get; set; } = "";
    public int Kapasite { get; set; }
    public ICollection<Sinav> Sinavlar { get; set; } = new List<Sinav>();
}