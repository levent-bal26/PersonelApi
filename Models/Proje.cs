using System.ComponentModel.DataAnnotations;

namespace PersonelApi.Models;

public class Proje
{
    public int ProjeId { get; set; } // PK
    
    [Required]
    public string ProjeAd{ get; set; } = string.Empty;

   public DateTime BaslamaTarihi{ get; set; }

   public DateTime BitisTarihi{ get; set; }

}
