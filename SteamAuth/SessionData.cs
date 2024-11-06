using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SteamAuth.Helpers;

namespace SteamAuth
{
    public class SessionData
    {
        [JsonPropertyName("SteamID")]
        public ulong SteamId { get; set; }

        private string AccessToken { get; set; }

        [JsonPropertyName("SessionID")] 
        private string SessionId { get; set; }
        
        public CookieContainer GetCookies()
        {
            SessionId ??= GenerateSessionId();

            var cookies = new CookieContainer();
            cookies.Add(new Cookie("steamLoginSecure", GetSteamLoginSecure(), "/", "steamcommunity.com"));
            cookies.Add(new Cookie("sessionid", SessionId, "/", "steamcommunity.com"));
            cookies.Add(new Cookie("mobileClient", "android", "/", "steamcommunity.com"));
            cookies.Add(new Cookie("mobileClientVersion", "777777 3.6.1", "/", "steamcommunity.com"));
            return cookies;
        }

        private string GetSteamLoginSecure()
        {
            return SteamId + "%7C%7C" + AccessToken;
        }

        private static string GenerateSessionId()
        {
            return GetRandomHexNumber(32);
        }

        private static string GetRandomHexNumber(int digits)
        {
            var random = new Random();
            var buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            var result = string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }
    }
}
