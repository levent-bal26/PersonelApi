using System.ComponentModel.DataAnnotations;

namespace PersonelApi.Models;

public class Ilce
{
    public int IlceId { get; set; } // PK

    [Required]
    public string IlceAd { get; set; } = string.Empty;

    public int IlId { get; set; } // FK

}