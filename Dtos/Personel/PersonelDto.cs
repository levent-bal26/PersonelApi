namespace PersonelApi.DTOs.Personel;

public class PersonelDto
{
    public int PersonelId { get; set; }

    public string Ad { get; set; } = string.Empty;
    public string Soyad { get; set; } = string.Empty;

    public string BirimAd { get; set; } = string.Empty;
    public string UnvanAd { get; set; } = string.Empty;

    public decimal Maas { get; set; }
    public decimal Prim { get; set; }
}
