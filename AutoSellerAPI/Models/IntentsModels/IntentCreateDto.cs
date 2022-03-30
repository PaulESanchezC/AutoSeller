using System.ComponentModel.DataAnnotations;

namespace Models.IntentsModels;

public class IntentCreateDto
{
    [Required] 
    public string IntentSenderId { get; set; }

    [Required]
    public string IntentReceiverId { get; set; }

    [Required] 
    public string ListedVehicleId { get; set; }
    
}