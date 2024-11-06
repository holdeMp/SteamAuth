using System.Text.Json.Serialization;

namespace SteamAuth;

public class SendConfirmationResponse
{
    [JsonPropertyName("success")] 
    public bool Success { get; set; }
}
