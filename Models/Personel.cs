using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace PersonelApi.Models;

public class Personel
{
    public int PersonelId { get; set; } // PK

    [Required]
    public string Ad{ get; set; } = string.Empty;

   [Required]
    public string Soyad { get; set; } = string.Empty;

   [Required]
    public string Cinsiyet { get; set; } = string.Empty;
    
    public DateTime DogumTarihi { get; set; }

   [Required]
    public string DogumYeri { get; set; } = string.Empty;

    public DateTime BaslamaTarihi { get; set; } 

    public int BirimId { get; set; }

    public int UnvanId { get; set; }

    public byte CalismaSaati { get; set; }

   [Column(TypeName = "decimal(18,2)")]
   public decimal Maas { get; set; }

  [Column(TypeName = "decimal(18,2)")]
  public decimal Prim { get; set; } = 0m;

}
