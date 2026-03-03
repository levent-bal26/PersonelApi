using System.ComponentModel.DataAnnotations;

namespace PersonelApi.Models;

public class Gorevlendirme
{
    public int GorevlendirmeId { get; set; } // PK

    [Required]
    public string ProjeNo { get; set; } = string.Empty;
    
    public int PersonelId { get; set; }

}
