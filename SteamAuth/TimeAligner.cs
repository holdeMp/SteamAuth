using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using SteamAuth.Helpers;

namespace SteamAuth
{
    /// <summary>
    /// Class to help align system time with the Steam server time. Not super advanced; probably not taking some things into account that it should.
    /// Necessary to generate up-to-date codes. In general, this will have an error of less than a second, assuming Steam is operational.
    /// </summary>
    public class TimeAligner
    {
        private static bool _aligned;
        private static int _timeDifference;
        private static HttpClient _httpClient;
        public static long GetSteamTime()
        {
            if (!_aligned)
            {
                AlignTime();
            }
            return Util.GetSystemUnixTime() + _timeDifference;
        }

        private static async void AlignTime()
        {
            var currentTime = Util.GetSystemUnixTime();
            using var client = new HttpClient();
            try
            {
                var jsonString = await "steamid=0".ToJsonAsync();
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(APIEndpoints.TWO_FACTOR_TIME_QUERY, content);
                var query = await response.Content.ToString().FromJsonAsync<TimeQuery>();
                _timeDifference = (int)(query.Response.ServerTime - currentTime);
                _aligned = true;
            }
            catch (WebException)
            {
            }
        }

        internal abstract class TimeQuery
        {
            [JsonPropertyName("response")]
            internal TimeQueryResponse Response { get; set; }

            internal abstract class TimeQueryResponse
            {
                [JsonPropertyName("server_time")]
                public long ServerTime { get; set; }
            }

        }
    }
}
