namespace Models.ListedVehiclesModels;

public class ListedVehicleSearchModelDto
{
    public string? MakerName { get; set; }
    public int MaxPrice { get; set; }
    public int MaxYear { get; set; }
    public int Mileage { get; set; }
    public string? Transmission { get; set; }
    public string? DriveTrain { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public string? Color { get; set; }
}