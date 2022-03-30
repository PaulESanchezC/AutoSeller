using System.ComponentModel.DataAnnotations;
using Models.ApplicationUserModels;
using Models.ListedVehicleModels;

namespace Models.IntentModels;

public class Intents
{
    [Required]
    public string IntentId { get; set; }

    [Required]
    public string IntentSenderId { get; set; }
    public ApplicationUser IntentSender { get; set; }

    //Control data
    public bool IsSold { get; set; } = false;
    public DateTime DateSold { get; set; }
    public bool IsRead { get; set; } = false;
    public bool IsDiscarded { get; set; } = false;

    [Required]
    public string IntentReceiverId { get; set; }
    public ApplicationUser IntentReceiver { get; set; }

    [Required]
    public DateTime DateOfIntent { get; set; }

    [Required]
    public string ListedVehicleId { get; set; }
    public ListedVehicle ListedVehicle { get; set; }
}