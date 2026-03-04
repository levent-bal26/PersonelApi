namespace PersonelApi.DTOs.Personel;

public class PersonelListDto
{
    public int PersonelId { get; set; }

    public string Ad { get; set; } = string.Empty;
    public string Soyad { get; set; } = string.Empty;

    public string BirimAd { get; set; } = string.Empty;
}
