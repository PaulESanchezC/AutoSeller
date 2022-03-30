namespace Models.ImagesModels;

public class ImagesCreateVm
{
    public byte[]? ImageBytes { get; set; }
    public string ListedVehicleId { get; set; }
    public int ImageIndex { get; set; }
}