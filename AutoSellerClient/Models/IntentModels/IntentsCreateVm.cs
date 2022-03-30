using System.ComponentModel.DataAnnotations;

namespace Models.IntentModels;

public class IntentsCreateVm
{
    [Required]
    public string IntentSenderId { get; set; }

    [Required]
    public string IntentReceiverId { get; set; }

    [Required]
    public string ListedVehicleId { get; set; }
}