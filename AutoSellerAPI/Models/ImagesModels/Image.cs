using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace Models.ImagesModels;

public class Image
{
    [Key] 
    public string ImageId { get; set; } = Guid.NewGuid().ToString();
    
    [Required(AllowEmptyStrings = false,ErrorMessage = "The image is required")]
    public byte[]? ImageBytes { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The Listed Vehicle is required")]
    public string ListedVehicleId { get; set; }

    [Required(AllowEmptyStrings = false,ErrorMessage = "the ImageIndex is required")]
    [Display(Name = "Image Index")]
    public int ImageIndex { get; set; }
}