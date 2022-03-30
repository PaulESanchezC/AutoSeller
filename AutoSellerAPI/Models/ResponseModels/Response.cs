namespace Models.ResponseModels;

public class Response 
{
    public string Title { get; set; }
    public string Message { get; set; }
    public bool IsSuccessful { get; set; }
    public int StatusCode { get; set; }
    public int TotalResponseCount { get; set; }
    public object? ResponseObject { get; set; }
}