using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SteamWebAuthenticator;

namespace SteamAuth
{
    public partial class Confirmation
    {
        [JsonPropertyName("id")]
        public ulong Id { get; set; }

        [JsonPropertyName("nonce")]
        public ulong Key { get; set; }

        [JsonPropertyName("creator_id")]
        public ulong Creator { get; set; }

        [JsonPropertyName("headline")]
        public string Headline { get; set; }

        [JsonPropertyName("summary")]
        public List<string> Summary { get; set; }

        [JsonPropertyName(Constants.Accept)]
        public string Accept { get; set; }

        [JsonPropertyName(Constants.Cancel)]
        public string Cancel { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter ))]
        public EMobileConfirmationType ConfType { get; set; } = EMobileConfirmationType.Invalid;
    }
}
