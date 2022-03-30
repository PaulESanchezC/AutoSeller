using System.ComponentModel.DataAnnotations;
using Models.ApplicationUsersModels;
using Models.ListedVehiclesModels;

namespace Models.IntentsModels;

public class IntentDto
{
    [Required]
    public string IntentId { get; set; }

    //Control Data
    public bool IsSold { get; set; } = false;
    public DateTime DateSold { get; set; }
    public bool IsRead { get; set; } = false;
    public bool IsDiscarded { get; set; } = false;

    [Required]
    public string IntentSenderId { get; set; } = "";
    public ApplicationUser IntentSender { get; set; } = new();

    [Required]
    public string IntentReceiverId { get; set; } = "";
    public ApplicationUser IntentReceiver { get; set; } = new();

    [Required]
    public string ListedVehicleId { get; set; } = "";
    public ListedVehicle ListedVehicle { get; set; } = new();
}