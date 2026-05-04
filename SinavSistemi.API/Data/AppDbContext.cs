using Microsoft.EntityFrameworkCore;
using SinavSistemi.API.Models;

namespace SinavSistemi.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Hoca> Hocalar { get; set; }
    public DbSet<Salon> Salonlar { get; set; }
    public DbSet<Ogrenci> Ogrenciler { get; set; }
    public DbSet<Ders> Dersler { get; set; }
    public DbSet<OgrenciDersi> OgrenciDersleri { get; set; }
    public DbSet<Sinav> Sinavlar { get; set; }
    public DbSet<OgrenciSinavi> OgrenciSinavlari { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OgrenciDersi>()
            .HasIndex(x => new { x.OgrenciId, x.DersId })
            .IsUnique();

        modelBuilder.Entity<OgrenciSinavi>()
            .HasIndex(x => new { x.OgrenciId, x.SinavId })
            .IsUnique();

        modelBuilder.Entity<OgrenciSinavi>()
            .Property(x => x.Notu)
            .HasPrecision(5, 2);

        // ✅ TEK VE DOĞRU RELATION (CASCADE)
        modelBuilder.Entity<OgrenciSinavi>()
            .HasOne(x => x.Sinav)
            .WithMany(x => x.OgrenciSinavlari)
            .HasForeignKey(x => x.SinavId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}