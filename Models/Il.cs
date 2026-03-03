using System.ComponentModel.DataAnnotations;

namespace PersonelApi.Models;

public class Il
{
    public int IlId { get; set; } // PK

    [Required]
    public string IlAd { get; set; } = string.Empty;
}
