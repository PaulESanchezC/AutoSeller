namespace Models.ImagesModels;

public class Images
{
    public string ImageId { get; set; }
    public byte[]? ImageBytes { get; set; }
    public string ListedVehicleId { get; set; }
    public int ImageIndex { get; set; }
}