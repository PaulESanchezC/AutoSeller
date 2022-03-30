using System.ComponentModel.DataAnnotations;
namespace Models.IntentsModels;

public class IntentUpdateDto
{
    [Required]
    public string IntentId { get; set; }

    [Required] 
    public string IntentSenderId { get; set; }

    [Required]
    public string IntentReceiverId { get; set; }

    [Required] 
    public string ListedVehicleId { get; set; }
    
    [Required]
    public bool IsRead { get; set; } = false;
    
    [Required]
    public bool IsDiscarded { get; set; } = false;

}