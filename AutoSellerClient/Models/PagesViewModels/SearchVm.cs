using System.ComponentModel.DataAnnotations;
using Models.EnumModels;
using Models.MakerModels;

namespace Models.PagesViewModels;

public class SearchVm
{
    public string? MakerName { get; set; } 
    public int MaxPrice { get; set; } = 0;
    public int MaxYear { get; set; } = DateTime.Now.Year;
    public int Mileage { get; set; } = 0;
    public string? Transmission { get; set; }
    public string? DriveTrain { get; set; } 
    public string? Color { get; set; } 
    public int pageSize { get; set; } = 4;
    public int CurrentPage { get; set; } = 1;
}