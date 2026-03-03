using System.ComponentModel.DataAnnotations;

namespace PersonelApi.Models;

public class Birim
{
    public int BirimId { get; set; }

    [Required]
    public string BirimAd { get; set; } = string.Empty;
}
