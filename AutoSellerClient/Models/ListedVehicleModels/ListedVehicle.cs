using Models.ApplicationUserModels;
using Models.ImagesModels;
using Models.VehicleModels;

namespace Models.ListedVehicleModels;

public class ListedVehicle
{
    public string ListedVehicleId { get; set; }
    public string Transmission { get; set; }
    public string Color { get; set; }
    public string DriveTrain { get; set; }
    public int Year { get; set; }
    public int Mileage { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public DateTime DateListed { get; set; }
    
    //Control data
    public bool IsSold { get; set; } = false;
    public DateTime DateSold { get; set; }

    //Application User
    public string ApplicationUserId { get; set; } = "";
    public ApplicationUser ApplicationUser { get; set; }

    //Vehicle Model
    public string VehicleId { get; set; }
    public Vehicles Vehicle { get; set; }
    
    //Images
    public IList<Images> Images { get; set; } = new List<Images>();
}