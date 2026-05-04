namespace SinavSistemi.MVC.Models;

public class HocaVM
{
    public int HocaId { get; set; }
    public string Ad { get; set; } = "";
    public string Soyad { get; set; } = "";
    public int DersSayisi { get; set; }
}

public class SalonVM
{
    public int SalonId { get; set; }
    public string SalonAdi { get; set; } = "";
    public int Kapasite { get; set; }
}

public class OgrenciVM
{
    public int OgrenciId { get; set; }
    public string OgrenciNo { get; set; } = "";
    public string Ad { get; set; } = "";
    public string Soyad { get; set; } = "";
}

public class DersVM
{
    public int DersId { get; set; }
    public string DersAdi { get; set; } = "";
    public int HocaId { get; set; }
    public string HocaAd { get; set; } = "";
    public int OgrenciSayisi { get; set; }
}

public class SinavVM
{
    public int SinavId { get; set; }
    public int DersId { get; set; }
    public int SalonId { get; set; }
    public string DersAdi { get; set; } = "";
    public string HocaAdi { get; set; } = "";
    public string SalonAdi { get; set; } = "";
    public string SinavTarihi { get; set; } = "";
    public string BaslangicSaati { get; set; } = "";
    public string BitisSaati { get; set; } = "";
    public int OgrenciSayisi { get; set; }
}

public class SinavFormVM
{
    public int SinavId { get; set; }
    public int DersId { get; set; }
    public int SalonId { get; set; }
    public string SinavTarihi { get; set; } = "";
    public string BaslangicSaati { get; set; } = "";
    public string BitisSaati { get; set; } = "";
    public List<DersVM> Dersler { get; set; } = new();
    public List<SalonVM> Salonlar { get; set; } = new();
}

public class OgrenciSinaviVM
{
    public int Id { get; set; }
    public int OgrenciId { get; set; }
    public string OgrenciNo { get; set; } = "";
    public string OgrenciAdi { get; set; } = "";
    public string DersAdi { get; set; } = "";
    public string HocaAdi { get; set; } = "";
    public string SalonAdi { get; set; } = "";
    public string SinavTarihi { get; set; } = "";
    public string BaslangicSaati { get; set; } = "";
    public string BitisSaati { get; set; } = "";
    public decimal? Notu { get; set; }
}