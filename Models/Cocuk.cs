using System.ComponentModel.DataAnnotations;

namespace PersonelApi.Models;

public class Cocuk
{
    public int CocukId { get; set; } // PK

    [Required]
    public string Ad { get; set; } = string.Empty;

    [Required]
    public string Soyad { get; set; } = string.Empty;

    [Required]
    public string Cinsiyet { get; set; } = string.Empty;

    public DateTime DogumTarihi { get; set; } 

    [Required]
    public string DogumYeri { get; set; } = string.Empty;

    public int PersonelId { get; set; }

}
