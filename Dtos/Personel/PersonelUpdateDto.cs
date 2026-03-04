namespace PersonelApi.DTOs.Personel;

public class PersonelUpdateDto
{
    public string Ad { get; set; } = string.Empty;
    public string Soyad { get; set; } = string.Empty;
    public DateTime DogumTarihi { get; set; }

    public int BirimId { get; set; }
    public int UnvanId { get; set; }

    public decimal Maas { get; set; }
    public decimal Prim { get; set; }

    public byte CalismaSaati { get; set; }
}
