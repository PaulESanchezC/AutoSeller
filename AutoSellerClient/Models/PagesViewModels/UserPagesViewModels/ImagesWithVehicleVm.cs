using System.ComponentModel.DataAnnotations;

namespace Models.PagesViewModels.UserPagesViewModels;

public class ImagesWithVehicleVm
{
    public string ListedVehicleId { get; set; }

    [Display(Name = "Add images to the Vehicle")]
    public byte[] ImageBytes { get; set; }
}