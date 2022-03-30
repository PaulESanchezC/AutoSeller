using System.ComponentModel.DataAnnotations;

namespace Models.ImagesModels;

public class ImageUpdateDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "The image Id is required")]
    public string ImageId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "the ImageIndex is required")]
    [Display(Name = "Image Index")]
    public int ImageIndex { get; set; }
}