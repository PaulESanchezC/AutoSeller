using System.ComponentModel.DataAnnotations;

namespace Models.ImagesModels;

public class ImageCreateDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "The image is required")]
    public byte[]? ImageBytes { get; set; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "The Listed Vehicle is required")]
    public string ListedVehicleId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "the ImageIndex is required")]
    [Display(Name = "Image Index")]
    public int ImageIndex { get; set; }
}