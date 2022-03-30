using System.ComponentModel.DataAnnotations;

namespace Models.MakerModels;

public class MakerUpdateDto
{
    public string MakerId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Maker field is required")]
    [StringLength(100)]
    public string MakerName { get; set; }
}