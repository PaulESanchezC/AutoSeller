using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Models.ExternalLoginModels;

public class FacebookUserInfoResult
{
    [JsonPropertyName("first_name")]
    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("email")]
    [JsonProperty("email")]
    public string Username { get; set; }

    [JsonPropertyName("id")]
    [JsonProperty("id")]
    public string FacebookUserId { get; set; }
}