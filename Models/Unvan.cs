using System.ComponentModel.DataAnnotations;

namespace PersonelApi.Models;

public class Unvan
{
    public int UnvanId { get; set; } // PK

    [Required]
    public string UnvanAd { get; set; } = string.Empty;
}
