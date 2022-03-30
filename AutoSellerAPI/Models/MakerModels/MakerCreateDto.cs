using System.ComponentModel.DataAnnotations;

namespace Models.MakerModels;

public class MakerCreateDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Maker field is required")]
    [StringLength(100)]
    public string MakerName { get; set; }
}