using System.ComponentModel.DataAnnotations;
using Models.ApplicationUsersModels;
using Models.ListedVehiclesModels;

namespace Models.IntentsModels;

public class Intent
{
    [Key]
    public string IntentId { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public DateTime DateOfIntent { get; set; } = DateTime.Now;

    //Control Data
    public bool IsSold { get; set; } = false;
    public DateTime DateSold { get; set; }
    public bool IsRead { get; set; } = false;
    public bool IsDiscarded { get; set; } = false;


    //FluentApi
    public string IntentSenderId { get; set; } = "";
    public ApplicationUser IntentSender { get; set; }

    //FluentApi
    public string IntentReceiverId { get; set; } = "";
    public ApplicationUser IntentReceiver { get; set; }

    //Vehicle
    [Required]
    public string ListedVehicleId { get; set; } = "";
    public ListedVehicle ListedVehicle { get; set; }
}