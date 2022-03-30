using Models.IntentModels;

namespace Models.ListedVehicleModels;

public class ListedVehicleSoldVm
{
    public ListedVehicle ListedVehicle { get; set; } = new ListedVehicle();
    public Intents Intent { get; set; } = new Intents();
}