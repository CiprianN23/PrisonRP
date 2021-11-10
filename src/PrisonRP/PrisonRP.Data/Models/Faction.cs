using System.ComponentModel.DataAnnotations;

namespace PrisonRP.Data.Models;

public class Faction
{
    public int Id { get; set; }

    [Required]
    [MaxLength(12)]
    public string Name { get; set; }
    public ICollection<User> Players { get; set; }
}
