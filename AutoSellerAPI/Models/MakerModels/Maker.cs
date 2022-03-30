using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Models.VehiclesModels;

namespace Models.MakerModels;

[Index(nameof(MakerName),IsUnique = true)]
public class Maker
{
    [Key] 
    public string MakerId { get; set; } = Guid.NewGuid().ToString();
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Maker field is required")]
    [StringLength(100)]
    public string MakerName { get; set; }

    public IList<Vehicle> Vehicles { get; set; }
}