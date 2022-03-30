using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Models.ExternalLoginModels;

public class FacebookTokenValidationResult
{
    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public Data Data { get; set; }
}

public class Data
{
    [JsonProperty("app_id")]
    [JsonPropertyName("app_id")]
    public string AppId { get; set; }

    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("application")]
    [JsonProperty("application")]
    public string Application { get; set; }

    [JsonPropertyName("data_access_expires_at")]
    [JsonProperty("data_access_expires_at")]
    public long DataAccessExpiresAt { get; set; }

    [JsonPropertyName("expires_at")]
    [JsonProperty("expires_at")]
    public long ExpiresAt { get; set; }

    [JsonPropertyName("is_valid")]
    [JsonProperty("is_valid")]
    public bool IsValid { get; set; }

    [JsonPropertyName("scopes")]
    [JsonProperty("scopes")]
    public List<object> Scopes { get; set; }

    [JsonPropertyName("user_id")]
    [JsonProperty("user_id")]
    public string UserId { get; set; }

    [JsonPropertyName("error")]
    [JsonProperty("error")]
    public Error Error { get; set; } = new ();
}
public class Error
{
    [JsonPropertyName("code")]
    [JsonProperty("code")]
    public long Code { get; set; } = 0;

    [JsonPropertyName("message")]
    [JsonProperty("message")]
    public string Message { get; set; } = "OK";
}
