using Microsoft.EntityFrameworkCore;

using PersonelApi.Models;

namespace PersonelApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Birim> Birimler => Set<Birim>();

    public DbSet<Cocuk> Cocuklar => Set<Cocuk>();

    public DbSet<Gorevlendirme> Gorevlendirmeler => Set<Gorevlendirme>();

    public DbSet<Il> Iller => Set<Il>();

    public DbSet<Ilce> Ilceler => Set<Ilce>();

    public DbSet<Personel> Personeller => Set<Personel>();

    public DbSet<Proje> Projeler => Set<Proje>();

    public DbSet<Unvan> Unvanlar => Set<Unvan>();

}
